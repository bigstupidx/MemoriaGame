using UnityEngine;
using System.Collections;



public class ComboGUI : MonoBehaviour {

    public UI2DSpriteAnimation dismiss;

    public UI2DSpriteAnimation combo2;

    public UI2DSpriteAnimation combo3;

    public UI2DSpriteAnimation combo4;

    public UI2DSpriteAnimation comboFinal;

    bool isCombo = false;

    int lastCombo = 0;
   
    void ScaleNormal(){
        TweenScale.Begin (gameObject, 0.1f, new Vector3 (1, 1, 1));

    }
    void ScaleBig(){
        TweenScale.Begin(gameObject,0.1f,new Vector3(1.1f,1.1f,1.1f)).AddOnFinished(new EventDelegate(this,"ScaleNormal")) ;


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
            break;
        case 2:
            if (!combo2.isPlaying && lastCombo != 2) {
                TweenAlpha.Begin (gameObject, 0.3f, 1);
                ScaleBig ();
                combo2.Play ();
                audio.Play ();
            }
            isCombo = true;
            break;
        case 3:
            if (!combo3.isPlaying && lastCombo != 3) {
                ScaleBig ();

                combo3.Play ();
                audio.Play ();

            }
            isCombo = true;
            break;
        case 4:
            if (!combo4.isPlaying && lastCombo != 4) {
                ScaleBig ();

                combo4.Play ();
                audio.Play ();

            }
            isCombo = true;
            break;
        default:
            if (!comboFinal.isPlaying && lastCombo != ManagerCombo.Instance.GetCombo) {
                ScaleBig ();

                comboFinal.Play ();
                audio.Play ();

            }
            isCombo = true;
            break;
        }

        lastCombo = ManagerCombo.Instance.GetCombo;
    }
}
