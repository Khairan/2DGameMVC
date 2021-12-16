using System.Collections.Generic;
using UnityEngine;


namespace platformerMVC
{
    public class BulletEmitterController
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet;

        private float _delay = 2f;
        private float _startSpeed = 10f;

        public BulletEmitterController(List<LevelObjectView> bulletViews, Transform emitterTransform)
        {
            _transform = emitterTransform;
            foreach (LevelObjectView bulletView in bulletViews)
            {
                _bullets.Add(new BulletController(bulletView));
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position, -_transform.up * _startSpeed);
                _currentIndex++;
                if (_currentIndex >= _bullets.Count)
                {
                    _currentIndex = 0;
                }
                _bullets[_currentIndex].Active(false);
            }
        }
    }
}
