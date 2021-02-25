using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    ShipController player = null;
    public Transform targetPosition = null;
    public AnimationCurve landingCurve = null;
    public float timeScale = 1f;
    public delegate void LevelFinished();
    public LevelFinished levelFinished;
    

    void Start()
    {
        player = FindObjectOfType<ShipController>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.CanMove = false;
            StartCoroutine(LandPlayer(other.gameObject));
        }
    }

    IEnumerator LandPlayer(GameObject movable)
    {
        float timeElapsed = 0;
        Vector3 startPosition = movable.transform.position;
        
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime;
            movable.transform.position = Vector3.Lerp(startPosition, targetPosition.position, landingCurve.Evaluate(timeElapsed));
            yield return new WaitForEndOfFrame();
        }
        levelFinished();
    }
}
