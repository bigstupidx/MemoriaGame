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
        PlayerPrefs.Save ();
        Application.LoadLevel (Application.loadedLevelName);

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
}

