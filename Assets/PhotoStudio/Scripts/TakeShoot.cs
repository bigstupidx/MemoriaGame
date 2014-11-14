using UnityEngine;
using System.Collections;

public class TakeShoot : MonoBehaviour {


    public UITexture target;
    bool isTaking = false;
    Texture initialTexture;
    #if UNITY_EDITOR
    public Texture initialTextureExample;

    #endif
    void Awake(){
        initialTexture = target.mainTexture;
    }

    private void OnImageSaved (ISN_Result result) {
        IOSCamera.instance.OnImageSaved -= OnImageSaved;
        if(result.IsSucceeded) {
            IOSMessage.Create("Success", "Image Successfully saved to Camera Roll");
        } else {
            IOSMessage.Create("Success", "Image Save Failed");
        }
    }
    private void OnImage (IOSImagePickResult result) {
        if (result.IsSucceeded)
        {
            if(target.mainTexture != initialTexture)
                DestroyImmediate(target.mainTexture,true);
            target.mainTexture = result.image;
            IOSMessage.Create("Success", "Image Successfully Loaded, Image size: " + result.image.width + "x" + result.image.height);

        }
        else
        {
            IOSMessage.Create("Success", "Image Load Failed");

        }
        isTaking = false;
        IOSCamera.instance.OnImagePicked -= OnImage;
    }

    public void TakePhotoButton(){
        #if UNITY_EDITOR
        target.mainTexture =initialTextureExample;
        return;
        #endif
        if (!isTaking)
        {
            isTaking = true;
            IOSCamera.instance.OnImagePicked += OnImage;
            IOSCamera.instance.GetImageFromCamera();
           
        }
    }
    public void SavePhotoButton(){
        IOSCamera.instance.OnImageSaved += OnImageSaved;
        IOSCamera.instance.SaveScreenshotToCameraRoll();
    }

    public void BorrarFoto(){
        #if UNITY_EDITOR
        target.mainTexture = initialTexture;
        return;
    #endif
        if(target.mainTexture != initialTexture)
            DestroyImmediate(target.mainTexture,true);
        target.mainTexture = initialTexture;
    }

}
