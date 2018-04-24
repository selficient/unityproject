using System;
using System.Collections;
using UnityEngine;
using Task;

namespace Business
{
	public class TestDataService : IDataService
	{
		public TestDataService ()
		{
			
		}
	
		#region IDataService implementation

		public IEnumerator GetRequest (string uri)
		{
			yield return new WaitForSeconds (2);
			HardwareService hardwareService = new HardwareService();
			EventManager.TriggerEvent ("triggerHardwareBuild", hardwareService.GetHardware() as System.Object);
			EventManager.TriggerEvent ("loading", true);

		}

		#endregion
	}
}

