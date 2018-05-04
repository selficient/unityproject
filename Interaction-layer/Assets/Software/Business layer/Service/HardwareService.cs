﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Task;
using Business.Domain;
using UnityEngine.Events;

namespace Business {
	public class HardwareService : IDataService {



		public IEnumerator AreaLoader(string uri)
		{
			UnityWebRequest www = UnityWebRequest.Get (uri);
			yield return www.SendWebRequest();

			if(www.isNetworkError || www.isHttpError) {
				EventManager.TriggerEvent ("loading", false);

			}
			else {
				Hardware[] hardware = JsonHelper.FromJson<Hardware> (www.downloadHandler.text);
				Debug.Log (www.downloadHandler.text);
				Area area = new Area () { name = "LivingRoom", x = 25, y = 25, z = 10, hardwareList = hardware };
			
				EventManager.TriggerEvent ("triggerHardwareBuild", area as System.Object);
				EventManager.TriggerEvent ("loading", true);
			}
		}

		public IEnumerator SaveHardwareState (System.Object hardwareObject, string uri)
		{
			KeyValuePair<Interaction, Hardware> keyvalue = (KeyValuePair<Interaction, Hardware>) hardwareObject;
			WWWForm form = new WWWForm ();
			form.AddField ("name", keyvalue.Value.name);
			form.AddField ("interaction", keyvalue.Key.name);
			form.AddField ("state", keyvalue.Value.state.code);
			UnityWebRequest www = UnityWebRequest.Post (uri, form);
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError) {
				//EventManager.TriggerEvent ("showInteractiveLoader", false);
				Debug.Log(www.downloadHandler.text);
			} else {
				Debug.Log (www.downloadHandler.text);
			}
		}
	}

}
