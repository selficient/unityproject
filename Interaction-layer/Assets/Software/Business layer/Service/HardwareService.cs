using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Task;
using Business.Domain;

namespace Business {
	public class HardwareService : IDataService {


		public IEnumerator AreaLoader(string uri)
		{
			UnityWebRequest www = UnityWebRequest.Get (uri);
			yield return www.Send();

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

		public void SaveHardwareState (Hardware hardwareObject)
		{
			throw new System.NotImplementedException ();
		}
	}

}
