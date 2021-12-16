using System;


namespace platformerMVC
{
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }
    }
}
