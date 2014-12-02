using UnityEngine;
using System.Collections;
using SmartLocalization;
public class InitSmartLocalization : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        string language = LanguageManager.Instance.GetSupportedSystemLanguageCode();
        if (string.IsNullOrEmpty(language))
        {
            LanguageManager.Instance.ChangeLanguage("en");
        }
        else
        {
            LanguageManager.Instance.defaultLanguage = language;
            LanguageManager.Instance.ChangeLanguage(language);
        }
	}
	
	// Update is called once per fra
}
