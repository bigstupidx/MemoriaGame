using UnityEngine;
using System.Collections;

public class QualitySet : MonoBehaviour {

    #if UNITY_IPHONE
    void Awake(){

        if( iPhone.generation == iPhoneGeneration.iPhone4)
        {
            QualitySettings.SetQualityLevel (0);
        }

    }
    #endif
	

}
