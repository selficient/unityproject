using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Task;
using Business.Domain;

namespace Business {
	
	public class AreaService : MonoBehaviour{
		//string uri = "http://localhost:3000/service/getallhardware?apikey=kdfjadslj2xk";
		private UnityAction<System.Object> saveHardwareState;

		[SerializeField] private string uri;
		private string stateUri = "http://localhost:3000/service/updatestate?apikey=kdfjadslj2xk";
		[SerializeField] private string areaId; // TODO: De NoSi zo aanpassen dat er per area in geladen kan worden.
		private IDataService serviceImplementation;
		// Use this for initialization

		void Awake(){
			saveHardwareState = new UnityAction<System.Object> (SaveHardwareState);

		}
		void OnEnable(){
			Debug.Log ("works");

			EventManager.StartListening ("updateHardwareState", SaveHardwareState); 

		}
		void OnDisable(){
			EventManager.StopListening ("updateHardwareState", SaveHardwareState); 

		}
		void Start () {
			serviceImplementation = new HardwareService ();
			StartCoroutine(AreaLoader(uri));
		}
	
		#region Hier worden alle data requests afgehandeld.
		private IEnumerator AreaLoader (string uri) // TODO : Hier dus een areaId aan mee geven.
		{
			return this.serviceImplementation.AreaLoader (uri);
		}

		private void SaveHardwareState (System.Object hardwareObject)
		{
			EventManager.TriggerEvent ("showInteractiveLoader", true);
			StartCoroutine (this.serviceImplementation.SaveHardwareState (hardwareObject, stateUri));
		}
		#endregion

	
	}

}
