using UnityEngine;
using System.Collections;

public class VideoCameraRender : Singleton<VideoCameraRender> {

    public string deviceName;
    public UITexture uiTexture = null;
    [HideInInspector]
    public WebCamTexture webcamTexture;
    WebCamDevice[] devices;

    void Start () {


        devices = WebCamTexture.devices;
        deviceName = devices[0].name;
        webcamTexture = new WebCamTexture(deviceName,(int)uiTexture.localSize.x, (int)uiTexture.localSize.y,12);

        uiTexture.mainTexture = webcamTexture;

        webcamTexture.Play();

    }

    void Front(){
        int t = 0;
        bool front_facing = false;

        while(!front_facing && t < devices.Length){
            deviceName = devices[t].name;
            front_facing = devices[t].isFrontFacing;
            t++;
        }
    }


}
