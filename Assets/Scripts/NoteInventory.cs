using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteInventory : MonoBehaviour
{
    [SerializeField]
    public NoteObject[] collectedNotes = new NoteObject[20];

    [SerializeField]
    Button[] noteButtons = new Button[20];

    [SerializeField]
    public GameObject noteUI;

    [SerializeField]
    public GameObject noteGrid;

    [SerializeField]
    public TMP_Text pageText;

    RectTransform openNoteTransform;
    RectTransform closeNoteTransform;

    
    // Start is called before the first frame update
    void Start()
    {
        //pageText = noteUI.GetComponent<TMP_Text>();
        closeNoteTransform = noteUI.GetComponent<RectTransform>();

        int i = 0;
        foreach (NoteButton noteButton in noteGrid.GetComponentsInChildren<NoteButton>())
        {
            //Debug.Log(i.ToString());
            noteButtons[i] = noteButton.GetComponent<Button>();
            noteButton.myNoteNumber = i;
            //noteButtons[i].OnPointerClick;
            noteButtons[i].onClick.AddListener(delegate { OpenNote(noteButton.myNoteNumber); });
            i++;
        }
        //Debug.Log(i.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenInventory()
    {

    }

    public void OpenNote(int NoteNumber)
    {
        Debug.Log(NoteNumber.ToString());
        noteUI.SetActive(true);
        pageText.text = collectedNotes[NoteNumber].myNoteText;
    }

    public void AddNoteToInventory(NoteObject noteToAdd)
    {
        collectedNotes[noteToAdd.myNoteNumber] = noteToAdd;
        noteButtons[noteToAdd.myNoteNumber].interactable = true;
        OpenNote(noteToAdd.myNoteNumber);

        //ASDF Movie Muffin T-T - Note: must specify gameobject type when calling destroy
        //Destroy(noteToAdd.gameObject);
    }
}
