using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Task;
using Business.Domain;
using VRStandardAssets.Utils;
using cakeslice;
using VRStandardAssets.Examples;

namespace Presentation {
	/*
	 * Verantwoordelijk voor het bouwen van de GameObjects.
	 * De GameObjects worden vanuit de Business.Domain.Hardware gegenereerd.
	 * @author T.J van der Ende
	 */
	public class HardwareBuilder : MonoBehaviour {
		// bouw de hardware laag op het moment dat de data is ingeladen.
		private UnityAction<System.Object> buildHardwareLayer;
		public GameObject sensorPrefab;
		public GameObject doorPrefab;
		private int interactionLayerId = 14;
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
			try {
				foreach (Hardware hardware in hardwares) {

					if (hardware.type.name == "Sensor") {
						sensorPrefab.gameObject.name = hardware.id;
						var obj = Instantiate (sensorPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
						obj.transform.parent = areaObject.transform;
						obj.transform.localPosition = new Vector3 (hardware.x, hardware.y, hardware.z); // zet relatief tot room

					} else if (hardware.type.name == "Door" || hardware.type.name == "Light") {
						/*doorPrefab.gameObject.name = hardware.id;
					var obj = Instantiate (doorPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
					obj.transform.parent = areaObject.transform;
					obj.transform.localPosition = new Vector3 (hardware.x, hardware.y, hardware.z); // zet relatief tot room
					obj.transform.localEulerAngles = new Vector3(90, 0);*/
						var interactiveElement = GameObject.Find (hardware.name); 

						if (interactiveElement != null) {
							interactiveElement.layer = interactionLayerId; // interaction layer
							interactiveElement.SetActive (false);
							Interactable interactable = null;
							if (hardware.type.name == "Door") {
								RuntimeAnimatorController deurController = (RuntimeAnimatorController)Resources.Load ("Animation/Deur2", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
								if(deurController != null) {
									var animation = interactiveElement.AddComponent<Animator> ();
									animation.runtimeAnimatorController = deurController;
									interactable = new DoorInteraction(animation);

								}else {
									throw new UnityException("Animatie voor deur kon niet gevonden worden"); 
								}

							}
							if(hardware.type.name == "Light") {
								Light lightObject = interactiveElement.GetComponent<Light>();
								interactable = new LampInteraction(lightObject);
							}
							//Material material = (Material)Resources.Load ("Material/SensorFocus", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

							VRInteractiveItem interactiveItem = interactiveElement.AddComponent<VRInteractiveItem> (); 
							// add interactible by gazing
							InteractionObject interactionObject = interactiveElement.AddComponent<InteractionObject>();
							interactionObject.m_InteractiveItem = interactiveItem; // add outline effect.

							interactiveElement.AddComponent<Outliner>(); 


							// add object specific interactions.
							ObjectInteraction interactive = interactiveElement.AddComponent<ObjectInteraction> ();
							interactive.gameObject = interactiveElement;
							interactive.interactionName = hardware.interactions[0].name;
							interactive.hardware = hardware; 
							interactive.m_InteractiveItem = interactiveItem;
							interactive.interactable = interactable;
							interactiveElement.SetActive (true);

						}

					}
				}
			}
			catch(UnityException e) { 
				print ("Errorrr" + e.Message);
			}

		}



	}
}

