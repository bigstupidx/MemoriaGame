using UnityEngine;
using System.Collections;

public class CameraPlane : MonoBehaviour {

    public void Start(){
        Debug.Log("Initialize");

      //  Texture2D BackgroundTexture = gameObject.AddComponent<GUITexture>();
     //    BackgroundTexture.pixelInset = new Rect(0,0,Screen.width,Screen.height);
        //set up camera
        WebCamDevice[] devices = WebCamTexture.devices;
        string backCamName="";
        for( int i = 0 ; i < devices.Length ; i++ ) {
            Debug.Log("Device:"+devices[i].name+ "IS FRONT FACING:"+devices[i].isFrontFacing);

            if (!devices[i].isFrontFacing) {
                backCamName = devices[i].name;
            }
        }

        WebCamTexture CameraTexture = new WebCamTexture(backCamName,Screen.width,Screen.height,30);
        CameraTexture.Play();
        GetComponent<Renderer>().material.mainTexture= CameraTexture;

    }
}
