using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerBook : MonoBehaviour {

    public List<AnimationPage> mPages; 

    protected int currentPage = 0;

    public void Init(){
        mPages[currentPage].TurnOn();
    }
    public void TurnOffIntroAnimation(GameObject intro){
        intro.SetActive(false);
    }
    public void RightPage(){
    
        if ((currentPage + 1) >= mPages.Count)
        {
            //Aqui apago la flecha
        }
        else
        {
            mPages[currentPage++].ToRight();
        }
    }
    public void LeftPage(){

        if ((currentPage - 1) < 0)
        {
            //Aqui apago la flecha
        }
        else
        {
            mPages[currentPage--].ToLeft();
        }
    }
}
