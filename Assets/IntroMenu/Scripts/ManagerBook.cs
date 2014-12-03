using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerBook : MonoBehaviour {

    public List<AnimationPage> mPages; 

    protected int currentPage = 0;


    public GameObject LeftArrow;
    public GameObject RightArrow;

    public AudioClip LeftAudio;
    public AudioClip RightAudio;

    public void Init(){
        mPages [currentPage].TurnOn ();

        if ((currentPage - 1 ) <= 0) {
            //Aqui apago la flecha
            LeftArrow.SetActive (false);
        } else if ((currentPage + 1) >= mPages.Count) {
            //Aqui apago la flecha
            RightArrow.SetActive (false);
        }
    }
    public void TurnOffIntroAnimation(GameObject intro){
        intro.SetActive(false);
    }
    public void RightPage(){
        if(!LeftArrow.activeSelf)
            LeftArrow.SetActive (true);

        if ((currentPage + 1) >= mPages.Count)
        {
            //Aqui apago la flecha
            RightArrow.SetActive (false);
        }
        else
        {
            mPages[currentPage++].ToRight();
//            Debug.Log (currentPage);
            if(currentPage+1 >= mPages.Count){
                RightArrow.SetActive (false);
            }
        }

        audio.PlayOneShot(RightAudio);
    }
    public void LeftPage(){
        if(!RightArrow.activeSelf)
            RightArrow.SetActive (true);

        if ((currentPage - 1) < 0)
        {
            //Aqui apago la flecha
            LeftArrow.SetActive (false);
        }
        else
        {

            mPages[currentPage--].ToLeft();
            if(currentPage - 1 <= 0)
                LeftArrow.SetActive (false);
        }

        audio.PlayOneShot(LeftAudio);

    }
}
