////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class MyAtlasController  {
	private static Material _atlasMaterial = null;


	//--------------------------------------
	// GET / SET
	//--------------------------------------
	
	public static Material  atlasMaterial {
		get {
			if(_atlasMaterial == null) {
				_atlasMaterial = Resources.Load("TutorialAtlasMaterial") as Material;
			}
			
			return _atlasMaterial;
		}
	}
	
}

