using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial = null;
    private Material material;
    [SerializeField] private float outlineScaleFactor = 1.05f;
    [ColorUsage(true, true)]
    [SerializeField] private Color outlineColor = new Color(0.3137f, 0.8941f, 1.4901f, 0);
    [Tooltip("Optional parameter for when the mesh to outline isn't on the same object as the script.")]
    [SerializeField] private GameObject meshReference = null;
    private Renderer outlineRenderer;
    [SerializeField] Vector3 outlineOffset = new Vector3(0, 0, 0);

    public bool OutlineEnabled {
        get { return outlineRenderer.enabled; }
        set 
        { 
            outlineRenderer.enabled = value; 
            if(value == true)
            {
                material.SetColor("Color_Outline", outlineColor);
                material.SetFloat("Vector1_Scale", outlineScaleFactor);
            }
        }
    }

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.transform.position += outlineOffset;
        material = outlineRenderer.material;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color){

        if (meshReference == null) {
            meshReference = gameObject;
        }
            
        GameObject outlineObject = new GameObject("Outline");
        outlineObject.transform.parent = meshReference.transform;
        outlineObject.transform.position = meshReference.transform.position;
        outlineObject.transform.rotation = meshReference.transform.rotation;
        outlineObject.transform.localScale = new Vector3(1, 1, 1);

        MeshFilter filter = outlineObject.AddComponent<MeshFilter>();
        filter.mesh = meshReference.GetComponent<MeshFilter>().mesh;
        Renderer rend = outlineObject.AddComponent<MeshRenderer>();
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rend.enabled = false;

        return rend;
    }
}