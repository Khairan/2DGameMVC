using System.Collections.Generic;
using UnityEngine;


namespace platformerMVC
{
    [CreateAssetMenu(fileName = "QuestItemCfg", menuName = "Configs / Quest Item Cfg", order = 1)]
    public class QuestItemConfig : ScriptableObject
    {
        public int questId;
        public List<int> questItemCollection;
    }
}
