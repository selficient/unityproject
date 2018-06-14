using Task;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentation.Dashboard
{
    public class DashboardGazeDetect : MonoBehaviour
    {

        public DashboardGazeDetect()
        {
        }


        void Update()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x < 0.0 || 1.0 < pos.x || pos.y < 0.0 || 1.0 < pos.y)
            {
                EventManager.TriggerEvent("hideDashboard", null);
            }
        }
    }
}