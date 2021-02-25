using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractChecker : MonoBehaviour
{
    float rayDistance = 3;
    bool isInteracting;
    Transform camTransform = null;
    IInteractable interactable = null;
    OutlineScript outlineScript = null;
    
    void Start() {
        camTransform = Camera.main.transform;
    }
    
    void Update()
    {
        if (isInteracting) return;

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hitInfo, rayDistance, LayerMask.GetMask("Interactable")))
        {
            interactable = (IInteractable) hitInfo.transform.GetComponent<IInteractable>();

            if (outlineScript != null)
                outlineScript.OutlineEnabled = false;

            outlineScript = (OutlineScript) hitInfo.transform.GetComponent<OutlineScript>();

            // !!!!!!!!!!!!!!!!!!!!! This might not be needed! !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (outlineScript == null)
            {
                outlineScript = (OutlineScript) hitInfo.transform.GetComponentInChildren<OutlineScript>();
            }
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            if (outlineScript)
                outlineScript.OutlineEnabled = true;

        } else {

            interactable = null;
            if(outlineScript != null)
            {
                outlineScript.OutlineEnabled = false;
                outlineScript = null;
            }
        }
    }

    public void OnInteract()
    {
        if(interactable != null && !isInteracting)
        {
            interactable.Interact();
        }
    }

    public void SetIsInteracting(bool isItInteractingOrNotBruh)
    {
        isInteracting = isItInteractingOrNotBruh;
    }

}
