using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public KeyCode keyToPress;

    private bool canBePressed = false;

    public bool CanBePressed { get => canBePressed; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBePressed)
        {
            if (Input.GetKeyDown(keyToPress))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = true;
            RythmActionButtonController rabc = collision.GetComponent<RythmActionButtonController>();
            rabc.arrowInRange = true;
            Vector3 error = transform.position - rabc.transform.position;
            rabc.hitError = error.y;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            canBePressed = false;
            RythmActionButtonController rabc = collision.GetComponent<RythmActionButtonController>();
            rabc.arrowInRange = false;
        } else if (collision.tag == "NoteDestroyer")
        {
            Debug.Log("hi");
        }
    }
}
