using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Task;
using AssemblyCSharp.BusinessLayer.Domain;

namespace Business {
	
	public class DataService : MonoBehaviour, IDataService{
		//string uri = "http://localhost:3000/service/getallhardware?apikey=kdfjadslj2xk";

		[SerializeField] private string uri;
		private IDataService serviceImplementation;
		// Use this for initialization

		void Start () {
			serviceImplementation = new TestDataService ();
			StartCoroutine(GetRequest(uri));
		}
		// Update is called once per frame
		void Update () {

		}

		#region IDataService implementation
		public IEnumerator GetRequest (string uri)
		{
			return this.serviceImplementation.GetRequest (uri);
		}
		#endregion
	}

}
