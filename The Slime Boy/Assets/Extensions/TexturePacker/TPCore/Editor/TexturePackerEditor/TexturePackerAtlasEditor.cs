////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class TexturePackerAtlasEditor  {

	private int atlasIndex = -1;
	private string search = string.Empty;

	private Vector2 scrollPos;
	

	private Dictionary<string, Rect> images =  new Dictionary<string, Rect>();

	public static List<string> selection = new List<string>();


	public TexturePackerEditor editor;
	
	private TPAtlas currentAtlas = null;
	
	private float _maxY = 0f;
	
	
	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------


	public void render() {
		renderAtlasPreview ();
	}
	

	private void renderAtlasPreview() {

		string[] atlasesNames = TPAssetPostprocessor.atlasesNames;
		if(atlasesNames.Length == 0) {
			return;
		}

		#if UNITY_4_3
		#else
		
		EditorGUIUtility.LookLikeInspector();
		#endif
		
		GUILayout.BeginHorizontal(TexturePackerStyles.toolBarBoxStyle);
		GUILayout.FlexibleSpace();

		EditorGUI.BeginChangeCheck();

		if(atlasIndex == -1) {
			int i = 0;
			string n = TPEditorData.selectedAtlasName;
			foreach(string atName in atlasesNames) {
				if(atName.Equals(n)) {
					atlasIndex = i;
					break;
				}
				i++;
			}
			if(atlasIndex == -1) {
				atlasIndex = 0;
			}
		}

		atlasIndex = EditorGUI.Popup(
			new Rect(-10, 0, TexturePackerStyles.ATLAS_POPUP_WIDTH, 20),
			"",
			atlasIndex, 
			atlasesNames, TexturePackerStyles.toolBarDropDwonStyle);
		if(EditorGUI.EndChangeCheck()) {
			images.Clear ();
			selection.Clear();
			TPEditorData.selectedAtlasName = atlasesNames [atlasIndex];
		}

	
		GUIStyle btnStyle = EditorStyles.toolbarButton;

		if(GUILayout.Button("Refresh", btnStyle)) {
			TPackManager.clear();
			TPAssetPostprocessor.UpdateAtlasesInfromation();
		}

		
		if(GUILayout.Button(addButtonText, btnStyle)) {
			addSelectedFrameToAnimation();
		}
		
		
		if(TPEditorData.isExtensionsEnabled) {
			btnStyle = TexturePackerStyles.toobarEnabledButton;
		}



		if(GUILayout.Button("Extensions", btnStyle)) {
			TPEditorData.isExtensionsEnabled = !TPEditorData.isExtensionsEnabled;
		}
	
	


		search = GUILayout.TextField(search, GUI.skin.FindStyle("ToolbarSeachTextField"), TexturePackerStyles.FixedWidth(200f));


		if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
		{
			// Remove focus if cleared
			search = "";
			GUI.FocusControl(null);
		}


		GUILayout.EndHorizontal();



		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width (editor.position.width), GUILayout.Height (editor.position.height - 17f));

		GUILayout.Box("", GUIStyle.none, new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(_maxY)});
		_maxY = 0f;


		TextureNodeRenderer.calculateVars (editor.position.width - 10f);
		currentAtlas = getAtlas (atlasesNames[atlasIndex]);
		Rect r;
		int index = 0;
		images.Clear ();
		foreach(string name in currentAtlas.frameNames) {
			if(search != string.Empty) {
				if(!name.ToLower().Contains(search.ToLower())) {
					continue;
				}
			} 

			r = TextureNodeRenderer.RenderNode (index, currentAtlas, name);
			rememberImageRect (name, r);
			setNodeY(r.y + r.height * 1.5f);
			index++;

		}


		EditorGUILayout.EndScrollView();


	}
	
	
	public void setNodeY(float y) {
		if(y > _maxY) {
			_maxY = y;
		}
	}



	//--------------------------------------
	// GET / SET
	//--------------------------------------


	private TPAtlas getAtlas(string atlasName) {
		string path = TPAtlasesData.getAtlasPath(atlasName);
		return TPackManager.getAtlas(path);
	}

	private string addButtonText {
		get {
			if(Selection.activeObject is GameObject) {
				GameObject anim = Selection.activeObject as GameObject;

				TPSpriteTexture sTexture = anim.GetComponent<TPSpriteTexture>();
				if(sTexture != null) {
					return "Set Texture";
				}

				TPStaticSprite st = anim.GetComponent<TPStaticSprite>();
				if(st != null) {
					return "Set Texture";
				}


				TPSpriteAnimation sanim = anim.GetComponent<TPSpriteAnimation>();
				if(sanim != null) {
					return "Add To Animation";
				}

				TPSprite sprite = anim.GetComponent<TPSprite>();
				if(sprite != null) {
					return "Add To Animation";
				}

				TPMeshSpriteAnimation meshAnim = anim.GetComponent<TPMeshSpriteAnimation>();
				if(meshAnim != null) {
					return "Add To Animation";
				}

			} 

			return "Add To Animation";

		}
	}
	
	



	//--------------------------------------
	// EVENTS
	//--------------------------------------

	public void OnLeftMouseClick(Vector2 mousePos) {
		string image = GetClickedImageName (mousePos);
		if(!image.Equals(string.Empty)) {
			
			if(TexturePackerEditor.IsCtrlPressed) {
				if(!selection.Contains(image)) {
					selection.Add(image);	
				} else {
					selection.Remove(image);
				}
				
			} else {
				if(TexturePackerEditor.isShiftPressed) {
					if(selection.Count == 0) {
						selection.Add(image);	
					} else {
						addRangeToSelection(selection[0], image);
					}
				} else {
					selection.Clear();
					selection.Add(image);	
				}
			}
		} else {
			selection.Clear();
		}
		
		editor.Repaint ();
	}
	
	public void addRangeToSelection(string fromTex, string toTex) {
		int fromIndex = currentAtlas.IndexOfFrame(fromTex);
		int toIndex = currentAtlas.IndexOfFrame(toTex);
		
		if(fromIndex == toIndex) {
			return;
		}
		
		if(fromIndex > toIndex) {
			int temp;
			temp = fromIndex;
			fromIndex = toIndex;
			toIndex = temp;
		}
		
		selection.Clear();
		for(int i = fromIndex; i <= toIndex; i++) {
			selection.Add(currentAtlas.frameNames[i]);
		}
		
	}


	public void OnRightMouseClick(Vector2 mousePos) {

		string image = GetClickedImageName (mousePos);
		if(!image.Equals(string.Empty)) {
			
			selection.Clear();
			selection.Add(image);
			editor.Repaint ();

			GenericMenu menu = new GenericMenu ();
			menu.AddItem (new GUIContent ("Copy Name"), false, SubMenuCallBack,     TexturePackerMenuItem.COPY_TO_CLIPBOARD);
			menu.AddSeparator ("");
			menu.AddItem (new GUIContent ("Documentation"), false, SubMenuCallBack, TexturePackerMenuItem.DOCUMENTATION);
			menu.ShowAsContext ();
		} else {
			selection.Clear();
		}
		
		
		editor.Repaint ();

	}

	private void SubMenuCallBack(object obj) {
		TexturePackerMenuItem item = (TexturePackerMenuItem) obj;
		switch(item) {
		case TexturePackerMenuItem.COPY_TO_CLIPBOARD:
			EditorGUIUtility.systemCopyBuffer = selection[0];
			break;
		case TexturePackerMenuItem.DOCUMENTATION:
			Application.OpenURL ("http://goo.gl/WZpqj2");
			break;
		}
	}


	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------

	private void rememberImageRect(string name, Rect rect) {
		if(images.ContainsKey(name)) {
			images [name] = rect;
		} else {
			images.Add (name, rect);
		}
	}
	
	
	private void addSelectedFrameToAnimation() {
		
		if(selection.Equals(string.Empty)) {
			EditorUtility.DisplayDialog("Select Texture", "Select texture please", "Okay");
			return;
		}
		
		if(Selection.activeObject is GameObject) {
			GameObject anim = Selection.activeObject as GameObject;
			TPSpriteAnimation sanim = anim.GetComponent<TPSpriteAnimation>();
			if(sanim != null) {
				addFrameToSprite(sanim.sprite);
				sanim.OnEditorFrameChange();
				EditorUtility.SetDirty( sanim );
				return;
			}
			
			TPSprite sprite = anim.GetComponent<TPSprite>();
			if(sprite != null) {
				addFrameToSprite(sprite);
				EditorUtility.SetDirty( sprite.transform.parent.GetComponent<TPForceSelectedComponent>() );
				return;
			}
			
			TPMeshSpriteAnimation meshAnim = anim.GetComponent<TPMeshSpriteAnimation>();
			if(meshAnim != null) {
				addFrameToSprite(meshAnim);
				meshAnim.OnEditorFrameChange();
				EditorUtility.SetDirty( meshAnim );
				return;
			}
			
			TPSpriteTexture sTexture = anim.GetComponent<TPSpriteTexture>();
			if(sTexture != null) {
				addFrameToSprite(sTexture.sprite);
				sTexture.OnEditorFrameChange();
				EditorUtility.SetDirty( sTexture );
				return;
			}
			
		}
		
		showErrorMessage();
		
	}
	
	private void showErrorMessage() {
		EditorApplication.Beep();
		EditorUtility.DisplayDialog("Select Animation", "Please select game object with TPSpriteAnimation in scene hierarchy", "Okay");
	}
	
	private void addFrameToSprite(TPFrameHolder sprite) {
		foreach(string selectedTexture in selection) {
			sprite.addFrame(new TPFameInfo(currentAtlas.name, selectedTexture));
		}
	}



	private string GetClickedImageName(Vector2 mousePos) {
		mousePos.y += scrollPos.y;

		foreach(KeyValuePair<string, Rect> entry in images) {
			Rect r = entry.Value;

			if(r.Contains(mousePos)) {
				return entry.Key;
			}
		}

		return string.Empty;
	}

}
