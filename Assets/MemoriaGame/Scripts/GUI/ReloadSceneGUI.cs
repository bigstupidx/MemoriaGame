using UnityEngine;
using System.Collections;

public class ReloadSceneGUI : MonoBehaviour {

    public UI2DSprite spriteBG;

    public UI2DSpriteAnimation spriteAnim;
    public UIWidget objectFunc;

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
        spriteBG.alpha = 0;
        objectFunc.alpha = 0;
    }

    void Update(){
    
        if (spriteBG.alpha > 0) {
        
            if (!spriteAnim.isPlaying) {
                objectFunc.alpha = 1;
            }
        }
    }
}

