using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Task;

namespace Presentation {
	public class InteractionLoader : MonoBehaviour {
		private UnityAction<System.Object> showInteractiveLoader;

		// Use this for initialization
		void Start () {
			showInteractiveLoader = new UnityAction<System.Object> (ShowInteractiveLoader);
			EventManager.StartListening ("showInteractiveLoader", ShowInteractiveLoader);	
			this.gameObject.SetActive (false);


		}

		void ShowInteractiveLoader (System.Object showOrHide)
		{
			bool response = Convert.ToBoolean(showOrHide);
			this.gameObject.SetActive (response);
		}
	}

}
