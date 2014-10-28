using UnityEngine;
using System.Collections;


public class TimeCallBacks
{
    public IEnumerator Wait(float duration)
    {


        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {

            yield return null;
        }
    }
    public IEnumerator WaitPause(float duration)
    {

        float timer = 0;
        while(timer < duration){
           
           
            yield return null;
            if (ManagerPause.Instance.Pause == false) {
                timer += Time.deltaTime;
            }
           
        }

    }
}
