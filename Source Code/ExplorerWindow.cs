using System;
using UnityEngine;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace TesseractModLoader.Window
{
	public class Explorer : MonoBehaviour
	{
		public Rect explorerWindowRect = new Rect(20,20,450,400);
        public Rect allObjs = new Rect(20, 20, 450, 400);
        public bool explorerWindow = false;
        public bool allObjsEnabled = false;
		public Vector2 explorerScrollBar = new Vector2();
        public Vector2 allObjsScroll = new Vector2();

        public GameObject toEdit;
        public string x;
        public string y;
        public string z;
        public string x1;
        public string y1;
        public string z1;

        public void OnGUI()
        {
            if (explorerWindow)
            {
				explorerWindowRect = GUI.Window (1, explorerWindowRect, ExplorerWindow, "Object Editor");                
            }
            if (allObjsEnabled)
            {
                allObjs = GUI.Window(3, allObjs, BrowserWindow, "Object Browser");
            }
		}

        public void BrowserWindow(int windowID)
        {
            GUILayout.BeginVertical();
            allObjsScroll = GUILayout.BeginScrollView(allObjsScroll);
            foreach (GameObject go in FindObjectsOfType<GameObject>())
            {
                if (GUILayout.Button(go.name))
                    Destroy(go);
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        public void ExplorerWindow(int windowID)
        {
			GUILayout.BeginVertical();
            explorerScrollBar = GUILayout.BeginScrollView(explorerScrollBar);

            if (toEdit != null)
            {
                GUILayout.Label("Object Name: '" + toEdit.name + "'");
                GUILayout.Label("Object Location (Level): '" + Application.loadedLevelName + "'");
                GUILayout.Label("");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Custom Position:");
                x = GUILayout.TextField(x);
                y = GUILayout.TextField(y);
                z = GUILayout.TextField(z);
                GUILayout.EndHorizontal();                
                if (GUILayout.Button("Apply position"))
                    toEdit.transform.position = new Vector3(float.Parse(x), float.Parse(y), float.Parse(z));
                GUILayout.Label("");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Custom Scale:");
                x1 = GUILayout.TextField(x1);
                y1 = GUILayout.TextField(y1);
                z1 = GUILayout.TextField(z1);
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Apply scale"))
                    toEdit.transform.localScale = new Vector3(float.Parse(x1), float.Parse(y1), float.Parse(z1));
                GUILayout.Label("");
                if (GUILayout.Button("Delete Object"))
                    Destroy(toEdit);
                GUILayout.Label("");
                GUILayout.Label("Components:");
                foreach (Component c in toEdit.GetComponents(typeof(Component)))
                {
                    GUILayout.Button(c.GetType().Name);  
                }
                GUILayout.Label("");
                GUILayout.Label("Children:");
                foreach (Transform child in toEdit.transform)
                {
                    GUILayout.Button(child.name);
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0, 0, explorerWindowRect.width, explorerWindowRect.height));
        }

		public void Update()
        {
            if (Cursor.visible == false)
                Cursor.visible = true;

			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKeyDown (KeyCode.O) || Input.GetKey (KeyCode.RightControl) && Input.GetKeyDown (KeyCode.O))
            {
				explorerWindow = !explorerWindow;
			}
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B) || Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.B))
            {
                allObjsEnabled = !allObjsEnabled;
            }

            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    toEdit = hit.transform.gameObject;
                    x = toEdit.transform.position.x.ToString();
                    y = toEdit.transform.position.y.ToString();
                    z = toEdit.transform.position.z.ToString();
                    x1 = toEdit.transform.localScale.x.ToString();
                    y1 = toEdit.transform.localScale.y.ToString();
                    z1 = toEdit.transform.localScale.z.ToString();
                }
            }
        }
	}
}

