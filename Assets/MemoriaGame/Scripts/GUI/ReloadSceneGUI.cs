using UnityEngine;
using System.Collections;

public class ReloadSceneGUI : MonoBehaviour {

    public UI2DSprite spriteBG;

    public UI2DSpriteAnimation spriteAnim;
    public UI2DSpriteAnimation spriteAnimNo;

    public UIWidget objectFunc;
    public UIWidget objectSound;


    bool isPressedNo = false;

    bool reset = false;
    bool sound  = false;

    public GameObject loadPrefab;

    void activeBG(){
        spriteBG.alpha = 1;

        spriteAnim.ResetToBeginning ();

        spriteAnim.Play ();

    }
    #region StartButton
    // Use this for initialization
    public  void Reload () {

        activeBG ();
        reset = true;

    }
    public void SoundTouch(){
        activeBG ();

        sound = true;
    }
    #endregion

    public void ReloadYes(){
        //CAMIBAR
        loadPrefab.SetActive (true);
        PlayerPrefs.Save ();
        LoadLevelManager.Instance.RestartCurrentLevel ();

    }
    public void Home(){
        loadPrefab.SetActive (true);

        //CAMIBAR
        PlayerPrefs.Save ();
        LoadLevelManager.Instance.LoadLevel ("HomeScene");
    }
    #region BackButon:
    public void ReloadNo(){

        spriteAnimNo.ResetToBeginning ();
        spriteAnimNo.Play ();

        objectFunc.alpha = 0;

        isPressedNo = true;
    }

 
    public void SoundTouchEnd(){
        spriteAnimNo.ResetToBeginning ();
        spriteAnimNo.Play ();

        objectSound.alpha = 0;

        isPressedNo = true;


    }
    #endregion
    void Update(){
    
        if (spriteBG.alpha > 0) {
        
            if (reset) {
                ResetUpdate ();
            } else if (sound) {
                SoundUpdate ();
            }else if(tutorial){
                TutorialUpdate ();
            }
        }
    }
    void SoundUpdate(){
        if (isPressedNo) {
            if (!spriteAnimNo.isPlaying) {
                spriteBG.alpha = 0;
                isPressedNo = false;
                sound = false;
            }
        }
        else if (!spriteAnim.isPlaying) {
            objectSound.alpha = 1;
        }
    }
    void ResetUpdate(){
        if (isPressedNo) {
            if (!spriteAnimNo.isPlaying) {
                spriteBG.alpha = 0;
                isPressedNo = false;
                reset = false;

            }
        }
        else if (!spriteAnim.isPlaying) {
            objectFunc.alpha = 1;
        }
    }


    public UIWidget objectTutorial;
    bool tutorial = false;

    public void TutorialTouch(){
        activeBG ();
        tutorial = true;
    }
    public void TutorialTouchEnd(){
        spriteAnimNo.ResetToBeginning ();
        spriteAnimNo.Play ();

        objectTutorial.alpha = 0;

        isPressedNo = true;
    }
    void TutorialUpdate(){
        if (isPressedNo) {
            if (!spriteAnimNo.isPlaying) {
                spriteBG.alpha = 0;
                isPressedNo = false;
                tutorial = false;

            }
        }
        else if (!spriteAnim.isPlaying) {
            objectTutorial.alpha = 1;
        }
    }

}

