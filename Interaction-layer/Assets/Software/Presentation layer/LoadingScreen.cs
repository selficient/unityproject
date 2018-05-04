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
		public GameObject interactiveLoader;
		private UnityAction<System.Object> hideLoader; 
		private UnityAction<System.Object> showInteractiveLoader;

		void Awake() {
			hideLoader = new UnityAction<System.Object> (HideLoader);
			showInteractiveLoader = new UnityAction<System.Object> (ShowInteractiveLoader);


		}
		void OnEnable(){
			EventManager.StartListening ("loading", HideLoader);	
			EventManager.StartListening ("showInteractiveLoader", ShowInteractiveLoader); 

		}
		void OnDisable(){
			EventManager.StopListening ("loading", HideLoader);
			EventManager.StartListening ("showInteractiveLoader", ShowInteractiveLoader); 

		}	

		void HideLoader (System.Object success)
		{
			bool response = Convert.ToBoolean(success);
			successIndicator.SetActive(response);
			errorIndicator.SetActive (!response);
			activityIndicator.SetActive(false);
			StartCoroutine (WaitForHiding());
		}

		void ShowInteractiveLoader (System.Object displayLoader)
		{
			bool show = Convert.ToBoolean (displayLoader);
			if (show) {
				interactiveLoader.SetActive (true);
				StartCoroutine (WaitForHidingInteractiveLoader ());
			} else {
				interactiveLoader.SetActive (true);

			}
		}

		IEnumerator WaitForHidingInteractiveLoader(){
			yield return new WaitForSeconds(2);
			interactiveLoader.SetActive (false);
		}
		IEnumerator WaitForHiding(){
			yield return new WaitForSeconds(2);
			loadingScreen.SetActive (false);

		}
	}

}
