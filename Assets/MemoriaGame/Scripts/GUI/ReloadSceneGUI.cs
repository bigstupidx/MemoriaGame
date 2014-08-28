using UnityEngine;
using System.Collections;

public class ReloadSceneGUI : MonoBehaviour {

    public UI2DSprite spriteBG;

    public UI2DSpriteAnimation spriteAnim;
    public UI2DSpriteAnimation spriteAnimNo;

    public UIWidget objectFunc;


    bool isPressedNo = false;
    // Use this for initialization
    public  void Reload () {

        spriteBG.alpha = 1;

        spriteAnim.ResetToBeginning ();

        spriteAnim.Play ();


    }

    public void ReloadYes(){
        Application.LoadLevel (Application.loadedLevelName);

    }

    public void ReloadNo(){

        spriteAnimNo.ResetToBeginning ();
        spriteAnimNo.Play ();

        objectFunc.alpha = 0;


        isPressedNo = true;
    }

    void Update(){
    
        if (spriteBG.alpha > 0) {
        
            if (isPressedNo) {
                if (!spriteAnimNo.isPlaying) {
                    spriteBG.alpha = 0;
                    isPressedNo = false;
                }
            }else if (!spriteAnim.isPlaying) {
                objectFunc.alpha = 1;
            }
        }
    }
}

