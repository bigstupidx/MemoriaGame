// SL_NGUISpriteName.cs
//
// Copyright (c) 2013-2014 Niklas Borglund, Jakob Hillerström
//

#define SMART_LOC_NGUI //<--- UNCOMMENT THIS FOR NGUI CLASSES

#if SMART_LOC_NGUI
using UnityEngine;
using System.Collections;
using SmartLocalization;

public class SL_NGUISprite2DName : MonoBehaviour 
{
    public string localizedKey = "INSERT KEY HERE";
    UI2DSprite uiSprite = null;

    void Awake()
    {
        uiSprite = GetComponent<UI2DSprite>();
    }

    void Start () 
    {
        //Subscribe to the change language event
        LanguageManager thisLanguageManager = LanguageManager.Instance;
        thisLanguageManager.OnChangeLanguage += OnChangeLanguage;

        OnChangeLanguage(thisLanguageManager);
    }

     void OnDestroy()
    {
        if (LanguageManager.HasInstance)
            LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
    }

    void OnChangeLanguage(LanguageManager thisLanguageManager)
    {
        Sprite atlasPrefab = thisLanguageManager.GetPrefab (localizedKey).GetComponent<SpriteRenderer>().sprite;
        if (atlasPrefab != null) {
            uiSprite.sprite2D = atlasPrefab;
        }
    }     
}
#endif
