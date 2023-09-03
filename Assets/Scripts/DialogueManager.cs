using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private bool dialogueRunning = false;
    private int sentenceId = 0;
    public TextMeshProUGUI boxName;
    public TextMeshProUGUI boxSentence;
    public Image portrait;
    public GameObject diagPanel;
    public Sprite[] portraits;

    (int portraitId, string name, string sentence) [] diag1 = new (int, string, string)[] {
        (0, "Keine", "Test dialogue A1!"),
        (1, "Mokou", "Test dialogue A2"),
        (0, "Keine", "Test dialogue A3")};

    (int portraitId, string name, string sentence)[] diag2 = new (int, string, string)[] {
        (0, "Keine", "Test dialogue B1!"),
        (1, "Mokou", "Test dialogue B2"),
        (1, "Mokou", "Test dialogue B3"),
        (0, "Keine", "Test dialogue B4!")};

    (int portraitId, string name, string sentence)[] currentDiag;
    private int currentDiagId = 0;

    (int x, int y)[] coords = new (int, int)[] { (1, 3), (5, 1), (8, 9) };

    // Start is called before the first frame update
    void Start()
    {
        diagPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueRunning && Input.GetKeyDown(KeyCode.Alpha6))
        {
            sentenceId++;
            if (sentenceId >= currentDiag.Length)
            {
                dialogueRunning = false;
                diagPanel.SetActive(false);
                sentenceId = 0;
                return;
            }
            boxName.text = currentDiag[sentenceId].name;
            boxSentence.text = currentDiag[sentenceId].sentence;
            portrait.sprite = portraits[currentDiag[sentenceId].portraitId];
        }

        if (!dialogueRunning && Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (currentDiagId < 2)
            {
                currentDiagId++;
                if (currentDiagId == 1)
                    currentDiag = diag1;
                else if (currentDiagId == 2)
                    currentDiag = diag2;

                dialogueRunning = true;
                diagPanel.SetActive(true);

                boxName.text = currentDiag[sentenceId].name;
                boxSentence.text = currentDiag[sentenceId].sentence;
                portrait.sprite = portraits[currentDiag[sentenceId].portraitId];
            }
        }
    }

    public void PlayNext()
    {
        // play next set of dialog for the day
    }
}
