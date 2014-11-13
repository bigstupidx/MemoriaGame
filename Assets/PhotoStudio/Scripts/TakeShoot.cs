using UnityEngine;
using System.Collections;

public class TakeShoot : MonoBehaviour {


    public UITexture target;
    Camera cam;
    void Awake(){
        cam = camera;
        cam.CopyFrom (Camera.main);

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
            Destroy(target.mainTexture);
            target.mainTexture = result.image;
            isTaking = false;
            IOSMessage.Create("Success", "Image Successfully Loaded, Image size: " + result.image.width + "x" + result.image.height);

        }
        else
        {
            IOSMessage.Create("Success", "Image Load Failed");

        }
        IOSCamera.instance.OnImagePicked -= OnImage;
    }
    private IEnumerator TakeScreenshot()
    {
        #if UNITY_EDITOR

        yield return new WaitForEndOfFrame();
        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
        // Read screen contents into the texture
        tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
        tex.Apply();
        Destroy( target.mainTexture);

        target.mainTexture = tex;
        /*
        Destroy(tex);

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = rt;

        Texture2D screenShot = new Texture2D(Screen.width,Screen.height, TextureFormat.ARGB32, false);
        RenderTexture.active = rt;

        cam.Render();

        yield return new WaitForEndOfFrame();
        screenShot.ReadPixels(new Rect(  0,0, Screen.width,  Screen.height),0,0);
        screenShot.Apply ();

        target.renderer.material.mainTexture = screenShot;

        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);*/
        #else

        yield return null;

        #endif


    }
    public void TakePhotoButton(){
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
    bool isTaking = false;
    void Update(){
    
        if ((Input.GetKeyDown (KeyCode.T) || Input.GetMouseButtonDown(0)) && !isTaking) {
 

            #if UNITY_EDITOR
            StartCoroutine (TakeScreenshot());
            #else
   
            #endif

        }
    }
}
