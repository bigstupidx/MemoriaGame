using UnityEngine;
using System.Collections;

public class LinkWidgetToMesh : MonoBehaviour {

    public UIWidget widget;
    protected Material mat;
    protected Renderer mesh;
    // Use this for initialization
    void Awake () {
        mesh = GetComponent<MeshRenderer>();
        if (mesh == null)
            mesh = GetComponent<SkinnedMeshRenderer> ();
        mat = mesh.material;

    }

    // Update is called once per frame
    void LateUpdate () {
        mat.SetFloat("_Alpha",widget.finalAlpha);
        if(widget.finalAlpha <= 0){
            if(mesh.enabled)
                mesh.enabled = false;
        }else if( !mesh.enabled){
            mesh.enabled = true;
        }
    }
}
