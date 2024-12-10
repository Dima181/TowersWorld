using Gameplay.Model;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.View
{
    public class BuildingView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        public Subject<Unit> OnClick => _onClickSubject;

        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Canvas[] _worldCanvases;

        private Subject<Unit> _onClickSubject;
        private Vector3 _startMousePos;

        public void SetWorldCanvases(Camera worldCamera)
        {
            foreach (var canvas in _worldCanvases)
                canvas.worldCamera = worldCamera;
        }

        public void SetBuildingName(string name) =>
            _nameText.text = name;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
            if ((Input.mousePosition - _startMousePos).sqrMagnitude < 100f)
                _onClickSubject.OnNext(Unit.Default);
        }

        public void OnPointerDown(PointerEventData eventData) =>
            _startMousePos = Input.mousePosition;
    }

    public class BuildingPresenter : IBuildingInitializable, IBuildingDisposable
    {
        private readonly BuildingView _view;
        private readonly BuildingModel _model;

        private CompositeDisposable _disposable;

        public BuildingPresenter(
            BuildingView view,
            BuildingModel model)
        {
            _view = view;
            _model = model;
            Debug.Log(_model.Name);
        }

        public void Dispose() =>
            _disposable?.Dispose();

        public void Initialize()
        {

            _view.SetBuildingName(_model.Name);
            _view.SetWorldCanvases(Camera.main);

            _disposable = new();

            _view.OnClick
                .Subscribe(_ => _model.OnBuildingClick.Execute())
                .AddTo(_disposable);

            _model.BuildingPoint = _view.gameObject;
        }
    }
}
