using System;
using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;
using UnityEngine.Events;

public class AutoCloseDashboard : MonoBehaviour {
    // Use this for initialization
    void Start () {

    }


    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            EventManager.TriggerEvent("hideDashboard", null); // sluit alle dashboards
        }
	}
}
