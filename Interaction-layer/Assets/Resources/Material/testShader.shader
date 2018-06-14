Shader "Custom/testShader" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	}
		SubShader{
		pass {
		Tags{ "Queue" = "Geometry+1" }
		ZTest Greater
		ZWrite Off

	}
	}
}
