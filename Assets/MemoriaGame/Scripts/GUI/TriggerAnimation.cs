using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour
{

    public Animator animator;
    public string triggerVar = "";

    public void SetTrigger ()
    {
    
        animator.SetTrigger (triggerVar);
    }
}
