using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class ARTrackingController : MonoBehaviour {



	/// <summary>
	/// True if the app is in the process of quitting due to an ARCore connection error, otherwise false.
	/// </summary>
	private bool m_IsQuitting = false;



	public Text m_camPoseText;

	public GameObject m_CameraParent;

	public float m_XZScaleFactor = 10;

	public float m_YScaleFactor = 2;

	public bool m_showPoseData = true;

	private bool trackingStarted = false;

	private Vector3 m_prevARPosePosition;

	/// <summary>
	/// Check and update the application lifecycle.
	/// </summary>
	private void _UpdateApplicationLifecycle()
	{
		// Exit the app when the 'back' button is pressed.
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

		// Only allow the screen to sleep when not tracking.
		if (Session.Status != SessionStatus.Tracking)
		{
			const int lostTrackingSleepTimeout = 15;
			Screen.sleepTimeout = lostTrackingSleepTimeout;
			//m_camPoseText.text = "Lost tracking, wait ...";
		}
		else
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			//m_camPoseText.text = "";
		}

		if (m_IsQuitting)
		{
			return;
		}

		// Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
		if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
		{
			_ShowAndroidToastMessage("Camera permission is needed to run this application.");
			m_IsQuitting = true;
			Invoke("_DoQuit", 0.5f);
		}
		else if (Session.Status.IsError())
		{
			_ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
			m_IsQuitting = true;
			Invoke("_DoQuit", 0.5f);
		}
	}

	/// <summary>
	/// Actually quit the application.
	/// </summary>
	private void _DoQuit()
	{
		Application.Quit();
	}

	/// <summary>
	/// Show an Android toast message.
	/// </summary>
	/// <param name="message">Message string to show in the toast.</param>
	private void _ShowAndroidToastMessage(string message)
	{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		if (unityActivity != null)
		{
			AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
			unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
				{
					AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
						message, 0);
					toastObject.Call("show");
				}));
		}
	}



	/****** added ****/
	public void Update (){
		_UpdateApplicationLifecycle ();
	
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Vector3 currentARPosition = Frame.Pose.position;

		if (!trackingStarted) {

			trackingStarted = true;

			m_prevARPosePosition = Frame.Pose.position;
		}

		//Remember the previous position so we can apply deltas

		Vector3 deltaPosition = currentARPosition - m_prevARPosePosition;

		m_prevARPosePosition = currentARPosition;

		if (m_CameraParent != null) {

			Vector3 scaledTranslation = new Vector3 (m_XZScaleFactor * deltaPosition.x, m_YScaleFactor * deltaPosition.y, m_XZScaleFactor * deltaPosition.z);

			m_CameraParent.transform.Translate (scaledTranslation);

			if (m_showPoseData) {

				m_camPoseText.text = "Pose = " + currentARPosition + "\n" + m_CameraParent.transform.position;
			}
		}

	}

}
