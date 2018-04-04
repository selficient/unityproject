using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	[System.Serializable]
	public class Hardware
	{
		public string id;
		public string _id;
		public string name;
		public List<Interaction> interactions;
		public List<State> state;

	}
}

