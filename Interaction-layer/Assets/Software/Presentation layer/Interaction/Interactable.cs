using System;

namespace Presentation
{
	public interface Interactable
	{
		void On();
		void Off();
		void Init();

		bool WantsUpdate (); // als de state niet opgeslagen hoeft te worden, kan dit hier worden aangegeven. (Via de implementatie).
	}
}

