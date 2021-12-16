using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace platformerMVC
{
    public class QuestsConfiguratorController
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuest;
        private CoinQuestModel _model;

        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questObjects;

        private List<IQuestStory> _questStories;

        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>(1);
        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);

        public QuestsConfiguratorController(QuestView questView)
        {
            _singleQuestView = questView._singleQuest;
            _model = new CoinQuestModel();

            _questStoryConfigs = questView._questStoryConfigs;
            _questObjects = questView._questObjects;
        }
        
        public void Init()
        {
            _singleQuest = new QuestController(_singleQuestView, _model);
            _singleQuest.Reset();

            _questStoryFactories.Add(QuestStoryType.Common, questCollection => new QuestStroyController(questCollection));
            _questStoryFactories.Add(QuestStoryType.Resettable, questCollection => new ResettableQuestStoryController(questCollection));

            _questFactories.Add(QuestType.Coins, () => new CoinQuestModel());

            _questStories = new List<IQuestStory>();
            foreach (QuestStoryConfig questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig questCfg in cfg.quests)
            {
                IQuest quest = CreateQuest(questCfg);
                if (quest == null) continue;
                quests.Add(quest);
                Debug.Log("AddQuest");
            }

            return _questStoryFactories[cfg.questStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            int questId = config.id;

            QuestObjectView questView = _questObjects.FirstOrDefault(value => value.Id == config.id);
            if (questView == null)
            {
                Debug.Log("No views");
                return null;
            }

            if (_questFactories.TryGetValue(config.questType, out var factory))
            {
                IQuestModel questModel = factory.Invoke();
                return new QuestController(questView, questModel);
            }

            Debug.Log("No model");
            return null;
        }
    }
}
