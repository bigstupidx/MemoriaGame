using UnityEngine;
using System.Collections;

public class TakeShoot : MonoBehaviour {


    public UITexture target;
    bool isTaking = false;
    Texture initialTexture;
    #if UNITY_EDITOR
    public Texture initialTextureExample;

    #endif
    public UIButton next;
    void Awake(){
        initialTexture = target.mainTexture;
    }
    /// <summary>
    /// Next button desactivar
    /// </summary>
    public void setIsEnableFalse(){
    
        next.isEnabled = false;
    }
    private void OnImageSaved (ISN_Result result) {
        IOSCamera.instance.OnImageSaved -= OnImageSaved;
        if(result.IsSucceeded) {
            IOSMessage.Create("Success", "Image Successfully saved to Camera Roll");
        } else {
            IOSMessage.Create("Success", "Image Save Failed");
        }
    }
    #if UNITY_IOS
    private void OnImage (IOSImagePickResult result) {
        if (result.IsSucceeded)
        {
            if(target.mainTexture != initialTexture)
                DestroyImmediate(target.mainTexture,true);
            target.mainTexture = result.image;
            next.isEnabled = true;

         //   IOSMessage.Create("Success", "Image Successfully Loaded, Image size: " + result.image.width + "x" + result.image.height);

        }
        else
        {
            IOSMessage.Create("Success", "Image Load Failed");

        }
        isTaking = false;
        IOSCamera.instance.OnImagePicked -= OnImage;
    }

    #elif UNITY_ANDROID
    private void OnImage(AndroidImagePickResult result) {

        if (result.IsSucceeded)
        {
            if(target.mainTexture != initialTexture)
                DestroyImmediate(target.mainTexture,true);
            target.mainTexture = result.image;
            next.isEnabled = true;

        }
        isTaking = false;
        AndroidCamera.instance.OnImagePicked -= OnImage;
    }
    #endif
    public void TakePhotoButton(){
        #if UNITY_EDITOR
        target.mainTexture =initialTextureExample;
        next.isEnabled = true;

        return;
        #endif
        if (!isTaking)
        {
            isTaking = true;
            #if UNITY_IOS
            IOSCamera.instance.OnImagePicked += OnImage;
            IOSCamera.instance.GetImageFromCamera();
            #elif UNITY_ANDROID
            AndroidCamera.instance.OnImagePicked += OnImage;
            AndroidCamera.instance.GetImageFromCamera();
            #endif
           
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
