////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;
using System.Collections;

public class TexturePackerStyles  {

	private static GUIStyle _toolBarBoxStyle = null;
	private static GUIStyle _toolBarDropDwonStyle = null;
	private static GUIStyle _imageLableStyle = null;
	private static GUIStyle _toobarEnabledButton = null;



	//--------------------------------------
	// CONFIG
	//--------------------------------------

	public const float ATLAS_POPUP_WIDTH = 150f;

	public const float TEXTURE_RECT_SIZE = 100f;

	public const float TOOLBAR_HEIGHT = 25f;

	//--------------------------------------
	// GET / SET
	//--------------------------------------
	

	public static GUIStyle toolBarBoxStyle {
		get {
			if(_toolBarBoxStyle ==  null) {
				_toolBarBoxStyle = new GUIStyle(EditorStyles.toolbar);
				_toolBarBoxStyle.fixedHeight = 19f;
			}

			return _toolBarBoxStyle;
		}
	}


	public static GUIStyle toolBarDropDwonStyle {
		get {
			if(_toolBarDropDwonStyle ==  null) {
				_toolBarDropDwonStyle = new GUIStyle(EditorStyles.toolbarPopup);
				_toolBarDropDwonStyle.alignment = TextAnchor.MiddleCenter;
			}

			return _toolBarDropDwonStyle;
		}
	}

	public static GUILayoutOption[] FixedWidth(float w) {
		return new GUILayoutOption[] { GUILayout.Width(w) , GUILayout.ExpandHeight(true) };
	}

	public static GUILayoutOption[] FixedWidthHeight(float w, float h) {
		return new GUILayoutOption[] { GUILayout.Width(w) , GUILayout.Height(h) };
	}

	



	public static GUIStyle toobarEnabledButton {
		get {
			if(_toobarEnabledButton ==  null) {
				_toobarEnabledButton = new GUIStyle(EditorStyles.toolbarButton);
				_toobarEnabledButton.normal.background = EditorStyles.toolbarButton.onNormal.background;
				_toobarEnabledButton.active.background = EditorStyles.toolbarButton.onActive.background;
			}

			return _toobarEnabledButton;
		}

	}

	public static GUIStyle imageLableStyle {
		get {
			if(_imageLableStyle ==  null) {
				_imageLableStyle = new GUIStyle(EditorStyles.label);
				_imageLableStyle.alignment = TextAnchor.UpperCenter;
				_imageLableStyle.wordWrap = true;
			}

			return _imageLableStyle;
		}

	}



}
