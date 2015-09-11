﻿using UnityEngine;
using System.Collections;

public class SavePhotoButton : MonoBehaviour
{

    public UIButton NextButton;
    public UITexture textureToSave;

    public void ErasePhoto ()
    {
    
        DestroyImmediate (textureToSave.mainTexture, true);
    }

    void OnEnable ()
    {
        #if UNITY_IOS
        //events use example
        IOSSocialManager.OnFacebookPostResult += OnPost;
        IOSSocialManager.OnTwitterPostResult += OnPost;


        //actions use example:
        IOSSocialManager.OnMailResult += OnMailResult;
        #elif UNITY_ANDROID
        #endif
    }

    void OnDisable ()
    {
        #if UNITY_IOS
        //events use example
        IOSSocialManager.OnFacebookPostResult -= OnPost;
        IOSSocialManager.OnTwitterPostResult -= OnPost;


        //actions use example:
        IOSSocialManager.OnMailResult -= OnMailResult;
        #elif UNITY_ANDROID
        #endif
    }

    public void SaveTextureToDevice ()
    {
        ((Texture2D)textureToSave.mainTexture).name = "TommyPlayground";
        NextButton.isEnabled = false;
        #if UNITY_IOS
        IOSCamera.OnImageSaved += OnImageSaved;
        IOSCamera.instance.SaveTextureToCameraRoll ((Texture2D)textureToSave.mainTexture);
        #elif UNITY_ANDROID
        AndroidCamera.instance.OnImageSaved += OnImageSaved;
        AndroidCamera.instance.SaveImageToGalalry((Texture2D)textureToSave.mainTexture);
        #endif
    }

    #if UNITY_IOS
    private void OnImageSaved (ISN_Result result)
    {
        IOSCamera.OnImageSaved -= OnImageSaved;
        NextButton.isEnabled = true;

        if (result.IsSucceeded) {
          
            IOSMessage.Create ("Success", "Image Successfully saved to Camera Roll");
        } else {
            IOSMessage.Create ("Success", "Image Save Failed");
        }
    }
    #elif UNITY_ANDROID
    private void OnImageSaved (GallerySaveResult result) {

        AndroidCamera.instance.OnImageSaved -= OnImageSaved;
    NextButton.isEnabled = true;

        if(result.IsSucceeded) {
            AN_PoupsProxy.showMessage("Saved", "Image saved to gallery \n" + "Path: " + result.imagePath);
            SA_StatusBar.text =  "Image saved to gallery";
        } else {
            AN_PoupsProxy.showMessage("Failed", "Image save to gallery failed");
            SA_StatusBar.text =  "Image save to gallery failed";
        }

    }
    #endif
    public void ShareTexture ()
    {

        #if UNITY_IOS
        IOSSocialManager.instance.ShareMedia ("Timi's Playground", (Texture2D)textureToSave.mainTexture);
        #elif UNITY_ANDROID
        AndroidSocialGate.StartShareIntent("Timi's Playground", "Timi's Playground", (Texture2D)textureToSave.mainTexture);
        #endif
    }


    private void OnPost (ISN_Result result)
    {

        //     IOSNativePopUpManager.showMessage("Posting example", "Post Succses!");
    }



    private void OnMailResult (ISN_Result result)
    {
        NextButton.isEnabled = true;

        if (result.IsSucceeded) {
            //     IOSNativePopUpManager.showMessage("Positng example", "Mail Sended");
        } else {
            //      IOSNativePopUpManager.showMessage("Positng example", "Mail Failed :(");
        }
    }
}
