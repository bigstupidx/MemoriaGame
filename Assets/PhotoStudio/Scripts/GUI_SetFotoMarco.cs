using UnityEngine;
using System.Collections;

public class GUI_SetFotoMarco : MonoBehaviour {

    public UITexture Foto;
    public UITexture TargetFoto;

    public UICenterOnChild Marco;
    public UI2DSprite TargetMarco;

    public GUI_GaleriaManager galeria;
    public void SetAll(){
        TargetFoto.mainTexture = Foto.mainTexture;
        TargetMarco.sprite2D = Marco.centeredObject.GetComponent<UI2DSprite>().sprite2D;
        //DestroyImmediate (Foto.mainTexture, true);

        TargetFoto.mainTexture = MergeTexture.Merge ((Texture2D)TargetFoto.mainTexture,(Texture2D)TargetMarco.sprite2D.texture, 0, 0);
        galeria.SaveInsideUnity((Texture2D)TargetFoto.mainTexture);
    }
}
