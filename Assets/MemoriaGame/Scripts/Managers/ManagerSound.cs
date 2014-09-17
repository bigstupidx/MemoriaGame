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

    AudioSource bgAudio;

    protected override void Awake(){
        base.Awake ();
        bgAudio = GameObject.FindGameObjectWithTag ("BgSound").GetComponent<AudioSource>();
    }


    void OnEnable() {
        //Aqui cargo
        bgVolume = PlayerPrefs.GetFloat ("bgVolume");
        fxVolume = PlayerPrefs.GetFloat ("fxVolume");
        bool.TryParse(PlayerPrefs.GetString ("Mute"),out mute);

        bgAudio.volume = bgVolume;
    }

    void OnDisable() {
        //Aqui salvo
        PlayerPrefs.SetFloat ("bgVolume", bgVolume);
        PlayerPrefs.SetFloat ("fxVolume", fxVolume);
        PlayerPrefs.SetString ("Mute", mute.ToString());

    }

    public void onBGValueChangue(float value){
        bgVolume = value;

        bgAudio.volume = bgVolume;

    }
    public void onFXValueChangue(float value){
        fxVolume = value;
    }
}

