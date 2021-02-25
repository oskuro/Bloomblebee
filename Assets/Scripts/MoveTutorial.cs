using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTutorial : MonoBehaviour
{
    public GameObject forwardText = null;
    public GameObject backwardText = null;
    public GameObject rightText = null;
    public GameObject leftText = null;
    public GameObject lookTextUpDown = null;
    public GameObject lookTextRightLeft = null;
    public GameObject goodJobText = null;
    public GameObject nectarText = null;

    
    public Transform onScreenTarget = null;
    public Transform offScreenTarget = null;
    public float timeScale = 1f;
    public AnimationCurve animationCurve = null;

    Vector2 movement = new Vector2(0,0);
    Vector2 look = new Vector2(0,0);

    public float timeBeforeNextUi = 1f;

    float timer = 0;
    bool nearNectar = false;

    LevelManager levelManager;
    void Start()
    {
        forwardText.SetActive(true);
        backwardText.SetActive(false);
        leftText.SetActive(false);
        rightText.SetActive(false);
        lookTextUpDown.SetActive(false);
        lookTextRightLeft.SetActive(false);
        goodJobText.SetActive(false);
        nectarText.SetActive(false);

        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
       
         timer += Time.deltaTime;

        if(forwardText.activeSelf && movement.y > 0)
        {
            timer = 0;
            forwardText.SetActive(false);
            backwardText.SetActive(true);
        }

        if(backwardText.activeSelf && movement.y < 0 && timer > timeBeforeNextUi)
        {
            timer = 0;
            backwardText.SetActive(false);
            leftText.SetActive(true);
        }

        if(leftText.activeSelf && movement.x < 0 && timer > timeBeforeNextUi)
        {
            timer = 0;
            leftText.SetActive(false);
            rightText.SetActive(true);
        }

        if(rightText.activeSelf && movement.x > 0 && timer > timeBeforeNextUi)
        {
            timer = 0;
            rightText.SetActive(false);
            lookTextUpDown.SetActive(true);
        }

        if(lookTextUpDown.activeSelf && look.y < 0 && timer > timeBeforeNextUi)
        {
            timer = 0;
            lookTextUpDown.SetActive(false);
            lookTextRightLeft.SetActive(true);
        }

        if(lookTextRightLeft.activeSelf && look.x < 0 && timer > timeBeforeNextUi)
        {
            timer = 0;
            lookTextRightLeft.SetActive(false);
            goodJobText.SetActive(true);
        }

        if(goodJobText.activeSelf && movement.y != 0 && timer > timeBeforeNextUi)
        {
            timer = 0;
            goodJobText.SetActive(false);
        }

        if (!nectarText.activeSelf && nearNectar)
        {
            nectarText.SetActive(true);
        } else if(nectarText.activeSelf && (!nearNectar || levelManager.NectarCount > 0))
        {
            nearNectar = false;
            nectarText.SetActive(false);
        }

        
        
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        nearNectar = true;    
    }

    private void OnTriggerExit(Collider other)
    {
        nearNectar = false;
    }

    IEnumerator MoveTransform(Transform transform, Vector3 start, Vector3 target, bool turnOn)
    {
        transform.position = start;
        if(turnOn)
            transform.gameObject.SetActive(true);

        float timeElapsed = 0;

        while(!transform.position.Equals(target))
        {
            timeElapsed += Time.deltaTime * timeScale;
            float t = animationCurve.Evaluate(timeElapsed);
            transform.position = Vector3.Lerp(start, target, t);
            yield return new WaitForEndOfFrame();
        }

        if(!turnOn)
            transform.gameObject.SetActive(false);

    }
}
