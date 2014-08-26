using UnityEngine;
using System.Collections;



public class ComboGUI : MonoBehaviour {

    public UI2DSpriteAnimation dismiss;

    public UI2DSpriteAnimation combo2;

    public UI2DSpriteAnimation combo3;

    public UI2DSpriteAnimation combo4;

    UI2DSprite sprite;
    bool isCombo = false;
    bool playOnce = false;
    void Awake(){
        sprite = GetComponent<UI2DSprite> ();
    }

    void LateUpdate(){
        switch (ManagerCombo.Instance.GetCombo) {
        case 1:
        case 0:

            if (isCombo) {
                dismiss.Play ();
                TweenAlpha.Begin (gameObject, 0.3f, 0);
            }
            isCombo = false;
            playOnce = false;
            break;
        case 2:
            if (!combo2.isPlaying && !playOnce) {
                TweenAlpha.Begin (gameObject, 0.3f, 1);

                combo2.Play ();
                playOnce = true;
            }
            isCombo = true;
            break;
        case 3:
            if (!combo3.isPlaying && playOnce) {
                combo3.Play ();
                playOnce = false;
            }
            isCombo = true;
            break;
        case 4:
            if (!combo4.isPlaying && !playOnce) {
                combo4.Play ();
                playOnce = true;
            }
            isCombo = true;
            break;
        default:
            isCombo = true;
            break;
        }


    }
}
