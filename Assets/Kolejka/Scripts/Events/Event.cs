using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kolejka.Events
{
    public abstract class Event : ScriptableObject
    {
        public abstract void Raise();
        public abstract void RegisterListener(GameEventListener listener);
        public abstract void UnregisterListener(GameEventListener listener);
    }
}
