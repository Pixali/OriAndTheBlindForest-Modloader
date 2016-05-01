using System;
using UnityEngine;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace TesseractModLoader.Window
{
	public class Console : MonoBehaviour
	{
        public Rect consoleWindowRect = new Rect(20,20,400,500);
		public bool consoleWindow = true;
        public Vector2 loadedMods;

		public void OnGUI()
        {
			if (consoleWindow)
            {
				consoleWindowRect = GUI.Window (0, consoleWindowRect, ConsoleWindow, "Console");
			}
		}

		public void ConsoleWindow(int windowID)
        {
            GUILayout.Label("SeinModLoader v0.2 Enabled!");
			GUILayout.Label("Press Ctrl + I to toggle the Console (this menu).");
			GUILayout.Label("Press Ctrl + O to toggle Object Editor (Developer-Only).");
            GUILayout.Label("Press Ctrl + B to toggle Object Browser (Developer-Only)");
            GUILayout.Label("Press Ctrl + P to toggle Debug Viewer (Developer-Only)");
			GUILayout.Label ("-----");
			GUILayout.Label ("Loaded mods:");
			GUILayout.BeginVertical ();
            loadedMods = GUILayout.BeginScrollView(loadedMods);
			foreach (String path in Directory.GetFiles(Application.dataPath+ "/Managed/Mods/","*.dll"))
            {
				GUILayout.Label(path);
			}
			GUILayout.EndVertical ();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0, 0, consoleWindowRect.width, consoleWindowRect.height));
        }

		public void Update() {
			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKeyDown (KeyCode.I) || Input.GetKey (KeyCode.RightControl) && Input.GetKeyDown (KeyCode.I)) {
				consoleWindow = !consoleWindow;
			}
		}
	}
}

