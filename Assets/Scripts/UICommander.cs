using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class UICommander : MonoBehaviour
{
    public static UICommander sin;

    public GameObject inputBlockUI;

    //public Image hpBarBg;
    public Image hpBarHealth;

    private void Awake()
    {
        if (sin == null)
            sin = this;
    }

}
