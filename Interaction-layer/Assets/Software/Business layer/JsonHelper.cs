using System;
using UnityEngine;

namespace Business {
	public static class JsonHelper
	{
		public static T[] FromJson<T>(string json)
		{
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
			return wrapper.hardware;
		}

		public static string ToJson<T>(T[] array)
		{
			Wrapper<T> wrapper = new Wrapper<T>();
			wrapper.hardware = array;
			return JsonUtility.ToJson(wrapper);
		}

		public static string ToJson<T>(T[] array, bool prettyPrint)
		{
			Wrapper<T> wrapper = new Wrapper<T>();
			wrapper.hardware = array;
			return JsonUtility.ToJson(wrapper, prettyPrint);
		}

		[Serializable]
		private class Wrapper<T>
		{
			public T[] hardware;
		}
	}

}

