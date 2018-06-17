using System;
using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    AsyncOperation manager;
    // Use this for initialization
    void Start () {
        StartCoroutine("load");
        StartCoroutine("LoadLevelProgress");
     
    }
    IEnumerator load()
    {
        manager = SceneManager.LoadSceneAsync("test", LoadSceneMode.Single);
        manager.allowSceneActivation = false;

        yield return manager;
    }
    IEnumerator LoadLevelProgress()
    {
        while (!manager.isDone)
        {
            if (manager.progress >= 0.9f)
                break;

            Debug.Log("Loading scene:" + manager.progress);

            yield return null;
        }

        Debug.Log("Loading complete");
        EventManager.TriggerEvent("sceneRendered", manager);
    }

   

}
