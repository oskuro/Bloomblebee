using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPerson : MonoBehaviour
{
    public string[] names;

    public void Generate()
    {
        int i = Random.Range(0, names.Length);
        print(names[i]);
    }
}
