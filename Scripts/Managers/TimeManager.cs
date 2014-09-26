using UnityEngine;
using System.Collections;


public class TimeManager : Singleton<TimeManager>
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
        if (ManagerPause.Instance.Pause)
            yield return null;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
 //           if (ManagerPause.Instance.Pause)
  //              yield return null;

            yield return null;
        }
    }
}
