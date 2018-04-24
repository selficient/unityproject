using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.BusinessLayer.Domain;
using UnityEngine.Networking;
using Task;

namespace Business {
	public class HardwareService : IDataService {

		public List<Hardware> GetHardware(){
			List<Hardware> testlist = new List<Hardware> ();
			testlist.Add (new Hardware () {
				id = 1,
				name = "co2meter",
				tag = "sensor",
				x = 35,
				y = 25, 
				z = 8
			});
			testlist.Add (new Hardware () {
				id = 2,
				name = "luchtvochtigheid",
				tag = "sensor",
				x = 40,
				y = 25,
				z = 8
			});
			return testlist;
		}
		public IEnumerator GetRequest(string uri)
		{
			UnityWebRequest www = UnityWebRequest.Get (uri);
			yield return www.Send();

			if(www.isNetworkError || www.isHttpError) {
				EventManager.TriggerEvent ("loading", false);

			}
			else {
				Hardware[] hardware = JsonHelper.FromJson<Hardware> (www.downloadHandler.text);
				EventManager.TriggerEvent ("loading", true);
			}
		}
	}

}
