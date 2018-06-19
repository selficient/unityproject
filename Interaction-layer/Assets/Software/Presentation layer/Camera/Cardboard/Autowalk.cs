/*
This script was developed by Jupp Otto. It's free to use and there are no restrictions in modification.
If there are any questions you can send me an Email: juppotto3@gmail.com
This script moves your player automatically in the direction he is looking at. You can 
activate the autowalk function by pull the cardboard trigger, by define a threshold angle 
or combine both by selecting both of these options.
The threshold is an value in degree between 0° and 90°. So for example the threshold is 
30°, the player will move when he is looking 31° down to the bottom and he will not move 
when the player is looking 29° down to the bottom. This script can easally be configured
in the Unity Inspector. 
How to get started with this script?: 
0. haven't the Google VR SDK yet, follow this guide https://developers.google.com/vr/unity/get-started
1. Import the exampple package downloaded in step 0 (GoogleVRForUnity.unitypackage).
2. Load the GVRDemo scene.
3. Attach a Rigidbody to the "Player" GameObject.
4. Freeze X, Y and Z Rotation of the Rgidbody in the inspector. 
5. Attach a Capsule Collider to the "Player" GameObject.
6. Attach the Autowalk script to the "Player" GameObject.
7. Configure the Script in the insector for example: 
      - walk when triggered   = true 
      - speed                 = 3  
8. Make sure your ground have a collider on it. (Otherwise you will fall through it)
9. Start the scene and have fun! 
*/

using UnityEngine;
using System.Collections;
using Task;
using UnityEngine.Events;
using System;

public class Autowalk : MonoBehaviour
{
    private const int RIGHT_ANGLE = 90;
    private UnityAction<System.Object> disableViewClick;

    // This variable determinates if the player will move or not 
    private bool isWalking = false;

    public Transform mainCamera;
	CharacterController controller;
    //This is the variable for the player speed
    [Tooltip("With this speed the player will move.")]
    public float speed;

    [Tooltip("Activate this checkbox if the player shall move when the Cardboard trigger is pulled.")]
    public bool walkWhenTriggered;

    private bool CanWalk = true;
	//#if !UNITY_EDITOR

    void Start()
    {
		controller = this.GetComponent<CharacterController> ();
        EventManager.StartListening("disableWalking", DisableWalking);

    }

    private void DisableWalking(object arg0)
    {
        bool test = Convert.ToBoolean(arg0);
        CanWalk = test;
    }

    void Update()
    {
        // Walk when the Cardboard Trigger is used 
        if (walkWhenTriggered && !isWalking && Input.GetButtonDown("Fire1") && CanWalk)
        {
            isWalking = true;
        }
        else if (walkWhenTriggered && isWalking && Input.GetButtonDown("Fire1"))
        {
            isWalking = false;
        }

        if (isWalking)
        {
            Vector3 direction = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized * speed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
            //transform.Translate(rotation * direction);
			controller.Move(rotation * direction);
        }

    }
	//#endif
}
