using System;
using UnityEngine;
using cakeslice;
using System.Collections.Generic;
using UnityEngine.Events;
using Task;

namespace Presentation
{
	/**
	 * Maakt gebruik van Outline.cs.
	 * Initialiseert deze scripts en zorgt voor functionaliteit om deze aan en uit te kunnen zetten voor de meegegven GameObject.
	 * Dit script zou er voor moeten zorgen dat de outline.cs op iedere child-component en parent wordt gezet
	 * @author T.J van der Ende
	 */

	public class Outliner : MonoBehaviour
	{
		private List<Outline> outlineScripts;

		public Outliner ()
		{
		}
		void Start(){
			InitOutlineEffect ();
			SetOutlineOff (); 
		}
		private void InitOutlineEffect() {
			this.outlineScripts = new List<Outline> ();
			this.outlineScripts.Add (gameObject.AddComponent<Outline> ());
			if (this.gameObject.transform.childCount > 0) {
				foreach (Transform child in this.gameObject.transform) {
					this.outlineScripts.Add (child.gameObject.AddComponent<Outline> ());
				}
			}
		}

		public void SetOutlineOn(){
			try {
				outlineScripts.ForEach (x => x.enabled = true);
			} catch (NullReferenceException e){
				Debug.Log ("Voeg een mesh renderer toe aan dit object, om gebruik te maken van de outline functionaliteit"); 
			}

		}
		public void SetOutlineOff(){
			try {
				outlineScripts.ForEach (x => x.enabled = false);
			} catch (NullReferenceException e){
				Debug.Log ("Voeg een mesh renderer toe aan dit object, om gebruik te maken van de outline functionaliteit"); 

			}

		}
	}


}
				 
