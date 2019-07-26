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

public class GameObjectMenu : EditorWindow {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	[MenuItem("GameObject/Create Other/Texture Packer/Sprite Animation")]
	public static void CreateTPSpriteAnimation() {

		Mesh m = Resources.Load("TPPlaneCentred") as Mesh;

		TPSpriteAnimationEx anim =  new GameObject("TP Sprite Animation").AddComponent<TPSpriteAnimationEx>();
		GameObject sprite =  new GameObject("TP Sprite");
		sprite.transform.parent = anim.transform;
		
		MeshFilter mf = sprite.AddComponent<MeshFilter>();
		mf.mesh = m;
		
		sprite.AddComponent<MeshRenderer>();
		
		
		anim.sprite = sprite.AddComponent<TPSpriteEx>();
		sprite.AddComponent<TPMeshTextureNoHelperEx>();
		anim.transform.localScale = TexturePackerEditorConfig.SCLE_FACTOR * Vector3.one;




		anim.pivotCenterX = TexturePackerEditorConfig.PIVOT_CENTER_X;
		anim.pivotCenterY = TexturePackerEditorConfig.PIVOT_CENTER_Y;

		if(Selection.activeGameObject != null) {
			anim.transform.parent = Selection.activeGameObject.transform;
		}

		anim.transform.localPosition = TexturePackerEditorConfig.CREATE_POSITION;
		Selection.activeGameObject = anim.gameObject;


	}
	
	
	[MenuItem("GameObject/Create Other/Texture Packer/Sprite Texture")]
	public static void CreateTPSprite()  {
		
		Mesh m = Resources.Load("TPPlaneCentred") as Mesh;

		TPSpriteTextureEx anim =  new GameObject("TP Sprite Texture").AddComponent<TPSpriteTextureEx>();
		GameObject sprite =  new GameObject("TP Static Sprite");
		sprite.transform.parent = anim.transform;
		
		MeshFilter mf = sprite.AddComponent<MeshFilter>();
		mf.mesh = m;
		
		sprite.AddComponent<MeshRenderer>();
		
		
		anim.sprite = sprite.AddComponent<TPStaticSpriteEx>();
		sprite.AddComponent<TPMeshTextureNoHelperEx>();
		anim.transform.localScale = TexturePackerEditorConfig.SCLE_FACTOR * Vector3.one;




		anim.pivotCenterX = TexturePackerEditorConfig.PIVOT_CENTER_X;
		anim.pivotCenterY = TexturePackerEditorConfig.PIVOT_CENTER_Y;

		if(Selection.activeGameObject != null) {
			anim.transform.parent = Selection.activeGameObject.transform;
		}

		anim.transform.localPosition = TexturePackerEditorConfig.CREATE_POSITION;

		Selection.activeGameObject = anim.gameObject;
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
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
