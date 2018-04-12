using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour {
    GameObject seenSensor;
    // Use this for initialization
    void Start () {
        Debug.Log("raycast test");
	}
	
	/*
	 * Per frame wordt er gekeken of een hit is met een object.
	 * Vervolgens roept deze functie de EventManager aan en die zorgt ervoor dat het GameObject bij wie de event hoort triggerd.
	*/
	void Update () {
        RaycastHit seen;
        Ray rayForward = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(rayForward, out seen))
        {
            if (seen.collider.tag == "Sensor") //in the editor, tag anything you want to interact with and use it here
            {
               /* print(transform);
                print(seen.collider);*/
                seenSensor = seen.collider.gameObject;
				EventManager.TriggerEvent ("activate-"+seenSensor.GetComponent<Renderer> ().gameObject.name);
            } 

        } else
        {
            if(seenSensor != null)
            {
				EventManager.TriggerEvent ("deactivate-"+seenSensor.GetComponent<Renderer> ().gameObject.name);
                //seenSensor.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        Debug.DrawRay(transform.position, transform.forward, Color.green); //unless you allow debug to be seen in game, this will only be viewable in the scene view
    }
}
