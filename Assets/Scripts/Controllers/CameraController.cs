using UnityEngine;


namespace platformerMVC
{
    public class CameraController
    {
        private float targetX;
        private float targetY;

        private float _offsetX = 6f;
        private float _offsetY = 4f;

        private int _camSpeed = 1;

        private float _xAxisInput;
        private float _yAxisInput;

        private LevelObjectView _player;
        private Transform _playerTransform;
        private Transform _cameraTransform;

        public CameraController(LevelObjectView player, Transform camera)
        {
            _player = player;
            _playerTransform = player.transform;
            _cameraTransform = camera;
        }

        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = Input.GetAxis("Vertical");

            float offsetX = 0;
            if (_xAxisInput > 0)
                offsetX = _offsetX;
            else if (_xAxisInput < 0)
                offsetX = -_offsetX;
            else
                offsetX = 0;

            float offsetY = 0;
            if (_yAxisInput < 0)
                offsetY = -_offsetY;
            else
                offsetY = 0;

            float yCamSpeed = _camSpeed;
            float _yVelocity = _player._rigidbody.velocity.y;
            if (_yVelocity < -1.0f)
                yCamSpeed = Mathf.Abs(_yVelocity);

            targetX = _playerTransform.position.x + offsetX;
            targetY = _playerTransform.position.y + offsetY;

            float xNew = Mathf.Lerp(_cameraTransform.position.x, targetX, Time.deltaTime * _camSpeed);
            float yNew = Mathf.Lerp(_cameraTransform.position.y, targetY, Time.deltaTime * yCamSpeed);
            
            _cameraTransform.position = new Vector3(xNew, yNew, _cameraTransform.position.z);

        }
    }
}
