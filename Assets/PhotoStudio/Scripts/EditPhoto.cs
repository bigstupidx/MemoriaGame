using UnityEngine;
using System.Collections;

public class EditPhoto : MonoBehaviour {

    public UIWidget button;

    public UITexture FotoManager;
    public UITexture FotoEdit;

    public UICenterOnChild MarcoManager;
    public UITexture MarcoEdit;


    public GUI_SetFotoMarco ShareFoto;
    public ManagerPhotoStudioMenu Menu;

    Texture2D marquitoTexture;

    #region ResetVar:
    Vector3 positionIniFoto;
    Vector3 scaleIniFoto;
    bool isEditing =false;

    #endregion
    #region Edit Var:
    Vector3 FirstTouchPos = Vector3.zero;
    bool isFirstTouch = true;
    #endregion

    void Start(){
        positionIniFoto = FotoEdit.transform.localPosition;
        scaleIniFoto = FotoEdit.transform.localScale;

    }

    /// <summary>
    /// Coloca la foto de la camra y la plantilal junta para su edicion.
    /// </summary>
    public void SetForEdit(){

        string marco_Name = MarcoManager.centeredObject.GetComponent<UISprite>().spriteName;//Este me da el nombre de la plantilla
        LoadSetMarcoPhoto (marco_Name);


    }
    /// <summary>
    /// CArgo el marco y coloco la foto en la escena q es
    /// </summary>
    /// <param name="marco_Name">Marco name.</param>
    protected void LoadSetMarcoPhoto(string marco_Name){
        marquitoTexture = (Texture2D)Resources.Load("Plantillas/" + marco_Name);//Carga la plantilla de tamano real
        MarcoEdit.mainTexture = marquitoTexture;

        FotoEdit.mainTexture = FotoManager.mainTexture;
        FotoEdit.transform.localPosition = positionIniFoto;
        FotoEdit.transform.localScale = scaleIniFoto;

        isEditing = true;
        button.alpha = 1;
    }
    private IEnumerator SaveScreenshot() {
        yield return new WaitForEndOfFrame();
        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
        // Read screen contents into the texture
        tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
        tex.Apply();

        FotoEdit.mainTexture = tex;
        SetAllToExit();

    }
    void SetAllToExit(){
        #if !UNITY_EDITOR
        DestroyImmediate(FotoManager.mainTexture,true);
        #endif
        FotoManager.mainTexture = FotoEdit.mainTexture;
        FotoEdit.mainTexture = null;


        MarcoEdit.mainTexture = null;
        Resources.UnloadAsset(marquitoTexture);

        isEditing = false;

        ShareFoto.SetAll2();
        Menu.GoToPreview();
    }
    public void TakeScreenshoot(){
        StartCoroutine(SaveScreenshot());

    }
    public void ExitEdit(){
        //Aqui tomo el screenshoot
        TweenAlpha.Begin(button.gameObject, 0.1f,0);
       // Invoke("TakeScreenshoot", 0.5f);
    }



    void Update(){
        if (isEditing)
        {
            #if UNITY_EDITOR
            if ( (Input.GetMouseButton(0) && !Input.GetMouseButton(1)))
            {
                if(isFirstTouch){
                    FirstTouchPos = Input.mousePosition;
                    isFirstTouch = false;
                }

                Vector3 deltaPos = Input.mousePosition - FirstTouchPos ;
                if (FotoEdit.transform.localScale.x >= 1 && FotoEdit.transform.localScale.y >= 1)
                {
                    FotoEdit.transform.localPosition += new Vector3(deltaPos.x,deltaPos.y,FotoEdit.transform.localPosition.z);
                }
                FirstTouchPos = Input.mousePosition;
            }else{
                isFirstTouch = true;
            }
            FotoEdit.transform.localScale = new Vector3(FotoEdit.transform.localScale.x +Input.GetAxis("Mouse ScrollWheel"),
                FotoEdit.transform.localScale.y+ Input.GetAxis("Mouse ScrollWheel"),
                FotoEdit.transform.localScale.z);

            if (FotoEdit.transform.localScale.x < 1 )
            {
                FotoEdit.transform.localScale = new Vector3(1, 1, FotoEdit.transform.localScale.z);
            }

            #else
            if (Input.touchCount == 1 )
            {

            if (FotoEdit.transform.localScale.x >= 1 && FotoEdit.transform.localScale.y >= 1 && FotoEdit.transform.localScale.z >= 1)
            {
            FotoEdit.transform.localPosition += new Vector3(Input.touches[0].deltaPosition.x,Input.touches[0].deltaPosition.y,FotoEdit.transform.localPosition.z);
            }
            }else if (Input.touchCount == 2)
            {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            FotoEdit.transform.localScale = new Vector3(FotoEdit.transform.localScale.x -deltaMagnitudeDiff*0.1f,
            FotoEdit.transform.localScale.y - deltaMagnitudeDiff*0.1f,
            FotoEdit.transform.localScale.z);

            if (FotoEdit.transform.localScale.x < 1 )
            {
            FotoEdit.transform.localScale = new Vector3(1, 1, FotoEdit.transform.localScale.z);
            }  
            }
            #endif
     
           
        }
    }
}
