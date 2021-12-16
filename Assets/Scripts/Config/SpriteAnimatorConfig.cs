using System;
using System.Collections.Generic;
using UnityEngine;


namespace platformerMVC
{
    public enum AnimState
    {
        idle = 0,
        Run = 1,
        Jump = 2,
        Fall = 3
    }

    [CreateAssetMenu(fileName = "SpriteAnimCfg", menuName = "Configs / Animatioon Cfg", order = 1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequence = new List<SpriteSequence>();
    }
}

