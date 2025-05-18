using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShader : MonoBehaviour{
    public ShaderClass ShaderBase;
    public Texture texture;
    public float strength;
    public float alpha;


    // Start is called before the first frame update
    void Start(){
        Renderer rend = GetComponent<Renderer>();

        Apply();
    }

    void Apply() { 
        if (ShaderBase.Dupe) {
            ApplyWithDupe();
        } else{
            ApplyNoDupe();
        }
    }

    void ApplyWithDupe(){

    }

    void ApplyNoDupe(){

    }
}

public class ShaderClass{
    public Shader[] shaders;
    public bool Dupe;
}
