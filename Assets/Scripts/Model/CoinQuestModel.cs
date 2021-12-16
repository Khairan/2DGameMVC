using UnityEngine;


namespace platformerMVC
{
    public class CoinQuestModel : IQuestModel
    {
        private const string TargetTag = "Player";

        public bool TryComplete(GameObject activator)
        {
            return activator.CompareTag(TargetTag);
        }
    }
}
