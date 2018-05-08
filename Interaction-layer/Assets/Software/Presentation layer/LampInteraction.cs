using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Task;
using Business.Domain;
using VRStandardAssets.Utils;

namespace Presentation
{
	/*
	 * Verantwoordelijk voor het bouwen van de GameObjects.
	 * De GameObjects worden vanuit de Business.Domain.Hardware gegenereerd.
	 */
	class LampInteraction : Interactable
	{
		Light light;
		public LampInteraction (Light light){
			this.light = light;
		}
		#region Interactable implementation
		public void On ()
		{
			light.intensity = 2;
		}
		public void Off ()
		{
			light.intensity = 0;
		}
		public void Init ()
		{
			light.intensity = 2;
		}
		#endregion
	}

}

