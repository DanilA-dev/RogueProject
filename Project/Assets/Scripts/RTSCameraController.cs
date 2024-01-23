using UnityEngine;
using Cinemachine;
using InputActions;

namespace RTSCamera
{
    public class RTSCameraController : MonoBehaviour
    {   

        [SerializeField] private bool _enableMouseMove;
        [SerializeField] private bool _enableKeyboardMove;

        [Space]
        [SerializeField] private float _panSpeed;
        [SerializeField] private float _zoomSpeed;

        private CinemachineVirtualCamera _virtualCam;
        private CinemachineConfiner _confiner;
        private CameraInput _camInput;

        private Vector3 _mouseMoveDir;
        private Vector3 _keyboardMoveDir;

        private void Awake()
        {
            _virtualCam = GetComponent<CinemachineVirtualCamera>();
            _confiner = GetComponent<CinemachineConfiner>();
        }
        
        private void Start()
        {
            _camInput = SystemInitializer.GetSystem<InputSystem>().CameraInput;
            _camInput.OnKeyboardMove += MoveCameraWASD;
            _camInput.OnMouseMove += MoveCameraWithMouse;
            _camInput.OnScroll += ZoomCamera;
        }

        private void OnDisable()
        {
            _camInput.OnKeyboardMove -= MoveCameraWASD;
            _camInput.OnMouseMove -= MoveCameraWithMouse;
            _camInput.OnScroll -= ZoomCamera;
        }

        private void Update()
        {
            CameraMovement();
        }

        private void CameraMovement()
        {
            ClampPosition();

            if(_mouseMoveDir != Vector3.zero && _keyboardMoveDir == Vector3.zero && _enableMouseMove)  
                MoveCamera(_mouseMoveDir.normalized);

            if(_keyboardMoveDir != Vector3.zero && _mouseMoveDir == Vector3.zero && _enableKeyboardMove)
                MoveCamera(_keyboardMoveDir);
        }

        private void MoveCameraWASD(Vector2 dir)
        {
            _keyboardMoveDir = new Vector3(dir.x, 0, dir.y);
        }

        private void MoveCameraWithMouse(Vector2 dir)
        {
            float yMaxBorder =  Screen.height * 0.95f;
            float yMinBorder = Screen.height * 0.05f;
            float xMaxBorder = Screen.width * 0.95f;
            float xMinBorder = Screen.width * 0.05f;


            if(dir.y >= yMaxBorder)
               _mouseMoveDir.z += _panSpeed * Time.deltaTime;
            else if(dir.y <= yMinBorder)
               _mouseMoveDir.z -= _panSpeed * Time.deltaTime;
            else if(dir.y > yMinBorder || dir.y < yMaxBorder)
               _mouseMoveDir.z = 0;

            if(dir.x >= xMaxBorder)
                _mouseMoveDir.x += _panSpeed * Time.deltaTime;
            else if(dir.x <= xMinBorder)
                _mouseMoveDir.x -= _panSpeed * Time.deltaTime;
            else if(dir.x > xMinBorder || dir.x < xMaxBorder)
                _mouseMoveDir.x = 0;
        }

        private void ZoomCamera(float z)
        {
            Vector3 zoomDir = z > 0 ? new Vector3(0, -z * _zoomSpeed * Time.deltaTime, 0)
             : new Vector3(0, z * -(_zoomSpeed * Time.deltaTime), 0);
            MoveCamera(zoomDir);
        }

        private void ClampPosition()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _confiner.m_BoundingVolume.bounds.min.x, _confiner.m_BoundingVolume.bounds.max.x),
            Mathf.Clamp(transform.position.y, _confiner.m_BoundingVolume.bounds.min.y, _confiner.m_BoundingVolume.bounds.max.y),
            Mathf.Clamp(transform.position.z, _confiner.m_BoundingVolume.bounds.min.z, _confiner.m_BoundingVolume.bounds.max.z));
        }

        private void MoveCamera(Vector3 dir)
        {
            transform.position = transform.position + dir * Time.deltaTime * _panSpeed;
        }
    }

}
