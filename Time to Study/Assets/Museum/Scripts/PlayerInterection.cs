using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject InteractionUI;
    public TextMeshProUGUI InteractionText;

    private InterectableInterface currentInteractable;

    void OnTriggerEnter(Collider other)
    {
        InterectableInterface interactable = other.GetComponent<InterectableInterface>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            InteractionText.text = interactable.GetDiscription();
            InteractionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        InterectableInterface interactable = other.GetComponent<InterectableInterface>();
        if (interactable != null && interactable == currentInteractable)
        {
            InteractionUI.SetActive(false);
            currentInteractable = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
            InteractionUI.SetActive(false); // Скрываем UI после взаимодействия
            currentInteractable = null; // Сбрасываем значение после взаимодействия
        }
    }
}
