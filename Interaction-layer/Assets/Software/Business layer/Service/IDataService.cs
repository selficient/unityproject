using System;
using System.Collections;
using Business.Domain;

namespace Business
{
	public interface IDataService
	{
		IEnumerator AreaLoader(string uri);
		IEnumerator SaveHardwareState (System.Object hardwareObject, string uri);

	}
}

