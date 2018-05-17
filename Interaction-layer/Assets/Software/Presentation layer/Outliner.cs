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
			if (this.gameObject.transform.childCount > 0) {
				foreach (Transform child in this.gameObject.transform) {
					this.outlineScripts.Add (child.gameObject.AddComponent<Outline> ());
				}
			} else {
				this.outlineScripts.Add (gameObject.AddComponent<Outline> ());

			}
		}

		public void SetOutlineOn(int color = 0){
			try {
				outlineScripts.ForEach (x => {x.color = color; x.eraseRenderer = false; });
			} catch (NullReferenceException e){
				Debug.Log ("Voeg een mesh renderer toe aan dit object, om gebruik te maken van de outline functionaliteit: " + this.gameObject.name); 
			}

		}
		public void SetOutlineOff(int color = 0){
			try {
				outlineScripts.ForEach (x => {x.eraseRenderer = true; x.color = color; });
			} catch (NullReferenceException e){
				Debug.Log ("Voeg een mesh renderer toe aan dit object, om gebruik te maken van de outline functionaliteit: " + this.gameObject.name); 

			}

		}
	}


}
				 
