using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {
	public int RayDistance = 10;
	private JSONObject json;

	void Start()
	{
		//		WWW www = new WWW ("http://154.16.159.176:3000/service/getallhardware");
		//		StartCoroutine (WaitForRequest (www));
	}

	//	private IEnumerator WaitForRequest(WWW www){
	//		yield return www;
	//		if (www.error == null) {
	//			json = new JSONObject (www.text);
	//		} else{
	//			print ("error" + www.error);
	//		}
	//	}

	void FixedUpdate()
	{

	}
	void Update(){
	}
}
