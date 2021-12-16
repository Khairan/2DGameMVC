using UnityEngine;


namespace platformerMVC
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}

