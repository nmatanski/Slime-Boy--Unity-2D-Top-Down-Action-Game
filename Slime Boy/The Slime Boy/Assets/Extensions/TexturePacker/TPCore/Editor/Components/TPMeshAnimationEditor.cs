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


[CustomEditor(typeof(TPMeshSpriteAnimationEx))]
public class TPMeshAnimationEditor : TPBaseAnimationEditor {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public override void OnInspectorGUI() {
		base.DrawAnimationInfo();
		
		
		
		base.DrawBaseAnimationGUI();
		
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
		
		base.DrawButtonsSection();
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	public TPMeshSpriteAnimation anim {
		get {
			return target as TPMeshSpriteAnimation;
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
