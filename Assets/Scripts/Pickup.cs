using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject promptCanvas = null;

    public Sprite gamepadSprite = null;
    public Sprite mouseSprite = null;
    Image promptImage = null;
    public bool secret = false;

    bool ending = false;
    LevelManager levelManager = null;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TogglePrompt();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TogglePrompt();
        }
    }

    private void TogglePrompt()
    {
        promptImage = promptCanvas.GetComponentInChildren<Image>();

        if (LevelManager.usesGamepad)
            promptImage.sprite = gamepadSprite;
        else
            promptImage.sprite = mouseSprite;

        promptCanvas.SetActive(!promptCanvas.activeSelf);
    }

    public void OnDrink()
    {
        if(promptCanvas.activeSelf)
        {
            if(!ending)
            {
                ending = true;
                GetComponent<AudioSource>().Play();
                StartCoroutine(DestroyNectar());
            }
        }
    }

    IEnumerator DestroyNectar()
    {
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float timeElapsed = 0;
        while(!transform.localScale.Equals(targetScale))
        {
            timeElapsed += Time.deltaTime * 2f;
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed);
            yield return new WaitForEndOfFrame();
        }

        if (secret)
            levelManager.SecretNectarCount++;
        else
            levelManager.NectarCount++;

        Destroy(gameObject);
    }
}
