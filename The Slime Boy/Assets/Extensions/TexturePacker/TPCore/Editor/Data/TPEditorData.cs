////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;

public class TPEditorData {


	public static string selectedAtlasName {
		get {
			if(EditorPrefs.HasKey("selectedAtlasName")) {
				return EditorPrefs.GetString ("selectedAtlasName");
			} else {
				return string.Empty;
			}
		}

		set {
			EditorPrefs.SetString ("selectedAtlasName", value);
		}
	}

	public static bool isExtensionsEnabled {
		get {
			if(EditorPrefs.HasKey("isExtensionsEnabled")) {
				return EditorPrefs.GetBool ("isExtensionsEnabled");
			} else {
				return false;
			}
		}

		set {
			EditorPrefs.SetBool ("isExtensionsEnabled", value);
		}
	}

}
