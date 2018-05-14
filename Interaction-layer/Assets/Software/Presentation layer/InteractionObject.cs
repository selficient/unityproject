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
		private bool inViewport = false;
		private Camera cam; 
		void Start(){
			GameObject camera = GameObject.Find ("CenterEyeAnchor");
			cam = camera.GetComponent<Camera> ();
		}
        private void Awake ()
        {
			
        }


		/*void Update()
		{
			bool oldViewport = inViewport;
			Transform target = this.gameObject.transform;
			Vector3 screenPoint = cam.GetComponent<Camera>().WorldToViewportPoint(target.position);
			//print (viewPos.x + " " + viewPos.y + " " + viewPos.z);
			// als x en y tussen de 0 en 1 zijn + z is positief zit het object in de viewport van de camera.
			if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1) {
				//print ("Seen " + this.gameObject.name);
				inViewport = true;
			} else {
				inViewport = false;
				//print ("Unseen " + this.gameObject.name);
			}
			updateOutlining (oldViewport);
				

		}*/
		void updateOutlining(bool oldViewport){
			if (oldViewport != inViewport) {
				if (inViewport) {
					this.gameObject.GetComponent<Outliner> ().SetOutlineOn (1);

				} else {
					this.gameObject.GetComponent<Outliner>().SetOutlineOff(1);

				}
			}


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