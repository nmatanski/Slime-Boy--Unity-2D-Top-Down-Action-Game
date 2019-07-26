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

[CustomEditor(typeof(TPSpriteAnimationEx))]
public class TPSpriteAnimationEditor : TPBaseAnimationEditor {

	protected SerializedProperty _GizmosColor;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public override void OnEnable () {
		base.OnEnable();

		_GizmosColor = serializedObject.FindProperty ("GizmosColor");

	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	void Awake() {
		anim.OnEditorFrameChange ();
	}
	
	
	public override void OnInspectorGUI() {


		base.DrawAnimationInfo();
		
		EditorGUILayout.PropertyField(_GizmosColor);


		base.DrawBaseAnimationGUI();

		base.DrawButtonsSection();
		
	}
	

	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public TPSpriteAnimation anim {
		get {
			return target as TPSpriteAnimation;
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
