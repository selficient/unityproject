using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataService : MonoBehaviour {
    string uri = "http://localhost:3000/service/getallhardware?apikey=kdfjadslj2xk";

    // Use this for initialization
    void Start () {
        StartCoroutine(GetRequest(uri));

        
    }
    IEnumerator GetRequest(string uri)
    {
		UnityWebRequest www = UnityWebRequest.Get (uri);
		yield return www.Send();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log (www.downloadHandler.text);
		}
    }
    // Update is called once per frame
    void Update () {
		
	}
}
