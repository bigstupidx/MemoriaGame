using UnityEngine;
using System.Collections;

public class ManagerPhotoStudioMenu : Singleton<ManagerPhotoStudioMenu> {

    public UIWidget intro;
    public UIWidget selectPlantilla;
    public UIWidget takePhoto;
    public UIWidget mergePhoto;
    public UIWidget galeria;
    public UIWidget edit;

    UIWidget current;

    void Awake(){
        current = intro;
    }
    public void GoToHome(){
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
        current.alpha = 0;
        intro.alpha = 1;
        current = intro;
    }
    public void GoToGaleria(){
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Pause();
        current.alpha = 0;
        galeria.alpha = 1;
        current = galeria;
    }  
    public void GoToSelectPlantilla(){
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Pause();
        current.alpha = 0;
        selectPlantilla.alpha = 1;
        current = selectPlantilla;
    }

    /// <summary>
    /// Cambia a la escena de tomar la foto
    /// </summary>
    public void GoToTakePhoto(){
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Pause();
        current.alpha = 0;
        takePhoto.alpha = 1;
        current = takePhoto;


    }

    public void GoToPreview(){
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Pause();
        current.alpha = 0;
        mergePhoto.alpha = 1;
        current = mergePhoto;


    }
    public void GoToPreviewWithCondition(GameObject obj){

        if (obj.GetComponent<UITexture>().mainTexture != null)
        {
            GoToPreview();
        }
        else
        {
            GoToHome();
        }


    }

    public void GoToEdit(){
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Pause();
        current.alpha = 0;
        edit.alpha = 1;
        current = edit;


    }
}
