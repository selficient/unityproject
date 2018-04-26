using System;
using System.Collections;
using Business.Domain;

namespace Business
{
	public interface IDataService
	{
		IEnumerator AreaLoader(string uri);
		void SaveHardwareState (Hardware hardwareObject);

	}
}

