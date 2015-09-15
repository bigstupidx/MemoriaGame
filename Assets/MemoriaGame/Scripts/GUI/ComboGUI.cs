using UnityEngine;
using System.Collections;



public class ComboGUI : MonoBehaviour
{

    public Animator anim;
    public AudioSource audio;
    bool isCombo = false;

    int lastCombo = 0;

    void LateUpdate ()
    {
        switch (ManagerCombo.Instance.GetCombo) {
        case 1:
        case 0:

            if (isCombo) {
                anim.SetTrigger ("Dismiss");
            }
            isCombo = false;
            break;
        case 2:
            if (lastCombo != 2) {
                anim.SetTrigger ("Combo_2");
                audio.Play ();
            }
            isCombo = true;
            break;
        case 3:
            if (lastCombo != 3) {
                anim.SetTrigger ("Combo_3");
                audio.Play ();
            }
            isCombo = true;
            break;
        case 4:
            if (lastCombo != 4) {
                anim.SetTrigger ("Combo_4");
                audio.Play ();

            }
            isCombo = true;
            break;
        default:
            if (lastCombo != ManagerCombo.Instance.GetCombo) {
                anim.SetTrigger ("Combo_Final");
                audio.Play ();
            }
            isCombo = true;
            break;
        }

        lastCombo = ManagerCombo.Instance.GetCombo;
    }
}
