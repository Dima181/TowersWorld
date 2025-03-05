using System.Security.Cryptography;
using UnityEngine;
using static UI.UILookToCamera;

namespace UI
{
    public class UILookToCamera : MonoBehaviour
    {
        public enum ETypeOfRotation
        {
            CopyRotationFromCamera = 0,
            LookAtCamera = 1
        }

        [Tooltip("CopyRotationFromCamera: A more efficient way to rotate, but copies the camera rotation and may not be suitable for some cases" +
        "\nLookAtCamera: An alternative way of rotation is for the current object to look at the location of the camera, which is more resource-intensive")]
        [SerializeField] private ETypeOfRotation _typeOfRotation;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            switch (_typeOfRotation)
            {
                case ETypeOfRotation.CopyRotationFromCamera:
                    //A more efficient way to rotate, but copies the camera rotation and may not be suitable for some cases
                    transform.rotation = _camera.transform.rotation;
                    return;
                case ETypeOfRotation.LookAtCamera:
                    //An alternative way of turning, resource-intensive
                    transform.LookAt(worldPosition: _camera.transform.position);
                    return;
                default:
                    return;
            }
        }
    }
}
