using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace platformerMVC
{
    public class CoinsController : IDisposable
    {
        private const float _animationSpeed = 10f;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _coinAnimator;
        private List<LevelObjectView> _coinsViews;

        public CoinsController(LevelObjectView playerView, List<LevelObjectView> coinsViews, SpriteAnimatorController coinAnimator)
        {
            _playerView = playerView;
            _coinAnimator = coinAnimator;
            _coinsViews = coinsViews;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (LevelObjectView coinView in _coinsViews)
            {
                _coinAnimator.StartAnimation(coinView._spriteRenderer, AnimState.Run, true, _animationSpeed);
            }
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_coinsViews.Contains(contactView))
            {
                _coinAnimator.StopAnimation(contactView._spriteRenderer);
                GameObject.Destroy(contactView.gameObject);
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }

        public void Update()
        {
            _coinAnimator.Update();
        }
    }
}
