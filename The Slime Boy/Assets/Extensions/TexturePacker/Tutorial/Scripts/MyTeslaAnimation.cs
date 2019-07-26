////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class MyTeslaAnimation : TPMeshAnimation {
	
	
	
	
	
	//--------------------------------------
	// EVENTS
	//--------------------------------------
	
	protected override void Update() {
		
		if(!Application.isPlaying) {
			if(GetComponent<Renderer>().sharedMaterial != MyAtlasController.atlasMaterial) {
				GetComponent<Renderer>().sharedMaterial = MyAtlasController.atlasMaterial;
			}
			
			frames = new string[10];
	
			frames[0] = "bolt_tesla_0001";
			frames[1] = "bolt_tesla_0002";
			frames[2] = "bolt_tesla_0003";
			frames[3] = "bolt_tesla_0004";
			frames[4] = "bolt_tesla_0005";
			frames[5] = "bolt_tesla_0006";
			frames[6] = "bolt_tesla_0007";
			frames[7] = "bolt_tesla_0008";
			frames[8] = "bolt_tesla_0009";
			frames[9] = "bolt_tesla_0010";
			
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
