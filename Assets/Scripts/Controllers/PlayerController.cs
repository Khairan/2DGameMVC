using UnityEngine;


namespace platformerMVC
{
    public class PlayerController
    {
        private bool _isJump;
        private bool _isMoving;

        private float _xAxisInput;
        private float _walkSpeed = 250f;
        private float _animationSpeed = 10f;
        private float _movingTreshhold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private float _jumpSpeed = 9f;
        private float _jumpTreshold = 1f;
        private float _fallTreshold = -1f;
        private float _yVelocity;
        private float _xVelocity;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _spriteAnimator;
        private ContactsPoller _contactsPoller;

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _playerView = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.idle, true, _animationSpeed);
            _contactsPoller = new ContactsPoller(_playerView._collider);
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _playerView._rigidbody.velocity = _playerView._rigidbody.velocity.Change(x: _xVelocity);
            _playerView.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }
        
        private void Jump()
        {
            if (_contactsPoller.IsGrounded)
            {
                _spriteAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.idle, true, _animationSpeed);

                if (_isJump && Mathf.Abs(_yVelocity) <= _jumpTreshold)
                {
                    _playerView._rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (_yVelocity > _jumpTreshold)
                {
                    _spriteAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
                else if (_yVelocity < _fallTreshold)
                {
                    _spriteAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Fall, true, _animationSpeed);
                }
            }
        }

        public void Update()
        {
            _contactsPoller.Update();
            _spriteAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = (Input.GetAxis("Vertical") > 0) || (Input.GetButtonDown("Jump"));
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshhold;
            _yVelocity = _playerView._rigidbody.velocity.y;

            if (_isMoving)
            {
                MoveTowards();
            }

            Jump();
        }
    }
}
