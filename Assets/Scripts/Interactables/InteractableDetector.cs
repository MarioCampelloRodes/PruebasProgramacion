using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
            currentInteractable = null;
        }
    }

    //Está configurado con layers para que solo pueda detectar interactuables
    private void OnTriggerEnter(Collider other)
    {
        //Guardar la interfaz interactable que tenga ese objeto
        currentInteractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentInteractable = null;    
    }
}
