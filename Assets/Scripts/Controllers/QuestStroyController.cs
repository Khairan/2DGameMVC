using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace platformerMVC
{
    public class QuestStroyController : IQuestStory
    {
        #region Fields

        private readonly List<IQuest> _questsCollection = new List<IQuest>();

        public bool IsDone => _questsCollection.All(value => value.IsCompleted);

        #endregion

        #region Life Cycle

        public QuestStroyController(List<IQuest> questsCollection)
        {
            _questsCollection = questsCollection;
            Subscribe();
            // старт первого квеста
            ResetQuest(0);
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
            if (IsDone)
            {
                Debug.Log("Story is Done!");
            }
            else
            {
                // если очередной квест выполнен, запускаем следующий квест
                ResetQuest(++index);
            }
        }

        private void ResetQuest(int index)
        {
            if (index < 0 || index >= _questsCollection.Count) 
                return;

            IQuest nextQuest = _questsCollection[index];
            
            if (nextQuest.IsCompleted)
                OnQuestCompleted(this, nextQuest);
            else 
                _questsCollection[index].Reset();
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
