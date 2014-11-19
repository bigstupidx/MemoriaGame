using UnityEngine;
using System.Collections;

public class SavePhotoButton : MonoBehaviour {

    public UITexture textureToSave;
    void Awake(){
        //events use example
        IOSSocialManager.instance.addEventListener(IOSSocialManager.FACEBOOK_POST_SUCCESS, OnPostSuccses);
        IOSSocialManager.instance.addEventListener(IOSSocialManager.TWITTER_POST_SUCCESS, OnPostSuccses);


        IOSSocialManager.instance.addEventListener(IOSSocialManager.FACEBOOK_POST_FAILED, OnPostFailed);
        IOSSocialManager.instance.addEventListener(IOSSocialManager.TWITTER_POST_FAILED, OnPostFailed);

        //actions use example:
        IOSSocialManager.instance.OnMailResult += OnMailResult;
    }
        

    public void SaveTextureToDevice(){
    
        IOSCamera.instance.OnImageSaved += OnImageSaved;
        IOSCamera.instance.SaveTextureToCameraRoll((Texture2D)textureToSave.mainTexture);
    }

    private void OnImageSaved (ISN_Result result) {
        IOSCamera.instance.OnImageSaved -= OnImageSaved;
        if(result.IsSucceeded) {
            IOSMessage.Create("Success", "Image Successfully saved to Camera Roll");
        } else {
            IOSMessage.Create("Success", "Image Save Failed");
        }
    }

    public void ShareTexture(){
    
        IOSSocialManager.instance.ShareMedia("Tomi Playground ", (Texture2D)textureToSave.mainTexture);

    }

    private void OnPostFailed() {
        IOSNativePopUpManager.showMessage("Positng example", "Post Failed :(");
    }

    private void OnPostSuccses() {
        IOSNativePopUpManager.showMessage("Positng example", "Posy Succses!");
    }



    private void OnMailResult (ISN_Result result) {
        if(result.IsSucceeded) {
            IOSNativePopUpManager.showMessage("Positng example", "Mail Sended");
        } else {
            IOSNativePopUpManager.showMessage("Positng example", "Mail Failed :(");
        }
    }
}
