////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;



public class TPHelper : MonoBehaviour  {

	public bool replaceMaterial = true;

	[HideInInspector] public int atlasID = 0;
	[HideInInspector] public int textureID = 0;

	private TPMeshTexture _meshTexture = null;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	
	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------
	

	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public TPMeshTexture meshTexture {
		get {
			if(_meshTexture == null) {
				_meshTexture = GetComponent<TPMeshTexture>();
			}
			return _meshTexture;
		}
	}
	
	//--------------------------------------
	// EVENTS
	//--------------------------------------

	public void OnAtlasChange(string atlasName) {
		string path = TPAtlasesData.getAtlasPath(atlasName);
		meshTexture.atlas = path;

		if(replaceMaterial) {
			Material m = Resources.Load(path + "Material") as Material;

			if(gameObject.GetComponent<Renderer>().sharedMaterial != m) {
				gameObject.GetComponent<Renderer>().sharedMaterial = m;
			}
		}



	}

	public void OnTextureChange(string textureName) {
		meshTexture.texture = textureName;

		meshTexture.UpdateView();
	}
	
	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	// DESTROY
	//--------------------------------------
}
