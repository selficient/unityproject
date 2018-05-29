using System;
using UnityEngine;
using Business.Domain;
using UnityEngine.UI;

/*
 * Renderer die automatisch dashboard genereert.
 * @author T.J van der Ende
 */
using System.Collections;
using Task;
using UnityEngine.Events;


namespace Presentation.Dashboard
{
	public class DashboardRenderer
	{
		private UnityAction<System.Object> saveHardwareState;

		private GameObject dataPanel;
		private GameObject infoPanel;
		private Material renderOnTop;
		private GameObject mainCanvas;
		private Hardware hardware;
		private bool contentRendered = false;
		public void InitializeDashboard(GameObject hardwareObject, Hardware domainHardware){
			EventManager.StartListening ("showDatasetLoader", ShowDatasetLoader);


			hardware = domainHardware;
			renderOnTop = Resources.Load("Material/DashboardRenderOnTop", typeof(Material)) as Material;
			Sprite backgroundColor = Resources.Load ("Textures/DashboardBackground", typeof(Sprite)) as Sprite;
			if (renderOnTop == null) {
				Debug.Log ("Kon dashboard material niet toewijzen, kijk of je dit kunt oplossen");
			}


			mainCanvas = new GameObject("Canvas");
			mainCanvas.name = "dasbhoard-"+hardwareObject.name;
			Canvas c = mainCanvas.AddComponent<Canvas>();
			c.renderMode = RenderMode.WorldSpace;
			mainCanvas.AddComponent<CanvasScaler> ().dynamicPixelsPerUnit = 1.75f;
			mainCanvas.AddComponent<GraphicRaycaster>();
			//RecalculateCanvasPosition (newCanvas, hardwareObject);

			dataPanel = RenderPanel (renderOnTop, mainCanvas.name + "-data", backgroundColor);
			infoPanel = RenderPanel (renderOnTop, mainCanvas.name + "-info", backgroundColor);
			VerticalLayoutGroup vGroup = infoPanel.AddComponent<VerticalLayoutGroup> ();
			VerticalLayoutGroup vGroupData = dataPanel.AddComponent<VerticalLayoutGroup> ();
			vGroup.padding.left = 20;
			vGroup.padding.right = 20;
			vGroup.padding.top = 20;

			vGroupData.padding.left = 20;
			vGroupData.padding.right = 20;
			vGroupData.padding.top = 20;


			float resolutionCanvasWidth = 800.0F;
			float resolutionCanvasHeigth = 600.0F;
	
			SetCanvasResolution (mainCanvas, resolutionCanvasHeigth, resolutionCanvasWidth);


			dataPanel.transform.SetParent (mainCanvas.transform, false);
			infoPanel.transform.SetParent(mainCanvas.transform, false);
			mainCanvas.transform.SetParent (hardwareObject.transform, false);


			float reformedHardwareX = (1.0F - hardwareObject.transform.localScale.x);
			float reformedHardwareY = (1.0F - hardwareObject.transform.localScale.y);

			float width = 2.0F / (resolutionCanvasWidth - (resolutionCanvasWidth * reformedHardwareX));
			float height = 2.0F / (resolutionCanvasHeigth - (resolutionCanvasHeigth * reformedHardwareY));
			mainCanvas.transform.localScale = new Vector3 (width , height);

			this.SetDashboardDataSize (mainCanvas, resolutionCanvasHeigth, resolutionCanvasWidth / 2); 
			this.SetDashboardInfoSize (mainCanvas, resolutionCanvasHeigth / 2, resolutionCanvasWidth / 2); 


			//this.RenderContent (hardware);

			RecalculateCanvasPosition (mainCanvas, hardwareObject);
			AddBillboardRenderer (mainCanvas);



		}
		public GameObject GetDashboard(){
			return this.mainCanvas;
		}
		public void RenderContent(){
			if (!contentRendered) {
				// load all "static" data
				GameObject hardwareTitle = this.RenderText(infoPanel, infoPanel.name+"-hardwareTitle", "Name: "+hardware.name, renderOnTop);
				GameObject infoTitle = this.RenderText(infoPanel, infoPanel.name+"-title", "Info", renderOnTop);
				GameObject hardwareType = this.RenderText(infoPanel, infoPanel.name+"-hardwareType", "Type: "+hardware.type.name, renderOnTop);

				GameObject dataTitle = this.RenderText(infoPanel, dataPanel.name+"-title", "Data", renderOnTop);

				infoTitle.transform.SetParent (infoPanel.transform, false);
				hardwareTitle.transform.SetParent (infoPanel.transform, false);
				hardwareType.transform.SetParent (infoPanel.transform, false);

				/*Sprite testSprite = Resources.Load ("Textures/Test1", typeof(Sprite)) as Sprite;
				GameObject image1 = this.RenderImage (dataPanel, dataPanel.name + "-data-whatever", testSprite, material);
				GameObject image2 = this.RenderImage (dataPanel, dataPanel.name + "-data-whatever2", testSprite, material); */


				dataTitle.transform.SetParent (dataPanel.transform, false);
				/*image1.transform.SetParent (dataPanel.transform, false);
				image2.transform.SetParent (dataPanel.transform, false); */
				EventManager.TriggerEvent ("loadHardwareDataset", "d1"); // load dataset corresponding to this hardware object.

				contentRendered = true;
			}
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

		private void ShowDatasetLoader (System.Object show)
		{
			bool showLoader = Convert.ToBoolean (show);
			GameObject prefab = Resources.Load ("Prefabs/InteractivityLoader", typeof(GameObject)) as GameObject;
			GameObject obj = GameObject.Instantiate (prefab, new Vector3 (0, 0, 0), Quaternion.identity);
			obj.transform.parent = dataPanel.transform;
			obj.SetActive (true);
			/*if (showLoader) {
				
			}*/
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
		private GameObject RenderText(GameObject dashboard, String name, String text, Material material,int fontSize = 50){

			GameObject newTextObject = new GameObject (name);
			Text myText = newTextObject.AddComponent<Text>();
			myText.fontSize = fontSize;
			myText.color = Color.black;
			myText.font = Resources.Load ("Fonts/Roboto-Medium", typeof(Font)) as Font;
			myText.transform.localEulerAngles = new Vector3 (0.0F, 180.0F, 0.0F);
			myText.material = material;
			myText.resizeTextForBestFit = true;
			myText.text = text;
			return newTextObject;
		}

		private GameObject RenderImage (GameObject dashboard, String name, Sprite resourceToLoad, Material material){
			GameObject newImageObject = new GameObject (name);
			Image imageObject = newImageObject.AddComponent<Image> ();
			imageObject.sprite = resourceToLoad;
			imageObject.material = material;
			imageObject.preserveAspect = true;
			return newImageObject;
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

