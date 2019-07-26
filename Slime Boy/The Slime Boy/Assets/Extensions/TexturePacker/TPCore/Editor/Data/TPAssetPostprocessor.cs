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

public class TPAssetPostprocessor : AssetPostprocessor {

	private const string TP_ATLASES_INTFO = "tp_atlases_intfo";

	private static List<string> atlases = null;



	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------

	public static void UpdateAtlasesInfromation() {
		atlases = new List<string>();
		DirSearch("Assets/");

		string atlasesString = "";
		bool isFirst = true;
		foreach(string s in atlases) {
			if(!isFirst) {
				atlasesString += ",";
			}
			if(isFirst) {
				isFirst = false;
			}

			atlasesString += s;

		}
		EditorPrefs.SetString(TP_ATLASES_INTFO, atlasesString);


		fillAtlasesDictionary();

	}



	

	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public static Dictionary<string, string> registerAtlases {
		get {
			if(TPAtlasesData.registerAtlases == null) {
				fillAtlasesDictionary();
			}
			return TPAtlasesData.registerAtlases;
		}
	}

	public static string[] atlasesNames {
		get {

			if(atlases == null) {
				UpdateAtlasesInfromation ();
			}

			string[] f = new string[registerAtlases.Count];
			int index = 0;
			foreach(string frName in registerAtlases.Keys) {
			
				f[index] = frName;
				index++;
			}
			return f;
		}
	}
	
	
	public static string ResourcesFolder {
		get {
			return "Resources/";
		}
	}
	
	//--------------------------------------
	// EVENTS
	//--------------------------------------
	

	private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets,  string[] movedAssets, string[] movedFromPath) {
		foreach(string a in importedAssets) {
			checkForAtlasDataFile(a);
		}

		foreach(string a in movedAssets) {
			checkForAtlasDataFile(a);
		} 
	}

	private static void checkForAtlasDataFile(string f) {
		
		if(f.Length >= 9) {
			string postfix = f.Substring(f.Length - 9, 9);
			if(postfix.Equals("_data.txt")) {
				
				int rIndex = f.IndexOf(ResourcesFolder) + 10;
				string atlasPath = f.Substring(rIndex , f.Length - rIndex - 9);

				checkForMaterial(atlasPath, f.Substring(0, f.IndexOf("_data.txt")));

				UpdateAtlasesInfromation ();
			}
		}
	}


	public static void checkForMaterial(string resPath, string path) {
		Material m = Resources.Load(resPath + "Material") as Material;
		if(m == null) {
		
			int rIndex = path.IndexOf("Assets/");
			if(rIndex != -1) {
				string materialPath = path.Substring(rIndex , path.Length - rIndex) + "Material.mat";

				Material material = new Material (Shader.Find("Unlit/Transparent"));

				material.SetTexture("_MainTex", Resources.Load(resPath) as Texture2D);
				AssetDatabase.CreateAsset(material, materialPath);
				//UpdateAtlasesInfromation ();

			}
		}
	}
	

	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------


	private static void fillAtlasesDictionary() {

		TPAtlasesData.registerAtlases = new Dictionary<string, string>();

		string[] l = EditorPrefs.GetString(TP_ATLASES_INTFO).Split("," [0]);
		


		bool key = true;
		string AtlasName = string.Empty;
		string AtlasPath = string.Empty;
		foreach(System.Object s in l) {
			if(key) {
				AtlasName = System.Convert.ToString(s); 
			} else {
				AtlasPath = System.Convert.ToString(s);
				if(AtlasName != string.Empty && AtlasPath != string.Empty) {
					TPAtlasesData.AddAtlasInfo(AtlasName, AtlasPath);
				}

			}

			key = !key;
		}

	}
	
	
	public static string ToMacStyle(string path) {
		return path.Replace("\\", "/");
	}
		

	private static void DirSearch(string sDir) {
		try {

			DirectoryInfo dir = new DirectoryInfo(sDir);

			foreach (DirectoryInfo d in dir.GetDirectories()) {
				foreach (FileInfo f in d.GetFiles())   {
					if(f.Extension == ".txt") {

						if(f.Name.Length > 9) {
							string postfix = f.Name.Substring(f.Name.Length - 9, 9);
							if(postfix.Equals("_data.txt")) {
								
								string fullName = ToMacStyle(f.FullName);
	
								if(fullName.Contains(ResourcesFolder)) {
									int rIndex = fullName.IndexOf(ResourcesFolder) + 10;
									string atlasPath = fullName.Substring(rIndex , fullName.Length - rIndex - 9);
									
									

									//cutting data
									string name = f.Name.Substring(0, f.Name.IndexOf("_data.txt"));


									atlases.Add(name);
									atlases.Add(atlasPath);

									checkForMaterial(atlasPath, fullName.Substring(0, fullName.IndexOf("_data.txt")));

								}

							}
						}

					}
				}

				DirSearch(d.FullName);
			}


		} catch (System.Exception excpt) {
			Debug.LogWarning(excpt.Message);
		}

	}
	
	//--------------------------------------
	// DESTROY
	//--------------------------------------
}
