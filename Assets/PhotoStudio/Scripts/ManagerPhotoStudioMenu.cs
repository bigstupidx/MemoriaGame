using UnityEngine;
using System.Collections;

public class ManagerPhotoStudioMenu : Singleton<ManagerPhotoStudioMenu> {

    public UIWidget intro;
    public UIWidget selectPlantilla;
    public UIWidget takePhoto;
    public UIWidget mergePhoto;
    public UIWidget galeria;

    UIWidget current;

    void Awake(){
        current = intro;
    }
    public void GoToHome(){

        current.alpha = 0;
        intro.alpha = 1;
        current = intro;
    }
    public void GoToGaleria(){

        current.alpha = 0;
        galeria.alpha = 1;
        current = galeria;
    }  
    public void GoToSelectPlantilla(){
    
        current.alpha = 0;
        selectPlantilla.alpha = 1;
        current = selectPlantilla;
    }

    public void GoToTakePhoto(){

        current.alpha = 0;
        takePhoto.alpha = 1;
        current = takePhoto;


    }

    public void GoToPreview(){

        current.alpha = 0;
        mergePhoto.alpha = 1;
        current = mergePhoto;


    }
}
