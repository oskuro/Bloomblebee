using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    Petal[] petals;
    public GameObject nectarPrefab;


    void Start()
    {
        petals = GetComponentsInChildren<Petal>();
        foreach (Petal p in petals)
        {
            p.petalChanged += PetalChanged;
        }
    }

    public void PetalChanged()
    {
        bool allPetalsPollinated = true;
        foreach(Petal p in petals)
        {
            if (!p.Pollinated) { 
                allPetalsPollinated = false;
                break;
            }
        }
        if(allPetalsPollinated)
        {
            DeactivatePetals();
            SpawnNectar();
        }
    }

    public void ColorChanged()
    {
        // We wont have black petals so black is considered unset or null
        Color petalColor = Color.black;
        bool allSameColor = true;
        foreach(Petal p in petals)
        {
            if(petalColor.Equals(Color.black))
            {
                petalColor = p.color;
            }
            else
            {
                if (!petalColor.Equals(p.color)) { 
                    allSameColor = false;
                    break;
                }
            }
        }

        if(allSameColor)
        {
            DeactivatePetals();
            SpawnNectar();
        }
    }

    private void SpawnNectar()
    {
        Instantiate(nectarPrefab, transform.position, Quaternion.identity, null);
    }

    private void DeactivatePetals()
    {
        foreach(Petal p in petals)
        {
            p.enabled = false;
        }
    }

    private void OnDestroy()
    {
        foreach (Petal p in petals)
        {
            p.petalChanged -= PetalChanged;
        }
    }
}
