using System;

namespace Business.Domain
{
	[Serializable]
	public class Type
	{
		public string name;
		public Type ()
		{
		}
		public override string ToString ()
		{
			return this.name;
		}
	}
}

