////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class AtlasGUIAnimationExample : MonoBehaviour {

	
	private TPGUIAnimation anim;
	private TPGUIAnimation Scaledanim;
	
	
	void Awake() {
		anim	    =  TPGUIAnimation.Create();
		Scaledanim  =  TPGUIAnimation.Create();
		
		for(int i = 1; i < 7; i++) {
			TPAtlasTexture frame = TPackManager.getAtlas(Atlases.EXAMPLE).getPngTexture("fireball_000" + i.ToString());
			anim.addFrame(frame);
			Scaledanim.addFrame(frame);
		}
		
		anim.pos = new Vector2(200, 200);
		anim.loop = true;
		anim.fps = 25;
		anim.Play();
		
		Scaledanim.pos = new Vector2(300, 200);
		Scaledanim.loop = true;
		Scaledanim.fps = 25;
		Scaledanim.scale = 0.5f;
		Scaledanim.Play();
		
		
		
	}
	
	void OnGUI() {
		anim.draw();
		
		Scaledanim.draw();
	}
}

