using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AssemblyCSharp.BusinessLayer.Domain {
	[Serializable]
	public class Hardware {
		public int id;
		public string tag;
		public string name;
		public int x; 
		public int y; 
		public int z;
		public Hardware() {
		}
	}
}

