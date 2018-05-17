using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Presentation.Dashboard;
using Business.Domain;

namespace Presentation {
	public class DashboardInteraction : Interactable {

		GameObject sensor;
		Hardware hardware;
		private DashboardRenderer renderer;
		private GameObject dashboard;
		public DashboardInteraction(GameObject sensor, Hardware hardware){
			this.sensor = sensor;
			this.renderer = new DashboardRenderer ();
		}

		#region Interactable implementation

		public void On ()
		{
			
			Debug.Log ("Show dashboard "+ this.sensor.name); 
			dashboard.SetActive (true);
		}

		public void Off ()
		{
			Debug.Log ("Hide dashboard " + this.sensor.name); 
			dashboard.SetActive (false);
		}

		public void Init ()
		{
			Debug.Log ("initialize sensor dashboard");
			//throw new System.NotImplementedException ();
			dashboard = this.renderer.InitializeDashboard(this.sensor, this.hardware);
			dashboard.SetActive (false);
		}

		#endregion


	}

}
