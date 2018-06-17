using System.Collections;
using System.Collections.Generic;
using System;

namespace Business.Domain {
	[Serializable]
	public class Hardware {
		public string id;
 		public string name;

		public List<Interaction> interactions;
		public State state;
		public Type type;

		public Hardware() {
		}
		public override string ToString ()
		{
			String debug =  "Name: " + name + " id " + id + " \n Type: " + type + " \n ";
			foreach (Interaction interaction in interactions) {
				debug +="Interactions: name: " + interaction.name;
				foreach (State action in interaction.actions) {
					debug += " Interaction action: code: " + action.code + " Omschrijving: " + action.description;
				}
			}
			return debug;
		}
	}
}

