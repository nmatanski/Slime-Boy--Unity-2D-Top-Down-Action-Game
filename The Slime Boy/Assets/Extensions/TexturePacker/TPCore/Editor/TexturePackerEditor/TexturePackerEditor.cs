////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TexturePackerEditor : EditorWindow {
	
	private TexturePackerAtlasEditor _atlasEditor;
	
	public static bool IsCtrlPressed  = false;
	public static bool isShiftPressed = false;


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	[MenuItem("Window/Texture Packer")]

	public static void init() {
		EditorWindow.GetWindow<TexturePackerEditor>();
	}


	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------


	void OnGUI() {
		title = "Texture Packer";
		atlasEditor.render();



		processInput ();
	}



	//--------------------------------------
	// GET / SET
	//--------------------------------------


	private TPAtlas getAtlas(string atlasName) {
		string path = TPAtlasesData.getAtlasPath(atlasName);
		return TPackManager.getAtlas(path);
	}

	private TexturePackerAtlasEditor atlasEditor {
		get {
			if(_atlasEditor == null) {
				_atlasEditor = new TexturePackerAtlasEditor ();
				_atlasEditor.editor = this;
			} 

			return _atlasEditor;

		}
	}

	//--------------------------------------
	// EVENTS
	//--------------------------------------


	


	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------



	private void processInput() {
		Event e = Event.current;;
		
		if (e.type == EventType.MouseDown) {

			Vector2 pos = Event.current.mousePosition;

			
			pos.y -= TexturePackerStyles.TOOLBAR_HEIGHT;

			if(Event.current.button == 0) {
				atlasEditor.OnLeftMouseClick (pos);
			}

			if(Event.current.button == 1) {
				atlasEditor.OnRightMouseClick (pos);
			}
		}
		
		isShiftPressed = e.shift;
		IsCtrlPressed = e.command || e.control;
		

	}
	

}
