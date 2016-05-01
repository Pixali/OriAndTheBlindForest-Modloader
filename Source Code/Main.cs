using System;
using UnityEngine;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace TesseractModLoader
{
	public class Main : MonoBehaviour
	{
		GameObject modObject;

		public void Start()
        {
			modObject = new GameObject ();
			modObject.AddComponent<TesseractModLoader.ModLoader>();
			modObject.AddComponent<TesseractModLoader.Window.Debug>();
			modObject.AddComponent<TesseractModLoader.Window.Explorer>();
			modObject.AddComponent<TesseractModLoader.Window.Console>();
			GameObject.DontDestroyOnLoad (modObject);
			modObject.name = "SeinModloaderLoader";
		}
	}

	public class ModLoader : MonoBehaviour
    {
		public void Start()
        {
			Directory.CreateDirectory (Application.dataPath + "/Managed/Mods/");
			foreach (String path in Directory.GetFiles(Application.dataPath+ "/Managed/Mods/","*.dll")) {
				Type type = Assembly.LoadFrom (path).GetType ("Mod.Main");
				MethodInfo method = type.GetMethod("Start");
				method.Invoke(Activator.CreateInstance(type), null);
			}
		}
	}
}