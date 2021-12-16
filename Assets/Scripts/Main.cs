using System.Collections.Generic;
using UnityEngine;


namespace platformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private int _animationSpeed = 10;

        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private SpriteAnimatorConfig _coinsConfig;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coinsViews;
        [SerializeField] private GeneratorLevelView _generatorView;
        [SerializeField] private QuestView _questView;

        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _coinAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private ParalaxManager _paralaxManager;
        private CannonAimController _cannonController;
        private BulletEmitterController _bulletEmitterController;
        private CoinsController _coinsController;
        private GeneratorController _generatorController;
        private QuestsConfiguratorController _questsController;

        void Start()
        {
            Transform mainCamera = Camera.main.transform;
            
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _coinsConfig = Resources.Load<SpriteAnimatorConfig>("CoinsAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
            }

            if (_coinsConfig)
            {
                _coinAnimator = new SpriteAnimatorController(_coinsConfig);
            }

            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
            
            _playerController = new PlayerController(_playerView, _playerAnimator);

            _cameraController = new CameraController(_playerView, mainCamera);

            _paralaxManager = new ParalaxManager(mainCamera, _background.transform);

            _cannonController = new CannonAimController(_cannonView._muzzleTransform, _playerView._transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView._bullets, _cannonView._emitterTransform);
            
            _coinsController = new CoinsController(_playerView, _coinsViews, _coinAnimator);
            
            _generatorController = new GeneratorController(_generatorView);
            _generatorController.Init();

            _questsController = new QuestsConfiguratorController(_questView);
            _questsController.Init();
        }

        void Update()
        {
            _playerController.Update();
            
            _cameraController.Update();
            
            _paralaxManager.Update();
            
            _cannonController.Update();
            if (_cannonController._canShoot)
            {
                _bulletEmitterController.Update();
            }
            
            _coinsController.Update();
        }
    }
}
