#if UNITY_4_6 || UNITY_5

namespace SmartLocalization.Editor
{
    using UnityEngine.UI;
    using UnityEngine;
    using UnityEditor;
    using System.Collections;

    [CustomEditor (typeof(LocalizedImage))]
    public class LocalizedImageInspector : Editor
    {
        private string selectedKey = null;

        void Awake ()
        {
            LocalizedImage textObject = ((LocalizedImage)target);
            if (textObject != null) {
                selectedKey = textObject.localizedKey;
            }
        }

        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
		
            selectedKey = LocalizedKeySelector.SelectKeyGUI (selectedKey, true, LocalizedObjectType.GAME_OBJECT);
		
            if (!Application.isPlaying && GUILayout.Button ("Use Key", GUILayout.Width (70))) {
                LocalizedImage textObject = ((LocalizedImage)target);
                textObject.localizedKey = selectedKey;
            }
        }
	
    }
}
#endif