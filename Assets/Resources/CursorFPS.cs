using UnityEngine;
using System.Collections;

public class CursorFPS : MonoBehaviour {
	CursorLockMode wantedMode;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0))
			Cursor.lockState = wantedMode = CursorLockMode.Locked;
		// Release cursor on escape keypress
		if (Input.GetKeyDown (KeyCode.Escape))
			Cursor.lockState = wantedMode = CursorLockMode.None;
	}

	// Apply requested cursor state
	void SetCursorState ()
	{
		Cursor.lockState = wantedMode;
		// Hide cursor when locking
		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}
	
	void OnGUI ()
	{	/*
		GUILayout.BeginVertical ();
		
		switch (Cursor.lockState)
		{
		case CursorLockMode.None:
			//GUILayout.Label ("Cursor is normal");
			if (GUILayout.Button ("Lock cursor"))
				wantedMode = CursorLockMode.Locked;
			if (GUILayout.Button ("Confine cursor"))
				wantedMode = CursorLockMode.Confined;
			break;
		case CursorLockMode.Confined:
			//GUILayout.Label ("Cursor is confined");
			if (GUILayout.Button ("Lock cursor"))
				wantedMode = CursorLockMode.Locked;
			if (GUILayout.Button ("Release cursor"))
				wantedMode = CursorLockMode.None;
			break;
		case CursorLockMode.Locked:
			//GUILayout.Label ("Cursor is locked");
			if (GUILayout.Button ("Unlock cursor"))
				wantedMode = CursorLockMode.None;
			if (GUILayout.Button ("Confine cursor"))
				wantedMode = CursorLockMode.Confined;
			break;
		}
		
		GUILayout.EndVertical ();
		*/
		GUI.Box(new Rect(Screen.width/2, Screen.height/2, 10, 10), "");
		
		SetCursorState ();
	}
}