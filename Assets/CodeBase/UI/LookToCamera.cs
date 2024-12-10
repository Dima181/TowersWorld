using UnityEngine;

namespace UI
{
    public class LookToCamera : MonoBehaviour
    {
        [SerializeField] private bool _alwaysUpdate;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;

            if (_camera.orthographic)
                transform.transform.rotation = _camera.transform.rotation;
        }

        private void Update()
        {
            if (!_alwaysUpdate && _camera.orthographic) return;

            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }
}
