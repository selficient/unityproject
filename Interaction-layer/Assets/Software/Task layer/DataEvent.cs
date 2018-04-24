using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Wordt aangeroepen als data ready is vanuit de Business laag (Voor dashboard laden en hardware laag laden).
 * @author T.J van der Ende
 * */
namespace Task {
	public class DataEvent : MonoBehaviour {
		private UnityAction<System.Object> doneLoadingHardware;

		void Awake() {

			doneLoadingHardware = new UnityAction<System.Object> (DoneLoadingHardware);
		}
		void DoneLoadingHardware(System.Object list){
			EventManager.TriggerEvent ("triggerHardwareBuild", list);
		}
		void OnEnable(){
			EventManager.StartListening ("doneLoadingHardware", DoneLoadingHardware);	

		}
		void Disable(){
			EventManager.StopListening ("doneLoadingHardware", DoneLoadingHardware);
		}
	}

}
