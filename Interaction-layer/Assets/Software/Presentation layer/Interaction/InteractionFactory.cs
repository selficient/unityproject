using System;
using Presentation;
using UnityEngine;
using Business.Domain;

namespace Presentation
{
	public class InteractionFactory
	{
		public static Interactable GetInteraction(GameObject interactiveElement, Hardware hardware){
			Interactable interactable = null;
			if (hardware.type.name == "Door") {
				RuntimeAnimatorController deurController = (RuntimeAnimatorController)Resources.Load ("Animation/"+hardware.name, typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
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
			if (hardware.type.name == "Sensor") {
				interactable = new DashboardInteraction (interactiveElement, hardware);
			}
			return interactable;
		}
	}
}

