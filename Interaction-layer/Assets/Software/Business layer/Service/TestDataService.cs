using System;
using System.Collections;
using UnityEngine;
using Task;
using System.Collections.Generic;
using Business.Domain;

namespace Business
{
	public class TestDataService : IDataService
	{
		public TestDataService ()
		{
			
		}
		private List<Hardware> GetHardware(){
			List<Hardware> testlist = new List<Hardware> ();
			testlist.Add (new Hardware () {
				id = "1",
				name = "co2meter",
				
			});
			testlist.Add (new Hardware () {
				id = "2",
				name = "luchtvochtigheid",
				
			});
			return testlist;
		}
		#region IDataService implementation

		public IEnumerator AreaLoader (string uri)
		{
			yield return new WaitForSeconds (2);
			HardwareService hardwareService = new HardwareService();
			EventManager.TriggerEvent ("triggerHardwareBuild", GetHardware() as System.Object);
			EventManager.TriggerEvent ("loading", true);

		}


		public IEnumerator SaveHardwareState (System.Object hardwareObject, string uri)
		{
			throw new NotImplementedException ();
		}

		public IEnumerator LoadHardwareDataset (string datasetId, string uri)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

