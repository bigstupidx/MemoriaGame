using UnityEngine;
using System.Collections;

public class QualitySet : MonoBehaviour {

    #if UNITY_IPHONE
    void Awake(){

        if( UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone4)
        {
            QualitySettings.SetQualityLevel (0);
        }

    }
    #endif
	

}
