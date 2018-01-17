using UnityEngine;
using UnityEngine.Events;

namespace Kolejka.Events
{
    public class GameEventListener : MonoBehaviour
    {
        public Event gameEvent;
        public UnityEvent Response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}