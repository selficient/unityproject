using System;
using UnityEngine;
using Business.Domain;
using UnityEngine.UI;

/*
 * Renderer die automatisch dashboard genereert.
 * @author T.J van der Ende
 */
namespace Presentation.Dashboard
{
	public class DashboardRenderer : MonoBehaviour
	{

		public GameObject InitializeDashboard(GameObject hardwareObject, Hardware hardware){
			
			Material renderOnTop = Resources.Load("Material/DashboardRenderOnTop", typeof(Material)) as Material;
			Sprite backgroundColor = Resources.Load ("Textures/DashboardBackground", typeof(Sprite)) as Sprite;
			if (renderOnTop == null) {
				Debug.Log ("Kon dashboard material niet toewijzen, kijk of je dit kunt oplossen");
			}


			GameObject newCanvas = new GameObject("Canvas");
			newCanvas.name = "dasbhoard-"+hardwareObject.name;
			Canvas c = newCanvas.AddComponent<Canvas>();
			c.renderMode = RenderMode.WorldSpace;
			newCanvas.AddComponent<CanvasScaler> ().dynamicPixelsPerUnit = 1.75f;
			newCanvas.AddComponent<GraphicRaycaster>();
			//RecalculateCanvasPosition (newCanvas, hardwareObject);

			GameObject dataPanel = RenderPanel (renderOnTop, newCanvas.name + "-data", backgroundColor);
			GameObject infoPanel = RenderPanel (renderOnTop, newCanvas.name + "-info", backgroundColor);
			VerticalLayoutGroup vGroup = infoPanel.AddComponent<VerticalLayoutGroup> ();
			VerticalLayoutGroup vGroupData = dataPanel.AddComponent<VerticalLayoutGroup> ();
			vGroup.padding.left = -25;
			vGroup.padding.top = 20;

			vGroupData.padding.left = -25;
			vGroupData.padding.top = 20;


			float resolutionCanvasWidth = 800.0F;
			float resolutionCanvasHeigth = 600.0F;
	
			SetCanvasResolution (newCanvas, resolutionCanvasHeigth, resolutionCanvasWidth);


			dataPanel.transform.SetParent (newCanvas.transform, false);
			infoPanel.transform.SetParent(newCanvas.transform, false);
			newCanvas.transform.SetParent (hardwareObject.transform, false);


			float reformedHardwareX = (1.0F - hardwareObject.transform.localScale.x);
			float reformedHardwareY = (1.0F - hardwareObject.transform.localScale.y);

			float width = 2.0F / (resolutionCanvasWidth - (resolutionCanvasWidth * reformedHardwareX));
			float height = 2.0F / (resolutionCanvasHeigth - (resolutionCanvasHeigth * reformedHardwareY));
			newCanvas.transform.localScale = new Vector3 (width , height);

			this.SetDashboardDataSize (newCanvas, resolutionCanvasHeigth, resolutionCanvasWidth / 2); 
			this.SetDashboardInfoSize (newCanvas, resolutionCanvasHeigth / 2, resolutionCanvasWidth / 2); 


			this.RenderContent (infoPanel, dataPanel, hardware, renderOnTop);

			RecalculateCanvasPosition (newCanvas, hardwareObject);
			AddBillboardRenderer (newCanvas);
			return newCanvas;
		}
		public void RenderContent(GameObject infoPanel, GameObject dataPanel, Hardware hardware, Material material){
			// load all "static" data
			GameObject hardwareTitle = this.TextObject(infoPanel, infoPanel.name+"-hardwareTitle", "Name: weatherscanner", material);
			GameObject infoTitle = this.TextObject(infoPanel, infoPanel.name+"-title", "Info", material);
			GameObject hardwareType = this.TextObject(infoPanel, infoPanel.name+"-hardwareType", "Type: Sensor", material);

			GameObject dataTitle = this.TextObject(infoPanel, dataPanel.name+"-title", "Data", material);

			infoTitle.transform.SetParent (infoPanel.transform, false);
			hardwareTitle.transform.SetParent (infoPanel.transform, false);
			hardwareType.transform.SetParent (infoPanel.transform, false);

			dataTitle.transform.SetParent (dataPanel.transform, false);

			// load async data from server
		}
		private void SetCanvasResolution(GameObject dashboard, float height, float width){
			this.ResizeObject (dashboard, height, width);
		}

		private void SetDashboardInfoSize(GameObject dashboard, float height, float width){
			var dashboardInfo = GetInfoPanel (dashboard);
			this.ResizeObject (dashboardInfo, height, width);
			this.RecalculateDataPanelPosition (dashboard, width);
			//this.ResizeObject (dashboard, height, width);
		}
		private void SetDashboardDataSize(GameObject dashboard, float height, float width){
			var dashboardData = GetDataPanel (dashboard);
			this.ResizeObject (dashboardData, height, width);
		}

		/**
		 * helpers 
		 */
		private GameObject GetInfoPanel(GameObject dashboard){
			return dashboard.transform.Find (dashboard.name+"-info").gameObject;
		}
		private GameObject GetDataPanel(GameObject dashboard) {
			return dashboard.transform.Find (dashboard.name + "-data").gameObject;
		}

		private void ResizeObject(GameObject obj, float height, float width){
			var rectTransform = obj.GetComponent<RectTransform> ();
			rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, height); 
			rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, width);
		}
		private void AddBillboardRenderer(GameObject dashboard){
			CameraFacingBillboard billboard = dashboard.AddComponent<CameraFacingBillboard> ();
			Camera mainCamera = Camera.main;
			billboard.amActive = true;
			billboard.m_Camera = mainCamera;
		}
		private void RecalculateDataPanelPosition(GameObject dashboard, float x){
			var dataPanel = GetDataPanel (dashboard);
			float finalPos = -(x + 40.0F);
			dataPanel.transform.localPosition = new Vector3 (finalPos, 0.0F, 0.0F); 
		}
		private GameObject RenderPanel(Material renderOnTop, String name, Sprite backgroundColor){
			GameObject panel = new GameObject("Panel");
			panel.name = name;
			panel.AddComponent<CanvasRenderer>();
			Image i = panel.AddComponent<Image>();
			i.sprite = backgroundColor;
			Color c = i.color;
			c.a = 0.5F; // opacity
			i.color = c;

			i.material = renderOnTop;
			return panel;
		}
		private GameObject TextObject(GameObject dashboard, String name, String text, Material material,int fontSize = 50){

			GameObject newTextObject = new GameObject (name);
			Text myText = newTextObject.AddComponent<Text>();
			myText.fontSize = fontSize;
			myText.color = Color.black;
			myText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			myText.transform.localEulerAngles = new Vector3 (0.0F, 180.0F, 0.0F);
			myText.material = material;
			myText.resizeTextForBestFit = true;
			myText.text = text;
			return newTextObject;
		}


		/*
		 * Het is lastig om een positie te vinden van een sensor / hardware object.
		 * Daarom is er een box-collider (die er altijd opzit) genomen en daar de scale * 4 van gedaan 
		 * TODO: Eventueel een betere berekening hiervoor maken
		 */
		private void RecalculateCanvasPosition(GameObject dashboard, GameObject hardwareObject){
			BoxCollider hardwareCollider = hardwareObject.GetComponent<BoxCollider> ();
			var calculatedPos = (hardwareCollider.bounds.size.x * 10);
			Debug.Log (calculatedPos);
			dashboard.transform.localPosition = new Vector3 (calculatedPos, 0.0F, 0.0F);

		}
	}
}

