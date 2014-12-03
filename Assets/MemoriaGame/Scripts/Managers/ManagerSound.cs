//
//  ManagerSound.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;

public class ManagerSound : Singleton<ManagerSound>
{
    [Range (0,1)]
    public float bgVolume = 1;
    [Range (0,1)]
    public float fxVolume = 1;


    public bool mute = false;

    public AudioSource bgAudio;
    public AudioSource bg2Audio;
    bool isNotFirstTime = false;
    protected override void AwakeChild(){
        GameObject obj = GameObject.FindGameObjectWithTag ("BgSound");
        if(bgAudio == null)
            bgAudio = obj.GetComponent<AudioSource>();
    }


    void OnEnable() {

        bool.TryParse(PlayerPrefs.GetString ("isNotFirstTime"),out isNotFirstTime);

        if (isNotFirstTime)
        {
            bgVolume = PlayerPrefs.GetFloat ("bgVolume");
            fxVolume = PlayerPrefs.GetFloat ("fxVolume");
            bool.TryParse(PlayerPrefs.GetString ("Mute"),out mute);
            isNotFirstTime = true;
            PlayerPrefs.SetString ("isNotFirstTime", isNotFirstTime.ToString());

        }
        else
        {
            bgVolume = 0.5f;
            fxVolume = 1.0f;
            mute = false;
        }
        //Aqui cargo

        if (bgAudio != null) {
            bgAudio.volume = bgVolume;

        }
        if (bg2Audio != null) {
            bg2Audio.volume = bgVolume;

        }
    }

    void OnDisable() {
        //Aqui salvo
        PlayerPrefs.SetFloat ("bgVolume", bgVolume);
        PlayerPrefs.SetFloat ("fxVolume", fxVolume);
        PlayerPrefs.SetString ("Mute", mute.ToString());

    }

    public void onBGValueChangue(float value){
        bgVolume = value;
        if (bgAudio != null) {
            bgAudio.volume = bgVolume;

        }
        if (bg2Audio != null) {
            bg2Audio.volume = bgVolume;

        }

    }
    public void onFXValueChangue(float value){
        fxVolume = value;
    }
}

