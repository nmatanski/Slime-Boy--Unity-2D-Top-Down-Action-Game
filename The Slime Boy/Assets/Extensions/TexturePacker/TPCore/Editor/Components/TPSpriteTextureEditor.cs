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


[CanEditMultipleObjects]
[CustomEditor(typeof(TPSpriteTextureEx))]
public class TPSpriteTextureEditor : TPSpriteAnimationEditor {



	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public override void OnInspectorGUI() {

		DrawInfo();

		EditorGUILayout.PropertyField(_GizmosColor);


		base.DrawButtonsSection();
		
	}
	
	public void DrawInfo() {
		EditorGUILayout.Separator();
		string msg = string.Empty;


		if(targets.Length == 1) {
			if(tex.sprite.frames.Count > 0) {
				msg += "Atlas: " + tex.sprite.frames[0].atlasPath + "\n";
				msg += "Texture: " + tex.sprite.frames[0].textureName;
			} else {
				msg = "Sprite is empty";
			}
		} else {
			EditorGUILayout.HelpBox("Multiedition Mode", MessageType.Info);
		}

		
		EditorGUILayout.HelpBox(msg, MessageType.Info);
		
		EditorGUILayout.Separator();
	}
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	public TPSpriteTexture tex {
		get {
			return target as TPSpriteTexture;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
