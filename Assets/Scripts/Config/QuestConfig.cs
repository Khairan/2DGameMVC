using UnityEngine;


namespace platformerMVC
{
    public enum QuestType
    {
        Coins
    }

    [CreateAssetMenu(fileName = "QuestCfg", menuName = "Configs / Quest Cfg", order = 1)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType questType;
    }
}
