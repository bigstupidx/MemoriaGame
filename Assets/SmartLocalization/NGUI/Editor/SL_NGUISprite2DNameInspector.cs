// SL_NGUISpriteNameInspector.cs
//
// Copyright (c) 2013-2014 Niklas Borglund, Jakob Hillerström
//

#define SMART_LOC_NGUI //<--- UNCOMMENT THIS FOR NGUI CLASSES

#if SMART_LOC_NGUI
namespace SmartLocalization.Editor
{
using UnityEngine;
using UnityEditor;

    [CustomEditor(typeof(SL_NGUISprite2DName))]
public class SL_NGUISprite2DNameInspector : Editor 
    {
private string selectedKey = null;

void Awake()
        {
            SL_NGUISprite2DName textObject = ((SL_NGUISprite2DName)target);
            if (textObject != null) {
                selectedKey = textObject.localizedKey;
            }
        }

public override void OnInspectorGUI()
        {
            base.OnInspectorGUI ();

            selectedKey = LocalizedKeySelector.SelectKeyGUI (selectedKey, true, LocalizedObjectType.GAME_OBJECT);

            if (!Application.isPlaying && GUILayout.Button ("Use Key", GUILayout.Width (70))) {
                SL_NGUISprite2DName spriteObject = ((SL_NGUISprite2DName)target);       
                spriteObject.localizedKey = selectedKey;
            }
        }
    }
} //namespace SmartLocalization.Editor
#endif

