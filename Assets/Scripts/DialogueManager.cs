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
    public Image portrait;
    public GameObject diagPanel;
    public Sprite[] portraits;

    static (int portraitId, string name, string sentence) [] diag1 = new (int, string, string)[] {
        (0, "Keine", "Test dialogue A1!"),
        (1, "Mokou", "Test dialogue A2"),
        (0, "Keine", "Test dialogue A3")};

    static (int portraitId, string name, string sentence)[] diag2 = new (int, string, string)[] {
        (0, "Keine", "Test dialogue B1!"),
        (1, "Mokou", "Test dialogue B2"),
        (1, "Mokou", "Test dialogue B3"),
        (0, "Keine", "Test dialogue B4!")};

    (int portraitId, string name, string sentence)[] currentDiag;

    (int portraitId, string name, string sentence)[][] diagLibrary = new (int, string, string)[][] {
        diag1, diag2 };

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
            portrait.sprite = portraits[currentDiag[sentenceId].portraitId];
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
        portrait.sprite = portraits[currentDiag[sentenceId].portraitId];
        return true;
    }
    private void FinishDiag()
    {
        eventController.DoneDiag();
    }
}
