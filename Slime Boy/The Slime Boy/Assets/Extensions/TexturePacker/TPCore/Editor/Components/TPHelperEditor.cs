////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TPHelper))]
public class TPHelperEditor : Editor {

	public bool xss;
	private string msg = string.Empty;
	private MessageType msgType = MessageType.Info;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {

		int index = 0;
		string[] atlasesNames = TPAssetPostprocessor.atlasesNames;

		if(atlasesNames.Length == 0) {
			return;
		}


		if(helper.meshTexture == null) {
			setMessage ("TPMeshTexture component missing", MessageType.Error);
			return;
		}



		foreach(string n in atlasesNames) {
			if(n.Equals(helper.meshTexture.atlas)) {
				helper.atlasID = index;
				break;
			}
			index++;
		}


		TPAtlas atlas = helper.meshTexture.TextureAtlas;
		if(atlas != null) {

			index = 0;
			string[] textures = atlas.frameNames;
			foreach(string n in textures) {
				string noExt = n.Substring (0, n.Length - 4);
				if(n.Equals(helper.meshTexture.texture) || noExt.Equals(helper.meshTexture.texture)) {
					helper.textureID = index;
					break;
				}
				index++;
			}

		}


		helper.OnAtlasChange(atlasesNames[helper.atlasID]);
		string[] tx = helper.meshTexture.TextureAtlas.frameNames;
		helper.OnTextureChange(tx[helper.textureID]);

	}
	
	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------

	public override void OnInspectorGUI() {


		if(TPAssetPostprocessor.atlasesNames.Length == 0) {
			setMessage ("No Atlas Found", MessageType.Error);
		}

		if(msgType != MessageType.Info) {
			EditorGUILayout.HelpBox(msg, msgType);
		}

		if(msgType != MessageType.Error) {

			string[] atlasesNames = TPAssetPostprocessor.atlasesNames;

			EditorGUI.BeginChangeCheck();
			helper.atlasID = EditorGUILayout.Popup("Atlas: ", helper.atlasID, atlasesNames);
			if(EditorGUI.EndChangeCheck()) {
				helper.OnAtlasChange(atlasesNames[helper.atlasID]);
				if(helper.meshTexture.TextureAtlas == null) {
					TPAssetPostprocessor.UpdateAtlasesInfromation();
					return;
				}
				string[] textures = helper.meshTexture.TextureAtlas.frameNames;
				helper.textureID = 0;
				helper.OnTextureChange(textures[helper.textureID]);
			}




			if(helper.meshTexture != null) {
				TPAtlas atlas = helper.meshTexture.TextureAtlas;
				if(atlas != null) {
					string[] textures = atlas.frameNames;

					EditorGUI.BeginChangeCheck();
					helper.textureID = EditorGUILayout.Popup("Texture: ", helper.textureID, textures);
					if(EditorGUI.EndChangeCheck()) {
						helper.OnTextureChange(textures[helper.textureID]);
					}

				}
			}


			helper.replaceMaterial = EditorGUILayout.Toggle ("Replace Material", helper.replaceMaterial);

		}

		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();


		GUILayout.FlexibleSpace();
		if(GUILayout.Button(new GUIContent("Update"),   GUILayout.Width(100))) { 
			TPAssetPostprocessor.UpdateAtlasesInfromation();
			
			
			if(helper.meshTexture != null) {
				TPAtlas atlas = helper.meshTexture.TextureAtlas;
				if(atlas != null) {
					string[] atlasesNames = TPAssetPostprocessor.atlasesNames;
					helper.OnAtlasChange(atlasesNames[helper.atlasID]);
					
					string[] textures = helper.meshTexture.TextureAtlas.frameNames;
					helper.OnTextureChange(textures[helper.textureID]);
				}
			}
			
			
		}


	
		EditorGUILayout.EndHorizontal();

		updateStatus ();

	}


	private void updateStatus() {

		if(helper.meshTexture == null) {
			setMessage ("TPMeshTexture component missing", MessageType.Error);
			return;
		}

		if(hasAtlas) {
			if(hasTexture) {
				setMessage ("Corrects", MessageType.Info);
			} else  {
				setMessage ("No Texture", MessageType.Warning);
			}
		} else {
			setMessage ("No Atlas", MessageType.Warning);
		}


	}
	
	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public TPHelper helper {
		get {
			return target as TPHelper;
		}
	}

	public bool hasAtlas {
		get {
			string[] atlasesNames = TPAssetPostprocessor.atlasesNames;
			foreach(string n in atlasesNames) {
			
				string atlasName = helper.meshTexture.atlas;
				int index = atlasName.LastIndexOf("/");
				if(index != -1) {
					index++;
					atlasName = atlasName.Substring(index, atlasName.Length - index);
				}
				
				if(n.Equals(atlasName)) {
					return true;
				}
			}

			return false;
		}
	}


	public bool hasTexture {
		get {
			TPAtlas atlas = helper.meshTexture.TextureAtlas;
			if(atlas != null) {

				string[] textures = atlas.frameNames;
				foreach(string n in textures) {
					string noExt = n.Substring (0, n.Length - 4);
					if(n.Equals(helper.meshTexture.texture) || noExt.Equals(helper.meshTexture.texture)) {
						return true;
					}
				}

			}

			return false;
		}
	}
	
	//--------------------------------------
	// EVENTS
	//--------------------------------------
	
	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------

	private void setMessage(string _msg, MessageType type) {
		msg = _msg;
		msgType = type;
	}
	
	//--------------------------------------
	// DESTROY
	//--------------------------------------
}
