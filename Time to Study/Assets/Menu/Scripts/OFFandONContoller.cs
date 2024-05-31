using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OFFandONContoller : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToEnable;
    [SerializeField] GameObject[] objectsToDisable;
    private ButtonController buttonController;

    private void Start()
    {
        buttonController = FindObjectOfType<ButtonController>();
    }

    public void OnButtonClick()
    {
        buttonController.ToggleObjects(objectsToEnable, objectsToDisable);
    }
}
