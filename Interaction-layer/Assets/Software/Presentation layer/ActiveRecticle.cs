using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Task;

public class ActiveRecticle : MonoBehaviour {
	private UnityAction<System.Object> activateRecticle; 
	private UnityAction<System.Object> deactivateRecticle; 

	[SerializeField] public Image imageObject;
	private Color startColor;
	// Use this for initialization
	void Start () {
		if (imageObject) {
			startColor = imageObject.color;
		} else {
			startColor = new Color (26F, 128F, 227F);
		}
	}
	void OnEnable(){
		EventManager.StartListening ("ActivateRecticle", ActivateRecticle);	
		EventManager.StartListening ("DeactivateRecticle", DeactivateRecticle);	

	}
	void OnDisable(){
		EventManager.StopListening ("ActivateRecticle", ActivateRecticle);
		EventManager.StartListening ("DeactivateRecticle", DeactivateRecticle);	

	}	
	// Update is called once per frame
	void Update () {
		
	}

	void ActivateRecticle (System.Object hardwareObject)
	{
		this.imageObject.color = Color.red;
	}

	void DeactivateRecticle (System.Object hardwareObject)
	{
		this.imageObject.color = this.startColor;
	}
}
