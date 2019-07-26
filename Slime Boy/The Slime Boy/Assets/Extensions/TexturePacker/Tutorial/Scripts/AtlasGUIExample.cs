////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtlasGUIExample : MonoBehaviour {

	private TPAtlasTexture play;
	
	private Texture2D unityTexture;
	
	
	void Awake() {
		play = TPackManager.getAtlas(Atlases.EXAMPLE).getPngTexture("play");
		
		//Warning to Get Texture2D from atlas, should remain the same size which was generated,
		//that's why we using another atlas here
		unityTexture = TPackManager.getAtlas(Atlases.EXAMPLE2).getUnityTexture("play");
	}
	
	
	
	void OnGUI() {
		play.draw(new Rect(0, 0, play.width * 0.5f, play.height * 0.5f));
		
		play.draw(new Rect(0, 75, play.width, play.height));
		
		GUI.DrawTexture(new Rect(0, 225, unityTexture.width, unityTexture.height ), unityTexture);
		
		
		TPAtlasTexture tex = TPackManager.getAtlas(Atlases.EXAMPLE).getPngTexture("f_share");
		tex.draw(new Rect(200, 0, 122, 42));
		

		TPackManager.getAtlas(Atlases.EXAMPLE).draw(new Rect(200, 100, 122, 42), "t_share.png");
		
		
	}

}
