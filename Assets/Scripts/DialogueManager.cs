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
        ("Mokou", "\"Mornin', Keine. Thanks for coming to help out with the charcoal making.\""),
        ("Keine", "\"Yeah, of course! Anything for a friend! Remember to press the space bar to complete tasks.\"")};

    static (string name, string sentence)[] diag2 = new (string, string)[] {
        ("Keine", "\"Hello, Mokou! Shall we do our best today as well?\""),
        ("Mokou", "\"Ah- Keine! *yawn… S'pose we should jump right in.\"")
    };

    static (string name, string sentence)[] diag3 = new (string, string)[] {
        ("Mokou", "\"Hey, Keine. Thanks for coming to help so often.\""),
        ("Mokou", "\"Customers keep piling up for the charcoal.\""),
        ("Keine", "\"No trouble! Even though it's messy, making the stuff is fun!\""),
    };

    static (string name, string sentence)[] diag4 = new (string, string)[] {
        ("Keine", "\"Hello, Mokou! The weather has been wonderful lately.\""),
        ("Mokou", "\"Heyo! Good to see you!\"")
    };

    static (string name, string sentence)[] diag5 = new (string, string)[] {
        ("Keine", "\"Good morning. It's quite chilly today, isn't it?\""),
        ("Mokou", "\"Hehe. What do you mean? Summer's barely over!\"")
    };

    static (string name, string sentence)[] diag6 = new (string, string)[] {
        ("Keine", "\"Ah, Mokou… I'm not sure how much I can do, but I'm happy to help.\""),
        ("Mokou", "\"That's alright! Every little bit helps!\"")
    };

    static (string name, string sentence)[] diag7 = new (string, string)[] {
        ("Keine", "\"Ah, Mokou… The gift for my granddaughter was wonderful. She's very grateful.\""),
        ("Mokou", "\"Always! Le'mme finish up the charcoal, and we'll eat some good food!\"")
    };

    static (string name, string sentence)[] diag8 = new (string, string)[] {
        ("Mokou", "\"Sup, Keine? Why don't you have a seat? I shouldn't take too long.\""),
        ("Keine", "\"Ah, thank you… The breeze is lovely today…\"")
    };

    static (string name, string sentence)[] diag9 = new (string, string)[] {
        ("Mokou", "\"I’ll do my best today too, Keine!\"")
    };

    static (string name, string sentence)[] diag10 = new (string, string)[] {
        ("Mokou", "\"Well… I s’pose we better get to it…\"")
    };

    static (string name, string sentence)[] diag11 = new (string, string)[] {
        ("Mokou", "\"Good morning…\"")
    };

    static (string name, string sentence)[] diag12 = new (string, string)[] {
        ("Mokou", "\"It’s chilly today, eh, Keine?\"")
    };

    static (string name, string sentence)[] diag13 = new (string, string)[] {
        ("Mokou", "\"...\"")
    };

    (string name, string sentence)[] currentDiag;

    (string name, string sentence)[][] diagLibrary = new (string, string)[][] {
        diag1, diag2, diag3, diag4, diag5, diag6, diag7, diag8, diag9, diag10,
        diag11, diag12, diag13};

    private int currentDiagId = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        diagPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueRunning && (Input.GetKeyDown(KeyCode.Space)))
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
        if (id < 0)
            return false;
        if (id >= diagLibrary.Length)
        {
            id = diagLibrary.Length - 1;
        }

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
