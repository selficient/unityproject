using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Task;
using Business.Domain;

namespace Presentation {
	/*
	 * Verantwoordelijk voor het bouwen van de GameObjects.
	 * De GameObjects worden vanuit de Business.Domain.Hardware gegenereerd.
	 */
	public class HardwareBuilder : MonoBehaviour {
		// bouw de hardware laag op het moment dat de data is ingeladen.
		private UnityAction<System.Object> buildHardwareLayer;
		public GameObject sensorPrefab;
		void Awake() {

			buildHardwareLayer = new UnityAction<System.Object> (BuildArea);
		}
		// Use this for initialization
		void Start () {
			
		}
		// "triggerHardwareBuild" wordt aangeroepen vanuit het DataEvent in de TaskLayer
	
		void OnEnable(){
			EventManager.StartListening ("triggerHardwareBuild", BuildArea);	

		}
		void Disable(){
			EventManager.StopListening ("triggerHardwareBuild", BuildArea);
		}
		/*
		 * Wordt aangeroepen als de data beschikbaar is (Vanuit de Tasklayer).
		 * Initieert de posities van de prefabs.
		 * Kijkt voor het type om welk prefab te initieren.
		 */
		void BuildArea (System.Object area) { // TODO: Support voor andere dingen dan Sensors toevoegen.
			print ("Build hardware layer here!");
			Area newArea = area as Area;
			Hardware[] hardwares = newArea.hardwareList;
			GameObject areaObject = GameObject.Find (newArea.name);
			foreach (Hardware hardware in hardwares) {
				sensorPrefab.gameObject.name = hardware.id;
				print (hardware.type.name);
				if (hardware.type.name == "Sensor") {
					var obj = Instantiate (sensorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
					obj.transform.parent = areaObject.transform;
					obj.transform.localPosition = new Vector3 (hardware.x, hardware.y, hardware.z); // zet relatief tot room
				}
			}
		}

	}
}

