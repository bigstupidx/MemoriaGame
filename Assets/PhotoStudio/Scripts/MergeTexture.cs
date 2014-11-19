using UnityEngine;
using System.Collections;

public class MergeTexture : MonoBehaviour {



    public static Texture2D Merge (Texture2D texToDraw ,Texture2D marco, int x, int y){

        TextureScale.Bilinear (texToDraw, marco.width, marco.height);


        Color32[] pix1 = marco.GetPixels32();
        Color32[] pix2 = texToDraw.GetPixels32();
        for(int i = 0; i < pix1.Length; ++i)
        {
            if(pix1[i].a < 255*0.5f  )
                pix1[i] = pix2[i];
        }
        /*
        for (int j = 0; j < marco.height; j++){
            for (int i = 0; i < marco.width; i++) {
                if(pix1[j*marco.width + i].a != 255  )
                    pix1[ j*marco.width + i] = pix2[j*texToDraw.width + i];
            }
        }*/
        Texture2D newphoto = new Texture2D(marco.width, marco.height);
        newphoto.SetPixels32(pix1);
        newphoto.Apply();
        return newphoto;
    }
}
