using UnityEngine;
using VRStandardAssets.Utils;
using Presentation;
using Task;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class InteractionObject : MonoBehaviour
    {
              
        [SerializeField] public VRInteractiveItem m_InteractiveItem;


        private void Awake ()
        {
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
          
        }


        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("Show over state");
          //  m_Renderer.material = m_OverMaterial;
			this.gameObject.GetComponent<Outliner>().SetOutlineOn();
			EventManager.TriggerEvent ("ActivateRecticle", false); 

        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
          // m_Renderer.material = m_NormalMaterial;
			this.gameObject.GetComponent<Outliner>().SetOutlineOff();
			EventManager.TriggerEvent ("DeactivateRecticle", false); 


        }
			
    }

}