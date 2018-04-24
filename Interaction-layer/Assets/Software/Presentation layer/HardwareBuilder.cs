using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Task;
using AssemblyCSharp.BusinessLayer.Domain;

namespace Presentation {
	/*
	 * Verantwoordelijk voor het bouwen van de GameObjects.
	 * De GameObjects worden vanuit de Business.Domain.Hardware gegenereerd.
	 */
	public class HardwareBuilder : MonoBehaviour {
		// bouw de hardware laag op het moment dat de data is ingeladen.
		private UnityAction<System.Object> buildHardwareLayer;
		public GameObject hardwareObject;
		void Awake() {

			buildHardwareLayer = new UnityAction<System.Object> (LoadHardwareObjects);
		}
		// Use this for initialization
		void Start () {
			
		}
		// "triggerHardwareBuild" wordt aangeroepen vanuit het DataEvent in de TaskLayer
	
		void OnEnable(){
			EventManager.StartListening ("triggerHardwareBuild", LoadHardwareObjects);	

		}
		void Disable(){
			EventManager.StopListening ("triggerHardwareBuild", LoadHardwareObjects);
		}
		/*
		 * Wordt aangeroepen als de data beschikbaar is (Vanuit de Tasklayer).
		 */
		void LoadHardwareObjects (System.Object hardwareList) {
			print ("Build hardware layer here!");
			List<Hardware> hardwares = hardwareList as List<Hardware>;
			foreach (Hardware hardware in hardwares) {
				hardwareObject.gameObject.name = hardware.name;
				Instantiate (hardwareObject, new Vector3 (hardware.x, hardware.y, hardware.z), Quaternion.identity);
			}
		}

	}
}

