namespace _Scripts.CustomEvents.BaseEvent
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}