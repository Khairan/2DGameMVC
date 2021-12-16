using UnityEngine;


namespace platformerMVC
{
    public class CannonAimController
    {
        public bool _canShoot;

        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _dir;
        private float _angle;
        private Vector3 _axis;
        private float _maxDistance = 8f;

        public CannonAimController(Transform muzzleTransform, Transform targetTransform)
        {
            _muzzleTransform = muzzleTransform;
            _targetTransform = targetTransform;
        }

        public void Update()
        {
            _dir = _targetTransform.position - _muzzleTransform.position;
            _angle = Vector3.Angle(Vector3.down, _dir);
            float distance = Mathf.Sqrt((_dir).sqrMagnitude);

            if (distance > _maxDistance)
            {
                _canShoot = false;
                return;
            }
            else _canShoot = true;

            _axis = Vector3.Cross(Vector3.down, _dir);
            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}
