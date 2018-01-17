using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kolejka.Events
{
    [CreateAssetMenu(menuName = "Game Events/Simple")]
    public class GameEvent : Event
    {
        private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

        public override void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public override void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public override void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}
