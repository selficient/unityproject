using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentation.Dashboard
{
    public class DashboardGazeDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public DashboardGazeDetect()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("testje");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Testjtjtktj");
            this.gameObject.SetActive(false);
        }
    }
}