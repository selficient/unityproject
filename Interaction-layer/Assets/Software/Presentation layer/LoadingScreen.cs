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
	//	public GameObject activityIndicator;
		public GameObject successIndicator;
		public GameObject errorIndicator;
        public GameObject textIndicator;
		private UnityAction<System.Object> hideLoader; 

		void Awake() {
			hideLoader = new UnityAction<System.Object> (HideLoader);
            
		//	activityIndicator.SetActive (true);

		}
		void OnEnable(){
			EventManager.StartListening ("loading", HideLoader);	

		}
		void OnDisable(){
			EventManager.StopListening ("loading", HideLoader);

		}	

		void HideLoader (System.Object success)
		{
            Debug.Log("succes loading shizzle");
			bool response = Convert.ToBoolean(success);
            //errorIndicator.SetActive (!response);
            Debug.Log(response);
            StartCoroutine(WaitForResponse(response));
            StartCoroutine(WaitForHiding());
		//	StartCoroutine (WaitForHiding());
		}
			


		IEnumerator WaitForHiding(){
			yield return new WaitForSeconds(3);
			loadingScreen.SetActive (false);

		}
        IEnumerator WaitForResponse(bool response)
        {
            yield return new WaitForSeconds(2);
            successIndicator.SetActive(response);

        }
    }

}
