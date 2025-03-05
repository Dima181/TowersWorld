using System;
using UniRx;
using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class DraggableView : MonoBehaviour
    {
        public ReactiveCommand<DraggableView> OnDragBegan = new();
        public ReactiveCommand<DraggableView> OnDragged = new();
        public ReactiveCommand<DraggableView> OnDragEnded = new();
        public ReactiveCommand<DraggableView> OnClick = new();

        private Collider _dragCollider;

        private Vector3 _catchOffset = default;
        private Vector3 _startClickPosition = default;
        private Camera _camera;
        private bool _isDragging;

        private float _dragTrashHold = 0.1f;

        private void Awake()
        {
            _dragCollider = GetComponent<Collider>();
            _camera = Camera.main;
            Enable();
        }

        private void OnMouseDown()
        {
            _startClickPosition = GetClickPosition();
            _catchOffset = transform.position - _startClickPosition;

            _isDragging = false;
        }

        private void OnMouseDrag()
        {
            var newObjectPosition = GetClickPosition();

            if(!_isDragging && (_startClickPosition - newObjectPosition).magnitude > _dragTrashHold)
            {
                OnDragBegan.Execute(this);
                _isDragging = true;
            }

            newObjectPosition.x = _catchOffset.x;
            newObjectPosition.z = _catchOffset.z;

            transform.position = newObjectPosition;

            if(_isDragging)
                OnDragged.Execute(this);
        }

        private Vector3 GetClickPosition()
        {
            Plane movementPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            movementPlane.Raycast(ray, out float enter);
            return ray.GetPoint(enter);
        }

        private void OnMouseUp()
        {
            OnDragEnded.Execute(this);
            if(!_isDragging)
                OnClick.Execute(this);
            _isDragging = false;
        }

        public void Enable()
        {
            if(!_dragCollider)
                return;

            _dragCollider.enabled = true;
            enabled = true;
        }

        public void Disable()
        {
            if (!_dragCollider)
                return;

            _dragCollider.enabled = false;
            enabled = false;
        }
    }
}