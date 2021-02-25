using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petal : MonoBehaviour
{
    public Color noColor;
    public Color color;
    private Material material;

    public delegate void PetalChanged();
    public PetalChanged petalChanged;

    bool pollinated = false;
    public bool Pollinated { get => pollinated; }
    float pollinatedTime = 0;
    float pollinatedDuration = 5;

    void Start()
    {
        //material = GetComponent<MeshRenderer>().material;
        //color = material.color;
    }

    private void Update()
    {
        pollinatedTime += Time.deltaTime;

        if(pollinated == true && pollinatedTime > pollinatedDuration)
        {
            pollinated = false;
            petalChanged();
        }
    }

    public void Pollinate()
    {
        pollinated = true;
        pollinatedTime = 0;
        petalChanged();
        Debug.Log("Petal pollinated");
    }

    //public Color GetColor()
    //{
    //    material.color = noColor;
    //    colorChanged();
    //    return color;
    //}

    //public void SetColor(Color _color)
    //{
    //    material.color = _color;
    //    colorChanged();
    //    color = _color;
    //}
}
