using UnityEngine;
using System.Collections;

public class SocialLinks : MonoBehaviour {

    bool FacebookApp = false;
    bool InstragramApp = false;
    bool YoutubeApp= false;
    void Awake(){
        IOSSharedApplication.applicationIsQuitting = false;//Esto es por el problema del singleton
    }

    void OnEnable() {

        #if UNITY_IOS

        IOSSharedApplication.OnUrCheckResultAction += OnUrCheckResultAction;
        #elif UNITY_ANDROID
        AndroidNativeUtility.instance.OnPackageCheckResult += OnPackageCheckResult;


        #endif

        #if UNITY_IOS

        IOSSharedApplication.instance.CheckUrl("instagram://");
        IOSSharedApplication.instance.CheckUrl("fb://");
        #elif UNITY_ANDROID

        AndroidNativeUtility.instance.CheckIsPackageInstalled("com.google.android.youtube");
        AndroidNativeUtility.instance.CheckIsPackageInstalled("com.google.android.facebook");
        AndroidNativeUtility.instance.CheckIsPackageInstalled("com.google.android.instagram");


        #endif
    }
 

    void OnDisable(){
        #if UNITY_IOS
        IOSSharedApplication.OnUrCheckResultAction -= OnUrCheckResultAction;
        #elif UNITY_ANDROID
        //AndroidNativeUtility.instance.OnPackageCheckResult -= OnPackageCheckResult;
        #endif
    }

    public void InstagramLink(){
        #if UNITY_IOS
      //  IOSSharedApplication.instance.CheckUrl("instagram://user?username=tomi_tipi");
        if(InstragramApp){
            IOSSharedApplication.instance.OpenUrl("instagram://user?username=timisplayground");
        }else{
            IOSSharedApplication.instance.OpenUrl ("http://instagram.com/timisplayground");

        }
        #elif UNITY_ANDROID
        if(InstragramApp){
            Application.OpenURL("http://instagram.com/timisplayground");
        }else{
            Application.OpenURL("http://instagram.com/timisplayground");

        }
        #endif

    }
    public void YoutubeLink () {
        #if UNITY_IOS

        IOSSharedApplication.instance.OpenUrl( "https://www.youtube.com/user/timisplayground");

        #elif UNITY_ANDROID
        if(YoutubeApp){
            Application.OpenURL("https://www.youtube.com/user/timisplayground");
        }else{
            Application.OpenURL( "https://www.youtube.com/user/timisplayground");

        }
        #endif

    }
   
	// Update is called once per frame
	public void FacebookLink () {
        #if UNITY_IOS

        //   IOSSharedApplication.instance.CheckUrl( "fb://profile/TomiTipiOficial");
        if (FacebookApp) {
            IOSSharedApplication.instance.OpenUrl ("fb://profile/TimisPlayground");
        } else {
           

            IOSSharedApplication.instance.OpenUrl ("https://www.facebook.com/TimisPlayground");

        }
        #elif UNITY_ANDROID
        if (FacebookApp) {
            Application.OpenURL("fb://profile/TimisPlayground");
        } else {


            Application.OpenURL ("https://www.facebook.com/TimisPlayground");

        }
        #endif

    }


    #if UNITY_IOS
    private void OnUrCheckResultAction (ISN_CheckUrlResult result) {

        if(result.IsSucceeded) {
            switch(result.url){
            case "instagram://":
                InstragramApp = true;
                break;
            case "fb://":
                FacebookApp = true;
                break;
            }
          //  IOSMessage.Create("Url Exists", "The " + result.url + " is registred" );
        } else {
           // IOSMessage.Create("Url Exists", "The " + result.url + " wasn't registred");
        }
    }
    #elif UNITY_ANDROID

    void OnPackageCheckResult (AN_PackageCheckResult res) {
        if(res.IsSucceeded) {
            switch(res.packageName){
                case "com.google.android.youtube":
                    YoutubeApp = true;
                    break;
                case "com.google.android.instagram":
                    InstragramApp = true;
                    break;
                case "com.google.android.facebook":
                    FacebookApp = true;
                    break;
            }
            //  IOSMessage.Create("Url Exists", "The " + result.url + " is registred" );
        }

       // AndroidNativeUtility.instance.OnPackageCheckResult -= OnPackageCheckResult;
    }
    #endif
}
