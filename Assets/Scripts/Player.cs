using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    void Update()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.ShowOptions();

                if (Input.GetKeyDown(KeyCode.F))
                    interactable.Interact(KeyCode.F);
                else if (Input.GetKeyDown(KeyCode.E))
                    interactable.Interact(KeyCode.E);
            }
            else
            {
                GameManager.manager.ClearInteraction();
            }
        }else
        {
            GameManager.manager.ClearInteraction();
        }
    }
}
