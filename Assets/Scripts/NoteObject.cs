using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour, I_Interactable
{
    [SerializeField]
    public int myNoteNumber;
    [SerializeField]
    public string myNoteText;

    [SerializeField]
    NoteInventory invRef;

    // Start is called before the first frame update
    void Start()
    {
        invRef = FindObjectOfType<NoteInventory>();
    }

    void Collect()
    {
        invRef.AddNoteToInventory(this);
    }

    void I_Interactable._interact()
    {
        Collect();
        Object.Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }
}
