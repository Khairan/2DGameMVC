using UnityEngine;


namespace platformerMVC
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;
        private TrailRenderer _trailRenderer;
        private Vector3 _initialPosition;
        
        public BulletController(LevelObjectView view)
        {
            _view = view;
            view.gameObject.TryGetComponent<TrailRenderer>(out _trailRenderer);
            _initialPosition = _view.transform.position;
            Active(false);
        }

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
            if (_trailRenderer) _trailRenderer.enabled = val;
            if (val == false) _view.transform.position = _initialPosition;
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            float angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 axis = Vector3.Cross(Vector3.left, _velocity);
            _view._transform.rotation = Quaternion.AngleAxis(angle, axis);

        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            Active(true);
            SetVelocity(velocity);
            _view._transform.position = position;
            _view._rigidbody.velocity = Vector2.zero;
            _view._rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}
