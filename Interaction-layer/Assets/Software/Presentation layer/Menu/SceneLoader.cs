using System;
using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    UnityAction<System.Object> unityAction;
    AsyncOperation manager;
    // Use this for initialization
    void Start () {
        unityAction = new UnityAction<System.Object>(StartRendering);
        EventManager.StartListening("startSceneRendering", StartRendering);
        manager = SceneManager.LoadSceneAsync("test", LoadSceneMode.Single);
        manager.allowSceneActivation = false;
    }

    private void StartRendering(object arg0)
    {
        manager.allowSceneActivation = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
