using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task;
using UnityEngine.Events;
using VRStandardAssets.Utils;
using System;

namespace Presentation {
	public class LoadingScreen : MonoBehaviour {
		public GameObject loadingScreen;
		public GameObject activityIndicator;
		public GameObject successIndicator;
		public GameObject errorIndicator;
		private UnityAction<System.Object> hideLoader; 

		void Awake() {
			hideLoader = new UnityAction<System.Object> (HideLoader);
			activityIndicator.SetActive (true);

		}
		void OnEnable(){
			EventManager.StartListening ("loading", HideLoader);	

		}
		void OnDisable(){
			EventManager.StopListening ("loading", HideLoader);

		}	

		void HideLoader (System.Object success)
		{
			bool response = Convert.ToBoolean(success);
			successIndicator.SetActive(response);
			errorIndicator.SetActive (!response);
			loadingScreen.SetActive(false);
		//	StartCoroutine (WaitForHiding());
		}
			


		IEnumerator WaitForHiding(){
			yield return new WaitForSeconds(2);
			loadingScreen.SetActive (false);

		}
	}

}
