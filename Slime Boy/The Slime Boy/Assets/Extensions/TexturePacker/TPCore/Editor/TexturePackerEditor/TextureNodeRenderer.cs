////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;

public class TextureNodeRenderer  {

	public static int colItemsCount;
	private static float itemsSpace;

	private static float box_size;
	private static float box_padding;



	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------

	public static Rect RenderNode(int index, TPAtlas antlas, string name) {

		Vector2 itemPos = new Vector2 ();
		itemPos.y =  25f;

		if(index + 1 > colItemsCount) {
			itemPos.y += Mathf.FloorToInt(index / colItemsCount) * TexturePackerStyles.TEXTURE_RECT_SIZE * 1.7f;
			index = index % colItemsCount;
		}

		itemPos.x = itemsSpace / 2f + (index) * (itemsSpace + box_size);






		GUILayout.BeginArea (new Rect (itemPos.x, itemPos.y  , box_size, box_size * 2f), ""); {


			if(TexturePackerAtlasEditor.selection.Contains(name)) {
				drawColordeBox (box_size, box_size, Color.green);
			/*	GUILayout.BeginArea (new Rect (box_padding, box_padding  , TexturePackerStyles.TEXTURE_RECT_SIZE, TexturePackerStyles.TEXTURE_RECT_SIZE), ""); {
					drawColordeBox (TexturePackerStyles.TEXTURE_RECT_SIZE, TexturePackerStyles.TEXTURE_RECT_SIZE);
				} GUILayout.EndArea ();
				*/

			} else {
				drawColordeBox (box_size, box_size);
			}


			TPAtlasTexture tx = antlas.getTexture (name);
			tx.draw (getSize(tx), true);

		
			string texNmae = string.Empty;

			if(TPEditorData.isExtensionsEnabled) {
				texNmae = tx.name;
			} else {
				texNmae = tx.nameNoExtention;
			}


			string imageInfo = texNmae + "\n(" + tx.width + "x" + tx.height + ")";
			GUILayout.Label (imageInfo, TexturePackerStyles.imageLableStyle, TexturePackerStyles.FixedWidthHeight(110f, 40f));


		} GUILayout.EndArea ();



		return new Rect (itemPos.x, itemPos.y, box_size, box_size);

	}




	public static void calculateVars(float width) {

		box_padding = TexturePackerStyles.TEXTURE_RECT_SIZE * 0.05f;
		box_size = TexturePackerStyles.TEXTURE_RECT_SIZE + box_padding * 2f;

		//Debug.Log (box_size);

		itemsSpace = width * 0.8f;

		float spaces = width - itemsSpace;

		float it = itemsSpace / box_size;
		colItemsCount = Mathf.FloorToInt(it);

		float des = itemsSpace - colItemsCount * box_size;
		spaces += des;

		itemsSpace =  spaces / (colItemsCount);
	}


	private static void drawColordeBox(float w, float h) {
		drawColordeBox (w, h, GUI.color);
	}

	private static void drawColordeBox(float w, float h, Color c) {

		Color guiColor = GUI.color;
		GUI.color = c;
		GUILayout.Box("", new GUILayoutOption[] { GUILayout.Width(w) , GUILayout.Height(h) } );

		GUI.color = guiColor;
	}


	private static Rect getSize(TPAtlasTexture tx) {
		Rect r = new Rect ();

		float biggestSide;
		if(tx.width > tx.height) {
			biggestSide = tx.width;
		} else {
			biggestSide = tx.height;
		}


		float scale = TexturePackerStyles.TEXTURE_RECT_SIZE / biggestSide;

		r.width = tx.width * scale;
		r.height = tx.height * scale;
		
	
		r.x = (TexturePackerStyles.TEXTURE_RECT_SIZE - r.width) / 2f  + box_padding;
		r.y = (TexturePackerStyles.TEXTURE_RECT_SIZE - r.height) / 2f + box_padding;
	

		return r;
	}

}
