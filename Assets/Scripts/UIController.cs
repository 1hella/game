using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFadedToBlack() && gameController.EndDay)
        {
            Debug.Log("Ending day");
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
}
