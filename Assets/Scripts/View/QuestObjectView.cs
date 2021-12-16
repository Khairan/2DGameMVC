using UnityEngine;


namespace platformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        [SerializeField] private Color _completedColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private int _id;

        public int Id => _id;

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
        }
        
        public void ProcessComplete()
        {
            _spriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            _spriteRenderer.color = _defaultColor;
        }
    }
}
