using UnityEngine;
using System.Collections;

public class GUI_SetFotoMarco : MonoBehaviour {

    public UITexture Foto;
    public UITexture TargetFoto;

    public UICenterOnChild Marco;
  //  public UI2DSprite TargetMarco;

    public GUI_GaleriaManager galeria;

  //  public Texture2D BGFoto;

    public UIButton BackButton;
    public void SetAll(){
        TargetFoto.mainTexture = Foto.mainTexture;
        string marco_Name = Marco.centeredObject.GetComponent<UISprite>().spriteName;

        Texture2D marquito = (Texture2D)Resources.Load("Plantillas/" + marco_Name);
        TargetFoto.mainTexture = MergeTexture.Merge ((Texture2D)TargetFoto.mainTexture,marquito, 0, 0);
       // DestroyImmediate (Foto.mainTexture, true);
       // Foto.mainTexture = BGFoto;
        galeria.SaveInsideUnity((Texture2D)TargetFoto.mainTexture);
        Resources.UnloadAsset(marquito);
    }
    public void SetAll2(){
        TargetFoto.mainTexture = Foto.mainTexture;

        galeria.SaveInsideUnity((Texture2D)TargetFoto.mainTexture);
    }


    public void SetFotoGaleria(){
        Texture2D texture = galeria.getTextureOfCenter();
        TargetFoto.mainTexture = texture;
        if (texture == null)
        {
         //Aqui deberia cambiar al intro  
        }
        BackButton.gameObject.SetActive (true);
    }
    public void OffBackButton(){

        BackButton.gameObject.SetActive (false);
    }
}
