using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialogue dialogue;

    //Este script, al interactuar mediante la interface, muestra el diálogo asignado
    public void Interact()
    {
        DialogueManager.singleton.BeginDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }
}
