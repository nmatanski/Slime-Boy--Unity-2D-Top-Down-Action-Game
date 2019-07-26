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


public class TPBaseAnimationEditor : Editor {


	private SerializedProperty _useCollider;

	private SerializedProperty _pivotCenterX;
	private SerializedProperty _pivotCenterY;

	private SerializedProperty _opacity;


	private SerializedProperty _useSeparateMaterial;

	private SerializedProperty _custom_shader;

	private SerializedProperty _IsForceSelected; 
	private SerializedProperty _useImageName;


	private SerializedProperty _frameRate;
	private SerializedProperty _PlayOnStart; 
	private SerializedProperty _Loop;

	private SerializedProperty _currentFrame;
	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public virtual void OnEnable () {

		_useCollider = serializedObject.FindProperty ("useCollider");

		_pivotCenterX = serializedObject.FindProperty ("pivotCenterX");
		_pivotCenterY = serializedObject.FindProperty ("pivotCenterY");

		_opacity = serializedObject.FindProperty ("opacity");

		_useSeparateMaterial = serializedObject.FindProperty("useSeparateMaterial");
		_custom_shader = serializedObject.FindProperty("custom_shader");

		_IsForceSelected = serializedObject.FindProperty("IsForceSelected");
		_useImageName = serializedObject.FindProperty("useImageName");

		_frameRate = serializedObject.FindProperty("frameRate");
		_PlayOnStart = serializedObject.FindProperty("PlayOnStart");
		_Loop = serializedObject.FindProperty("Loop");

		_currentFrame = serializedObject.FindProperty("currentFrame");

	}

	
	public void DrawAnimationInfo() {
		EditorGUILayout.Separator();

		if(targets.Length == 1) {
			int totalFrames = tpAnim.lastFrameIndex + 1;
			float duration = totalFrames * (1f / tpAnim.frameRate);
			EditorGUILayout.HelpBox("Total Frames: " + totalFrames + ", Duration: " + duration + " sec", MessageType.Info);
		} else {
			EditorGUILayout.HelpBox("Multiedition Mode", MessageType.Info);
		}

		
		EditorGUILayout.Separator();
	}
	
	public void DrawBaseAnimationGUI() {


		EditorGUI.BeginChangeCheck();
		EditorGUILayout.IntSlider(_currentFrame, 0, minLastFrame);

		if (EditorGUI.EndChangeCheck ()) {
			OnEditorFrameChange ();
		}
		

		EditorGUILayout.IntSlider(_frameRate, 1, 50);
		EditorGUILayout.PropertyField(_PlayOnStart);
		EditorGUILayout.PropertyField(_Loop);


	}
	
	public void DrawButtonsSection() {


		EditorGUILayout.PropertyField (_IsForceSelected);
		EditorGUILayout.PropertyField (_useImageName);

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField (_useCollider);
		if (EditorGUI.EndChangeCheck ()) {
			serializedObject.ApplyModifiedProperties ();
			foreach(TPBaseAnimation a in targets) {
				a.OnColliderSettingsChange();
			}

		}

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.Slider (_opacity, 0f, 1f);
		if (EditorGUI.EndChangeCheck ()) {
			OnEditorFrameChange ();
		}


		EditorGUI.BeginChangeCheck();

		EditorGUILayout.Slider (_pivotCenterX, 0f, 1f);
		EditorGUILayout.Slider (_pivotCenterY, 0f, 1f);

		
		if (EditorGUI.EndChangeCheck ()) {
			OnEditorFrameChange();
		}


		EditorGUILayout.Separator();

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField (_useSeparateMaterial);

		if (EditorGUI.EndChangeCheck ()) {
			OnEditorFrameChange();
		}


		if(tpAnim.useSeparateMaterial) {
			EditorGUI.BeginChangeCheck();
			_custom_shader.intValue = EditorGUILayout.Popup ("Shader", _custom_shader.intValue, TPShaders.importedShaders);
			if (EditorGUI.EndChangeCheck ()) {
				OnEditorFrameChange();
			}
		}
		
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		


		string addButtontext = "Add";
		if(tpAnim is TPSpriteTexture) {
			addButtontext = "Set";
		}

		if(GUILayout.Button(new GUIContent(addButtontext),   GUILayout.Width(80))) {
			EditorWindow.GetWindow<TexturePackerEditor>();
	    }
		
		
		if(GUILayout.Button(new GUIContent("Clear"),   GUILayout.Width(80))) {
			Clear();
	    }
		
		if(GUILayout.Button(new GUIContent("Update"),   GUILayout.Width(80))) {
			OnEditorFrameChange ();
	    }
		
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Separator();

		serializedObject.ApplyModifiedProperties ();
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public int minLastFrame {
		get {
			int min = tpAnim.lastFrameIndex;
			foreach(TPBaseAnimation a in targets) {
				if(a.lastFrameIndex < min) {
					min = a.lastFrameIndex;
				}

			}

			return min;
		}
	}
	
	public TPBaseAnimation tpAnim {
		get {
			return target as TPBaseAnimation;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------

	protected void Clear() {
		foreach(TPBaseAnimation a in targets) {
			a.ClearFrames();
			a.OnEditorFrameChange();
		}
	}

	protected void OnEditorFrameChange() {
		serializedObject.ApplyModifiedProperties ();
		foreach(TPBaseAnimation a in targets) {
			a.OnEditorFrameChange();
		}
	}
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
