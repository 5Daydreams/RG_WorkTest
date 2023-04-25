using _Scripts.CustomEvents.BaseEvent;
using UnityEngine;

namespace _Scripts.CustomEvents.VoidEvents
{
    [CreateAssetMenu(menuName = "CustomScriptables/Events/VoidEvent", fileName = "NewVoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}