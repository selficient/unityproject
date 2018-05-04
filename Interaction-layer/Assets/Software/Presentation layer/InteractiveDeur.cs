using UnityEngine;
using VRStandardAssets.Utils;
using Task;
using Business.Domain;
using System.Collections.Generic;

namespace Presentation
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.

    public class InteractiveDeur : MonoBehaviour
    {
     
		[SerializeField] public VRInteractiveItem m_InteractiveItem;
		[SerializeField] public GameObject gameObject;
		public Hardware hardware;
		private bool isActive;
		private Animator anim;
		string interactionName = "deur";
		Interaction interaction;

		void Start(){
			//this.anim = gameObject.GetComponent<Animator> (); 
		//	this.anim.transform.position = gameObject.transform.position;
		}
        private void Awake ()
        {
			interaction = hardware.interactions.Find (x => x.name.Equals(interactionName));
			anim = gameObject.GetComponent<Animator> ();
			anim.Play ("Idle");
			this.UpdateState (interaction);
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
			print("clicking is now active");
			isActive = true;
        }


        //Handle the Out event
        private void HandleOut()
        {
			print("clicking is now deactive");
			isActive = false;
        }
		/**
		 * Voer hier de open deur animatie uit"
		 * 
		 * */
        private void HandleClick()
        {
			

			if (isActive) {
				Debug.Log ("can handle this click door status:" + hardware.state);
				SaveState (interaction);
			}

        }
		private void UpdateState(Interaction interaction){
			if (interaction != null) {
				if (hardware.state.code.Equals("0")) { // dicht dus
					anim.Play ("open");
				} else if (hardware.state.code.Equals("1")) {
					anim.Play ("close");
				}

			}
		}
		private void SaveState(Interaction interaction) {
			if (hardware.state.code.Equals("0")) {
				hardware.state.code = "1";
				UpdateState (interaction);
			} else if(hardware.state.code.Equals("1")) {
				hardware.state.code = "0";
				UpdateState (interaction);
			}
			EventManager.TriggerEvent ("updateHardwareState", new KeyValuePair<Interaction, Hardware>(interaction, hardware));

		}


       
    }

}