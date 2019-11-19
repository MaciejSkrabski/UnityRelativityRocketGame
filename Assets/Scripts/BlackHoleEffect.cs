using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//https://www.youtube.com/watch?v=Yk8Rmf0ehHU

[RequireComponent(typeof(Camera))]
public class BlackHoleEffect : MonoBehaviour
{
    public Shader shader;
    public Transform blackHole;
    private float ratio; //aspect ratio of the screen
    public float radius; //size of the black hole

    private Camera cam;
    private Material _material; //procedurally generated. It doesn't exist in edit mode.
    private Vector3 wtsp;
    private Vector2 pos;

    Material material {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        ratio = 1f / cam.aspect;
    }

    void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //processing
        if (shader && material && blackHole)
        {
            wtsp = cam.WorldToScreenPoint(blackHole.position); //WorldToScreenPoint
            //if ()
            Debug.Log(wtsp);
            //is the black hole in front of the cam?
            if(wtsp.z > 0)
            {
                pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);
                //Debug.Log(pos.ToString());
                //apply shader parameters
                _material.SetVector("_Position", pos);
                _material.SetFloat("_Ratio", ratio);
                _material.SetFloat("_Rad", radius);
                _material.SetFloat("_Distance", Vector3.Distance(blackHole.position, transform.position));

                //apply shader to image
                Graphics.Blit(source, destination, material);

            }
        }
    }

}
