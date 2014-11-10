using UnityEngine;
using System.Collections;

public class TakeShoot : MonoBehaviour {


    public Renderer target;
    private IEnumerator TakeScreenshot()
    {
        Camera cam = Camera.main;
        Debug.Log (cam.name);
        Texture2D image = new Texture2D(Screen.width, Screen.height); 

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture.active = cam.targetTexture;
        cam.Render();

        yield return new WaitForEndOfFrame();

        image.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        //Resize the image. Useful if you don't need a 1:1 screenshot.
        //4 is just used as an example. You could use 10 to resize it
        //to a tenth of the original scale or whatever floats your boat.
      //  if (resizePhotos)
     //       TextureScale.Bilinear(image, image.width / 4, image.height / 4);

        image.Apply();
        RenderTexture.active = currentRT;

        target.renderer.material.mainTexture = image;

    }

    void Update(){
    
        if (Input.GetKeyDown (KeyCode.T)) {
            StartCoroutine (TakeScreenshot());
        }
    }
}
