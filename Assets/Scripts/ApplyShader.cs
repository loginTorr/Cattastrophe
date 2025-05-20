using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ApplyShader : MonoBehaviour{
    // for most shaders this list will only have one object,
    // but some (such as chromatic abberation) need multiple
    // parts.
    // if dupe = false, only put one item in this list.
    public List<Shader> shaders;

    // these two are for multi-part shaders, so the same
    // numbers can be applied to each part

    // the strength of the shader
    public float strength;
    // does the shader make the object semitransparent?
    public float alpha;


    // whether or not the shader needs to duplicate the object or
    // be applied to the object directly
    public bool dupe;

    // This list will hold onto game objects that have been created by
    // this applier. When applying a dupe shader with multiple parts (such
    // as chromatic abberation), it will skip objects in this list when
    // duping. 
    private List<GameObject> created = new List<GameObject>();


    // Start is called before the first frame update
    void Start(){
        Apply();
    }

    void Apply() { 
        if (dupe) {
            ApplyWithDupe();
        } else{
            ApplyNoDupe();
        }
    }

    void ApplyWithDupe() {
        if (transform.childCount > 0) {
            if (transform.parent == null) {
                for (int i = 0; i < transform.childCount; i++){
                    GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                    
                    if ((created.Contains<GameObject>(child)) == false){
                        for (int j = 0; j < shaders.Count; j++){
                            GameObject copy = Instantiate(child, child.transform.position, child.transform.rotation, transform);
                            created.Add(copy);

                            Renderer childRend = child.GetComponent<Renderer>();
                            Material childMat = childRend.material;
                            Texture texture = childMat.mainTexture;
                        
                            Renderer copyRend = copy.GetComponent<Renderer>();
                            copyRend.material = new Material(shaders[j]);
                            copyRend.material.SetFloat("_strength", strength);
                            copyRend.material.SetFloat("_Alpha", alpha);
                            copyRend.material.SetTexture("_texture", texture);
                        }
                    }
                }
            }else if (transform.parent.gameObject.GetComponent<ApplyShader>() == null) { 
                for (int i = 0; i < transform.childCount; i++){
                    GameObject child = this.gameObject.transform.GetChild(i).gameObject;
                    
                    if ((created.Contains<GameObject>(child)) == false){
                        for (int j = 0; j < shaders.Count; j++){
                            GameObject copy = Instantiate(child, child.transform.position, child.transform.rotation, transform);
                            created.Add(copy);

                            Renderer childRend = child.GetComponent<Renderer>();
                            Material childMat = childRend.material;
                            Texture texture = childMat.mainTexture;
                        
                            Renderer copyRend = copy.GetComponent<Renderer>();
                            copyRend.material = new Material(shaders[j]);
                            copyRend.material.SetFloat("_strength", strength);
                            copyRend.material.SetFloat("_Alpha", alpha);
                            copyRend.material.SetTexture("_texture", texture);
                        }
                    }
                }
            }
        } else{
            if (transform.parent == null){
                GameObject dummy = Instantiate(transform.gameObject, transform.position, transform.rotation, transform);
                for (int j = 0; j < shaders.Count; j++){
                    GameObject copy = Instantiate(dummy, transform.position, transform.rotation, transform);
                    created.Add(copy);

                    Renderer rend = GetComponent<Renderer>();
                    Material mat = rend.material;
                    Texture texture = mat.mainTexture;

                    Renderer copyRend = copy.GetComponent<Renderer>();
                    copyRend.material = new Material(shaders[j]);
                    copyRend.material.SetFloat("_strength", strength);
                    copyRend.material.SetFloat("_Alpha", alpha);
                    copyRend.material.SetTexture("_texture", texture);
                }
                Destroy(dummy);
            }else if (transform.parent.gameObject.GetComponent<ApplyShader>() == null){
                GameObject dummy = Instantiate(transform.gameObject, transform.position, transform.rotation, transform);
                for (int j = 0; j < shaders.Count; j++){
                    GameObject copy = Instantiate(transform.gameObject, transform.position, transform.rotation, transform);
                    created.Add(copy);

                    Renderer rend = GetComponent<Renderer>();
                    Material mat = rend.material;
                    Texture texture = mat.mainTexture;

                    Renderer copyRend = copy.GetComponent<Renderer>();
                    copyRend.material = new Material(shaders[j]);
                    copyRend.material.SetFloat("_strength", strength);
                    copyRend.material.SetFloat("_Alpha", alpha);
                    copyRend.material.SetTexture("_texture", texture);
                }
                Destroy(dummy);
            }
        }
    }

    void ApplyNoDupe(){
        Renderer rend = GetComponent<Renderer>();
        Texture texture = rend.material.mainTexture;

        rend.material = new Material(shaders[0]);
        rend.material.SetFloat("_strength", strength);
        rend.material.SetFloat("_Alpha", alpha);
        rend.material.SetTexture("_texture", texture);
        rend.material.SetTexture("_MainTex", texture);
    }
}
