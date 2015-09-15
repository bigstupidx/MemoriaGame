using UnityEngine;
using System.Collections;

public class ReloadSceneGUI : MonoBehaviour
{

    public Animator spriteAnim;


    #region StartButton

    // Use this for initialization
    public  void Reload ()
    {
        spriteAnim.SetBool ("Reload", true);
    }

    public void ReloadNo ()
    {
        spriteAnim.SetBool ("Reload", false);
    }

    public void SoundTouch ()
    {
        spriteAnim.SetBool ("Sound", true);
    }

    public void SoundTouchEnd ()
    {
        spriteAnim.SetBool ("Sound", false);
    }

    public void TutorialTouch ()
    {
        spriteAnim.SetBool ("Tutorial", true);
    }

    public void TutorialTouchEnd ()
    {
        spriteAnim.SetBool ("Tutorial", false);
    }


    #endregion


    #region BackButon:

    public void ReloadYes ()
    {
        //CAMIBAR
        PlayerPrefs.Save ();
        LoadLevelManager.Instance.RestartCurrentLevel ();
    }

    public void Home ()
    {
        //CAMIBAR
        PlayerPrefs.Save ();
        LoadLevelManager.Instance.LoadLevelWithLoadingScene ("HomeScene");
    }

    #endregion

}

