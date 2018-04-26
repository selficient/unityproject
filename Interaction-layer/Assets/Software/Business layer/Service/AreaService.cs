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

		[SerializeField] private string uri;
		[SerializeField] private string areaId; // TODO: De NoSi zo aanpassen dat er per area in geladen kan worden.
		private IDataService serviceImplementation;
		// Use this for initialization

		void Start () {
			serviceImplementation = new HardwareService ();
			StartCoroutine(AreaLoader(uri));
		}
	
		#region Hier worden alle data requests afgehandeld.
		private IEnumerator AreaLoader (string uri) // TODO : Hier dus een areaId aan mee geven.
		{
			return this.serviceImplementation.AreaLoader (uri);
		}

		private void SaveHardwareState (Hardware hardwareObject)
		{
			throw new System.NotImplementedException ();
		}
		#endregion

		#region Dit is beschikbaar om aangeroepen te worden vanuit de tasklayer.
		private void SetHardwareState () {
		}
		#endregion
	
	}

}
