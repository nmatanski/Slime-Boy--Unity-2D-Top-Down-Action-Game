////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class MyFireAnimation : TPMeshAnimation {
	
	
	
	
	
	//--------------------------------------
	// EVENTS
	//--------------------------------------
	
	protected override void Update() {
		if(!Application.isPlaying) {
			
			if(GetComponent<Renderer>().sharedMaterial != MyAtlasController.atlasMaterial) {
				GetComponent<Renderer>().sharedMaterial = MyAtlasController.atlasMaterial;
			}
			
			frames = new string[6];
	
			frames[0] = "fireball_0001";
			frames[1] = "fireball_0002";
			frames[2] = "fireball_0003";
			frames[3] = "fireball_0004";
			frames[4] = "fireball_0005";
			frames[5] = "fireball_0006";
			
			atlas = "TutorialAtlas";
			base.Update();
		}
		
	}
	
	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	// DESTROY
	//--------------------------------------
}
