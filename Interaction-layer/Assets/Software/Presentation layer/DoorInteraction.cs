using System;
using UnityEngine;

namespace Presentation
{
	public class DoorInteraction : Interactable
	{
		Animator anim;
		public DoorInteraction (Animator anim)
		{
			this.anim = anim;
		}

		#region Interactable implementation

		public void Init ()
		{
			anim.Play ("Idle");
		}

		public void On ()
		{
			anim.Play ("open");
		}

		public void Off ()
		{
			anim.Play ("close");
		}

		#endregion
	}
}

