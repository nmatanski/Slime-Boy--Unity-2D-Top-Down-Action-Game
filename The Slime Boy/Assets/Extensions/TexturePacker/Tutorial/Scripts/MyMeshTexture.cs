////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class MyMeshTexture : TPMeshTexture {
	public MyTextures tx;
	
	
	
	
	protected override void Update() {
		if(!Application.isPlaying) {
			if(GetComponent<Renderer>().sharedMaterial != MyAtlasController.atlasMaterial) {
				GetComponent<Renderer>().sharedMaterial = MyAtlasController.atlasMaterial;
			}
			
			atlas = "TutorialAtlas";
			texture = tx.ToString();
		
			base.Update();
		}
	}
}

