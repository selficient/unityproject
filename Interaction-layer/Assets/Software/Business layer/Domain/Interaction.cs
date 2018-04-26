using System;
using System.Collections.Generic;

namespace Business.Domain
{	
	[Serializable]
	public class Interaction
	{
		public string name;
		public List<State> actions;
		public Interaction ()
		{
		}
		public override string ToString ()
		{
			return string.Format ("[Interaction]");
		}
	}
}

