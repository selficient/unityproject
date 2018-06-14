using UnityEngine;
using VRStandardAssets.Utils;
using Task;
using Business.Domain;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Presentation
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.

	public class ObjectInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
     
		//[SerializeField] public VRInteractiveItem m_InteractiveItem;
		[SerializeField] public GameObject gameObject;
		[SerializeField] public Interactable interactable;

		public Hardware hardware;
		private bool isActive;
		private Animator anim;
		public string interactionName = "deur";
		Interaction interaction;

		void Start(){
			//this.anim = gameObject.GetComponent<Animator> (); 
		//	this.anim.transform.position = gameObject.transform.position;
		}
        private void Awake ()
        {
			interaction = hardware.interactions.Find (x => x.name.Equals(interactionName));
			//anim = gameObject.GetComponent<Animator> ();
			this.interactable.Init();
			this.UpdateState (interaction);
        }

		#region IPointerEnterHandler implementation
		public void OnPointerEnter (PointerEventData eventData)
		{
			this.HandleOver ();
		}
		#endregion

		#region IPointerExitHandler implementation

		public void OnPointerExit (PointerEventData eventData)
		{
			this.HandleOut ();
		}

		#endregion

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			this.HandleClick ();
		}

		#endregion

       /* private void OnEnable()
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
        }*/


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
			

			//if (isActive) {
				Debug.Log ("can handle this click door status:" + hardware.state);
				SaveState (interaction);
			//}

        }
		private void UpdateState(Interaction interaction){
			if (interaction != null) {
				if (hardware.state.code.Equals("0")) { // dicht dus
					this.interactable.On();
				} else if (hardware.state.code.Equals("1")) {
					this.interactable.Off ();
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
			if (this.interactable.WantsUpdate()) { // wanneer er geen update gewenst is , kan dit op false worden gezet.
				EventManager.TriggerEvent ("updateHardwareState", new KeyValuePair<Interaction, Hardware>(interaction, hardware));
			}

		}


       
    }

}