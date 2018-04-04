using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class Player_Interactable : MonoBehaviour {
	public float RayDistance = 25;
	//154.16.159.176
	public string BaseUrl ="http://154.16.159.176:3000/service/";
	private string ApiKey = "2842034248810fsfhusf009412";
	private HardwareList hardwareList;
	private bool doOnceLampje = false; // request before player can interact with lampje
	private bool doOnceVoordeur = false; // request before player can interact with voordeur
	private bool isPressed = false; //
	private bool requestDone = true; //request is done
	private bool doorOpen = false; // frontdoor is open

	// Use this for initialization
	void Start () {
		GameObject.FindWithTag ("deur1_right").GetComponent<Animator> ().Play("idle"); 
		FetchHardware();
	}
	
	// Update is called once per frame
	void Update () {
//		if (doOnceLampje) {
//			UpdateLampje ();
//			doOnceLampje = false;
//		}
//		if (doOnceVoordeur) {
//			UpdateVoordeur ();
//			doOnceVoordeur = false;
//		}
	}

	void FixedUpdate(){
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit rayhit = new RaycastHit ();
		if((Input.GetKeyUp(KeyCode.LeftShift)||Input.GetButton("Fire1")) && !isPressed && requestDone){
			if(Physics.Raycast(transform.position, transform.forward, out rayhit, RayDistance)){
				
				//Zet lampje aan
				if (rayhit.collider.CompareTag ("knopje_lampje")) {
					requestDone = false;
					SetHardwareState (hardwareList.hardware.Find(h => h.name.Equals("Lampje")),"light");
				}

				//Deur1_right open
				if (rayhit.collider.CompareTag ("deur1_right") && !GameObject.FindWithTag ("deur1_right").GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("open") &&
					!GameObject.FindWithTag ("deur1_right").GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("close")) {
					requestDone = false;
					SetHardwareState (hardwareList.hardware.Find(h => h.name.Equals("Voordeur")), "deur");
				}
				print (rayhit.collider.name);
			}
			isPressed = true;
		}
		if (!Input.GetButton ("Fire1") && !(Input.GetKeyUp(KeyCode.LeftShift))) {
			isPressed = false; 
		}
	}
		

/// <summary>
/// Webdingen
/// </summary>
/// <returns>The for request.</returns>
/// <param name="www">Www.</param>

	private IEnumerator WaitForRequest(WWW www, Action<string> callback){
		yield return www;
		// check for errors
		if (www.error == null)
		{
			if (callback != null) {
				callback (www.text);
			}
		} else {
			Debug.Log("WWW Error: "+ www.error+ "\n huis is gehadicapped");
		}
	}
		
	private void FetchHardware(){
		string url = BaseUrl + "getallhardware?apikey=" + ApiKey;
		try {
			WWW www = new WWW (url);
			Action<string> parser = body => {
				try {
					hardwareList = (HardwareList) JsonUtility.FromJson(body, typeof(HardwareList));
					requestDone = true;

					string s = "";
					foreach(Hardware h in hardwareList.hardware){
						s += ( h.name + ", ");
					}
					print("-> List of hardware updated: \n" +s);
				}
				catch(Exception e){
					print (e);
				}
				UpdateLampje();
				UpdateVoordeur();
			};
			StartCoroutine (WaitForRequest (www, parser));

		}
		catch(Exception e) {
			print ("www error: "+ e);
		}
			
	}

	private void UpdateLampje() {
		State stateLampje = (hardwareList.hardware.Find(h => h.name.Equals("Lampje")).state.Find(s => s.name.Equals("light")));
		if (stateLampje.state.Equals("0")) {
			GameObject.FindWithTag ("lampje_woonkamer").GetComponent<Light> ().intensity = 0.0f;
		}
		if (stateLampje.state.Equals("1")) {
			GameObject.FindWithTag ("lampje_woonkamer").GetComponent<Light> ().intensity = 20.0f;
		}
	}

	private void UpdateVoordeur(){
		State stateVoordeur = (hardwareList.hardware.Find(h => h.name.Equals("Voordeur")).state.Find(s => s.name.Equals("deur")));
		print (stateVoordeur.state);
		if (stateVoordeur.state.Equals ("0")) {
			GameObject.FindWithTag ("deur1_right").GetComponent<Animator> ().SetTrigger ("close");

		}
		else if (stateVoordeur.state.Equals ("1")) {
			GameObject.FindWithTag ("deur1_right").GetComponent<Animator> ().SetTrigger ("open");
		}

	}

	/// <summary>
	/// SetState
	/// </summary>
	private void SetHardwareState(Hardware hw, string interactionTrigger){
		string url = BaseUrl + "updatestate?apikey=" + ApiKey;
		State currentState = hw.state.Find(s => s.name.Equals(interactionTrigger));
		Interaction interaction = hw.interactions.Find(i => i.name.Equals(interactionTrigger));

		//NOTE: Currently only toggle actions, the system does not support more than 2 actions
		//var action = interaction.actions.Find(a => !a.code.Equals(currentState.state));
		var currentIndex = interaction.actions.FindIndex(a => a.code.Equals(currentState.state));
		var index = (currentIndex == interaction.actions.Count - 1) ? 0 : currentIndex + 1;
		var action = interaction.actions.Find(a => a.code.Equals(index.ToString()));
		//Doe dingen 

		WWWForm form = new WWWForm ();
		form.AddField ("name", hw.name);
		form.AddField ("interaction", interactionTrigger);
		form.AddField ("state", action.description);
		WWW postRequest = new WWW (url, form);
		Action<string> callback = body => {
			FetchHardware ();
		};
		StartCoroutine (WaitForRequest (postRequest, callback));
	}
}



///////////////////
/// Random ///
/// /////////////
/// //		Vector3 fwd = transform.TransformDirection(Vector3.forward);
//		if (Input.GetKeyUp (KeyCode.X)) {
//			RaycastHit hit;
//			if (Physics.Raycast (transform.position, fwd, out hit, RayDistance)) {
//				print (hit.collider.name);
//				if (hit.collider.CompareTag ("knopje_lampje")) {
//					if(GameObject.FindWithTag("lampje_woonkamer").GetComponent<Light>().intensity==20.0F){ 
//						GameObject.FindWithTag ("lampje_woonkamer").GetComponent<Light>().intensity = 0.0F;
//					} 
//					else GameObject.FindWithTag ("lampje_woonkamer").GetComponent<Light>().intensity = 20.0F;
//					print (GameObject.FindWithTag ("lampje_woonkamer").GetComponent<Light>().intensity.ToString());
//				} else
//					print ("Dit is geen lampje");
//			}
//		}
///////////////////
/// Dingen///
/// ///////////////
/// OSSL -> url getallhardware
/// ?apikey=28420342458810fsfhusf009412
/// 