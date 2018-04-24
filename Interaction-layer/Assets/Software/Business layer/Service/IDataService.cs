using System;
using System.Collections;

namespace Business
{
	public interface IDataService
	{
		IEnumerator GetRequest(string uri);
	}
}

