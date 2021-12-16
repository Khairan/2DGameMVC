using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace platformerMVC
{
    public class ResettableQuestStoryController : IQuestStory
    {
        #region Fields

        private readonly List<IQuest> _questsCollection = new List<IQuest>();

        public bool IsDone => _questsCollection.All(value => value.IsCompleted);

        private int _currentIndex;

        #endregion

        #region Life Cycle

        public ResettableQuestStoryController(List<IQuest> questsCollection)
        {
            _questsCollection = questsCollection;
            Subscribe();
            ResetQuests();
        }

        #endregion

        #region Methods

        private void Subscribe()
        {
            foreach (var quest in _questsCollection)
                quest.Completed += OnQuestCompleted;
        }

        private void Unsubscribe()
        {
            foreach (var quest in _questsCollection)
                quest.Completed -= OnQuestCompleted;
        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            int index = _questsCollection.IndexOf(quest);
            
            if (_currentIndex == index)
            {
                _currentIndex++;
                if (IsDone) 
                    Debug.Log("Story is Done!");
            }
            else
            {
                // сбрасываем цепочку, если был выполнен не целевой квест
                ResetQuests();
            }
        }

        private void ResetQuests()
        {
            _currentIndex = 0;
            foreach (var quest in _questsCollection)
            {
                quest.Reset();
            }
        }

        public void Dispose()
        {
            Unsubscribe();
            foreach (var quest in _questsCollection)
                quest.Dispose();
        }

        #endregion
    }
}
