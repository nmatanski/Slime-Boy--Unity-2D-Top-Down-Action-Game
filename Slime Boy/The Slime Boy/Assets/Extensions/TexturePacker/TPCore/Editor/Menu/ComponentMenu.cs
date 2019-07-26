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

public class ComponentMenu : EditorWindow {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	
	[MenuItem("Component/Texture Packer/TPMeshTextureEx")]
	public static void AddTPMeshTextureEx() {
		
		if(Selection.activeGameObject != null) {
			MeshRenderer m = Selection.activeGameObject.GetComponent<MeshRenderer>();
			if(m != null) {
				Selection.activeGameObject.AddComponent<TPMeshTextureEx>();
			} else {
				ShowError("TPMeshTexture requires MeshRenderer");
			}
		} else {
			ShowError("No Game Object Selected");
		}
	}
	
	
	[MenuItem("Component/Texture Packer/TPSkinnedMeshTextureEx")]
	public static void AddTPSkinnedMeshTextureEx() {
		
		if(Selection.activeGameObject != null) {
			SkinnedMeshRenderer m = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>();
			if(m != null) {
				Selection.activeGameObject.AddComponent<TPSkinnedMeshTextureEx>();
			} else {
				ShowError("TPSkinnedMeshTextureEx requires SkinnedMeshRenderer");
			}
		} else {
			ShowError("No Game Object Selected");
		}
	}
	
	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	private static void ShowError(string msg) {
		EditorApplication.Beep();
		EditorUtility.DisplayDialog("Texture Packer", msg, "Okay");

	}
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
