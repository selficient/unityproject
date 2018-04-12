using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Per hardware object wordt een "collision event" gemaakt:
 * Active: (raycast heeft dit object gezien) activeer het object met een kleur en er kan op geklikt worden
 * Deactive: (raycast is er weer vanaf gegaan) deactiveer het object door naar oorspronkelijke staat terug te brengen.
 * @author T.J van der Ende
*/
public class CollisionEvent : MonoBehaviour {
	private UnityAction onCollision;
	private UnityAction onDecollision;
	// haal gameobject op waar dit event aan gelinkt is
	private Renderer hardware = null;
	private string hardwareName = "undefined";
	void Awake() {
		hardware =  GetComponent<Renderer>();
		hardwareName = hardware.gameObject.name;
		onCollision = new UnityAction (Seen);
		onDecollision = new UnityAction (UnSeen);
	}
	void OnEnable(){
		EventManager.StartListening ("activate-"+hardwareName, Seen);	
		EventManager.StartListening ("deactivate-"+hardwareName, UnSeen);	

	}
	void Disable(){
		EventManager.StopListening ("activate-"+hardwareName, Seen);
		EventManager.StopListening ("deactivate-"+hardwareName, UnSeen);

	}
	void Seen(){
		Debug.Log ("Seen " + hardwareName);
		hardware.material.color = Color.red;

	}
	void UnSeen(){
		Debug.Log ("Unseen " + hardwareName);
		hardware.material.color = Color.white;
	}
}
