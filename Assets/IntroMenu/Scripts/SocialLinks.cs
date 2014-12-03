using UnityEngine;
using System.Collections;

public class SocialLinks : MonoBehaviour {

    bool FacebookApp = false;
    bool InstragramApp = false;
//    bool YoutubeApp= false;
    void Awake() {

        #if UNITY_IOS

        IOSSharedApplication.OnUrCheckResultAction += OnUrCheckResultAction;


        #endif
    }
    void Start(){
        #if UNITY_IOS

        IOSSharedApplication.instance.CheckUrl("instagram://");
        IOSSharedApplication.instance.CheckUrl("fb://");

        #endif

    }

    void OnDisable(){
        #if UNITY_IOS
        IOSSharedApplication.OnUrCheckResultAction -= OnUrCheckResultAction;
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
        #endif

    }
    public void YoutubeLink () {
        #if UNITY_IOS

       // IOSSharedApplication.instance.CheckUrl( "https://www.youtube.com/user/TomiTipiOficial");

        IOSSharedApplication.instance.OpenUrl( "https://www.youtube.com/user/timisplayground");
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
    #endif
}
