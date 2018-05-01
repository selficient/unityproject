using UnityEngine;
using VRStandardAssets.Utils;

namespace Presentation
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class InteractiveDeur : MonoBehaviour
    {
     
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
		[SerializeField] private GameObject gameObject;
		private bool isActive;

        private void Awake ()
        {
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
			Debug.Log ("clicking is now active");
			isActive = true;
        }


        //Handle the Out event
        private void HandleOut()
        {
			Debug.Log ("clicking is now deactive");
			isActive = false;
        }
		/**
		 * Voer hier de open deur animatie uit"
		 * 
		 * */
        private void HandleClick()
        {
			Animator anim = gameObject.GetComponent<Animator> ();
			if (isActive) {
				Debug.Log ("can handle this click");
				gameObject.GetComponent<Animator> ().SetTrigger ("open");

			} else {
				Debug.Log ("can not handle this click");
			}
        }


       
    }

}