using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private bool dialogueRunning = false;
    private int sentenceId = 0;
    public GameObject gameController;
    public EventController eventController;
    public TextMeshProUGUI boxName;
    public TextMeshProUGUI boxSentence;
    public GameObject diagPanel;

    static (string name, string sentence) [] diag1 = new (string, string)[] {
        ("Keine", "Test dialogue A1!"),
        ("Mokou", "Test dialogue A2"),
        ("Keine", "Test dialogue A3")};

    static (string name, string sentence)[] diag2 = new (string, string)[] {
        ("Keine", "Test dialogue B1!"),
        ("Mokou", "Test dialogue B2"),
        ("Mokou", "Test dialogue B3"),
        ("Keine", "Test dialogue B4!")};

    static (string name, string sentence)[] diag3 = new (string, string)[] {
        ("Keine", "Test dialogue C1!"),
        ("Mokou", "Test dialogue C2")};

    static (string name, string sentence)[] diag4 = new (string, string)[] {
        ("Mokou", "...")
    };

    (string name, string sentence)[] currentDiag;

    (string name, string sentence)[][] diagLibrary = new (string, string)[][] {
        diag1, diag2, diag3, diag4 };

    private int currentDiagId = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        diagPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueRunning && (Input.GetKeyDown(KeyCode.Alpha6)))
        {
            sentenceId++;
            if (sentenceId >= currentDiag.Length)
            {
                dialogueRunning = false;
                diagPanel.SetActive(false);
                sentenceId = 0;
                FinishDiag();
                return;
            }
            boxName.text = currentDiag[sentenceId].name;
            boxSentence.text = currentDiag[sentenceId].sentence;
        }
        /*
        if (!dialogueRunning && Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (currentDiagId < diagLibrary.Length)
            {
                currentDiagId++;
                if (currentDiagId == 1)
                    currentDiag = diagLibrary[0];
                else if (currentDiagId == 2)
                    currentDiag = diagLibrary[1];

                dialogueRunning = true;
                diagPanel.SetActive(true);

                boxName.text = currentDiag[sentenceId].name;
                boxSentence.text = currentDiag[sentenceId].sentence;
                portrait.sprite = portraits[currentDiag[sentenceId].portraitId];
            }
        }
        */
    }

    public void PlayNext()
    {
        // play next set of dialog for the day
    }

    public bool StartDialogueSet(int id)
    {
        if (id < 0 || id >= diagLibrary.Length)
            return false;

        currentDiagId = id;
        currentDiag = diagLibrary[currentDiagId];
        sentenceId = 0;

        dialogueRunning = true;
        diagPanel.SetActive(true);

        boxName.text = currentDiag[sentenceId].name;
        boxSentence.text = currentDiag[sentenceId].sentence;
        return true;
    }
    private void FinishDiag()
    {
        eventController.DoneDiag();
    }
}
