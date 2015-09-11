#if UNITY_4_6 || UNITY_5
namespace SmartLocalization.Editor
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    [RequireComponent (typeof(Image))]
    public class LocalizedImage : MonoBehaviour
    {
        public string localizedKey = "INSERT_KEY_HERE";
        Image imageObject;

        void Start ()
        {
            imageObject = this.GetComponent<Image> ();
	
            //Subscribe to the change language event
            LanguageManager languageManager = LanguageManager.Instance;
            languageManager.OnChangeLanguage += OnChangeLanguage;
		
            //Run the method one first time
            OnChangeLanguage (languageManager);
        }

        void OnDestroy ()
        {
            if (LanguageManager.HasInstance) {
                LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
            }
        }

        void OnChangeLanguage (LanguageManager languageManager)
        {
            GameObject textur = LanguageManager.Instance.GetPrefab (localizedKey);
            imageObject.sprite = textur.GetComponent<SpriteRenderer> ().sprite;
        }
    }
}
#endif