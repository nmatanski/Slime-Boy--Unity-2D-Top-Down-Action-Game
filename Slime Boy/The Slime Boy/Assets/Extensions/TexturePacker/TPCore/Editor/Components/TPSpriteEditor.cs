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

[CustomEditor(typeof(TPSpriteEx))]
public class TPSpriteEditor : Editor {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public override void OnInspectorGUI() {
		
		serializedObject.Update();
		#if UNITY_4_3
		#else
		
		EditorGUIUtility.LookLikeInspector();
		#endif
		SerializedProperty tps = serializedObject.FindProperty ("frames");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(tps, true);
		if(EditorGUI.EndChangeCheck()) {
			serializedObject.ApplyModifiedProperties();
		}
		#if UNITY_4_3
		#else
		
		EditorGUIUtility.LookLikeControls();
		#endif
		
		
		
		if(Selection.activeGameObject == sprite.gameObject) {
			TPForceSelectedComponent ff = sprite.transform.parent.gameObject.GetComponent<TPForceSelectedComponent>();
			if(ff != null) {
				if(ff.IsForceSelected) {
					Selection.activeGameObject = sprite.transform.parent.gameObject;
				}
			}		
		}
		
		
		
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	public TPSprite sprite {
		get {
			return target as TPSprite;
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
