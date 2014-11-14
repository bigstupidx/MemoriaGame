using UnityEngine;
using System.Collections;

public class MergeTexture : MonoBehaviour {



    public static Texture2D Merge (Texture2D texToDraw ,Texture2D background){
        int x=0;
        int y=0;
        Sprite test;

        Color32[] pix1 = background.GetPixels32();
        Color32[] pix2 = texToDraw.GetPixels32();

        for (int j = 0; j < texToDraw.height; j++){
            for (int i = 0; i < texToDraw.width; i++) {
                pix1[background.width/2 - texToDraw.width/2 + x + i + background.width*(background.height/2-texToDraw.height/2+j+y)] = pix2[i + j*texToDraw.width];
            }
        }
        Texture2D newphoto = new Texture2D(background.width, background.height);
        newphoto.SetPixels32(pix1);
        newphoto.Apply();
        return newphoto;
    }
}
