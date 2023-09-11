using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameController gameController;
    public GameObject laundrySheet1;
    public GameObject laundrySheet2;
    public GameObject laundrySheet3;
    public GameObject bambooPile;
    public Animator bambooAnimator;

    // Start is called before the first frame update
    void Start()
    {
        ResetLaundry();
        ResetBamboo();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFadedToBlack() && gameController.EndDay)
        {
            StartCoroutine(FadeBlackOutSquare(true));
            gameController.EndDay = false;
        } else if (isFadedToBlack() && Input.GetKeyDown(KeyCode.Space)) {
            gameController.RestartLevel();
            StartCoroutine(FadeBlackOutSquare(false));
        }
    }

    private IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 2)
    {
        
        Color objectColor = blackOutSquare.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            Debug.Log("fading to black");
            while (blackOutSquare.GetComponent<SpriteRenderer>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<SpriteRenderer>().color = objectColor;
                yield return null;
            }
        } else
        {
            while (blackOutSquare.GetComponent<SpriteRenderer>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<SpriteRenderer>().color = objectColor;
                yield return null;
            }
        }
    }

    public void FadeToBlack(bool fadeToBlack = true)
    {
        StartCoroutine(FadeBlackOutSquare(fadeToBlack));
    }

    public bool isFadedToBlack()
    {
        return blackOutSquare.GetComponent<SpriteRenderer>().color.a > 0;
    }

    public void DoLaundryStep(int count)
    {
        if (count == 1)
        {
            DoLaundryStep1();
        } else if (count == 2)
        {
            DoLaundryStep2();
        } else if (count == 3)
        {
            DoLaundryStep3();
        }
    }

    public void ResetLaundry()
    {
        laundrySheet1.SetActive(false);
        laundrySheet2.SetActive(false);
        laundrySheet3.SetActive(false);
    }

    public void DoLaundryStep1()
    {
        laundrySheet1.SetActive(true);
        laundrySheet2.SetActive(false);
        laundrySheet3.SetActive(false);
    }

    public void DoLaundryStep2()
    {
        laundrySheet1.SetActive(true);
        laundrySheet2.SetActive(true);
        laundrySheet3.SetActive(false);
    }

    public void DoLaundryStep3()
    {
        laundrySheet1.SetActive(true);
        laundrySheet2.SetActive(true);
        laundrySheet3.SetActive(true);
    }

    public void ResetBamboo()
    {
        HideBambooPile();
    }

    public void ShowBambooPile()
    {
        bambooPile.SetActive(true);
    }

    public void HideBambooPile()
    {
        bambooPile.SetActive(false);
    }
}
