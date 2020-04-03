using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Task { 
	public class SelficientEvent : UnityEvent<System.Object> {
	}

	/**
 * Overgenomen van https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system
 * Wordt gebruikt om events te registreren en af te handelen.
*/
	public class EventManager : MonoBehaviour{

		private Dictionary <string, SelficientEvent> eventDictionary;
		private static EventManager eventManager;
        private static bool created = false;
        void Awake()
        {
            if (!created)
            {
                DontDestroyOnLoad(gameObject);
                created = true;
            }
        }
        public static EventManager instance
		{
			get{
				if (!eventManager) {
					eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;
					if (!eventManager) {
						Debug.LogError ("Minimaal één actieve eventmanager script actief op een GameObject");
					} else {
						eventManager.Init ();
					}
				}
				return eventManager;
			}
		}
		void Init (){

			if (eventDictionary == null) {
				eventDictionary = new Dictionary<string, SelficientEvent> ();
			}
		}
		public static void StartListening (string eventName, UnityAction<System.Object> listener){
			SelficientEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.AddListener (listener);

			} else {
				thisEvent = new SelficientEvent();
				thisEvent.AddListener (listener);
				instance.eventDictionary.Add (eventName, thisEvent);
			}
		}
		public static void StopListening(string eventName, UnityAction<System.Object> listener){
			if (eventManager == null)
				return;
			SelficientEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.RemoveListener (listener);
			}
		}

		public static void TriggerEvent (string eventName, System.Object data){
			SelficientEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.Invoke (data);
			}
		}

        internal static void StartListening(string v, object disableWalking)
        {
            throw new NotImplementedException();
        }
    }

}
