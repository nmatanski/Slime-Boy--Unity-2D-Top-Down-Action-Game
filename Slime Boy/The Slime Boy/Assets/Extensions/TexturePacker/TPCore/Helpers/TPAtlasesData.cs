////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class TPAtlasesData  {

	public static Dictionary<string, string> registerAtlases = null;


	public static void AddAtlasInfo(string atlas, string path) {
		registerAtlases.Add(atlas, path);
	}




	public static string getAtlasPath(string AtlasName) {
		if(registerAtlases.ContainsKey(AtlasName)) {
			return registerAtlases[AtlasName];
		} else {
			return string.Empty;
		}
	}

}
