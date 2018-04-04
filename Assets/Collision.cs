using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
	JSONObject jaiys_son;


	// Use this for initialization
	void Start () {
		WWW www = new WWW ("http://154.16.159.176:3000/service/getallhardware");
		StartCoroutine (WaitForRequest (www));
	}

	private IEnumerator WaitForRequest(WWW www){
		yield return www;
		if (www.error == null) {
			jaiys_son = new JSONObject (www.text);
		} else {
			print ("error" + www.error);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("deur")) {
			print(jaiys_son);
		}
	}
}
