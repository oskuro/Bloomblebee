using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitpoints : MonoBehaviour
{
    public float maxHP = 10f;
    public float currentHP = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

    }

    // Update is called once per frame
    void RecieveDamage(float _amount)
    {
        currentHP -= _amount;

        if(currentHP <= 0f)
        {
            Destroy(gameObject);
        }

        UICommander.sin.hpBarHealth.fillAmount = currentHP / maxHP;
    }
}
