using UnityEngine;
using System.Collections;

public class TakeShoot : MonoBehaviour {


    public Renderer target;
    Camera cam;
    void Awake(){
        cam = camera;
        cam.CopyFrom (Camera.main);
    }
    private IEnumerator TakeScreenshot()
    {/*
        Texture2D image = new Texture2D(VideoCameraRender.Instance.webcamTexture.width, VideoCameraRender.Instance.webcamTexture.height); 

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        cam.Render();

        yield return new WaitForEndOfFrame();

        image.ReadPixels(new Rect(0, 0, VideoCameraRender.Instance.webcamTexture.width,VideoCameraRender.Instance.webcamTexture.height), 0, 0);
        //image.SetPixels(VideoCameraRender.Instance.webcamTexture.GetPixels());
        image.Apply();

        RenderTexture.active = currentRT;

        target.renderer.material.mainTexture = image;
        */
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
        Destroy(rt);

        Debug.Log("Capture!!");

    }

    void Update(){
    
        if (Input.GetKeyDown (KeyCode.T) || Input.GetMouseButtonDown(0)) {
            StartCoroutine (TakeScreenshot());
        }
    }
}
