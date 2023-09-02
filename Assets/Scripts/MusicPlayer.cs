using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour
{
    public AudioClip intro1;
    public AudioClip loop1;
    private AudioSource audioSource;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            audioSource.clip = intro1;
            audioSource.Play();
            started = true;
        }

        if (started && (!audioSource.isPlaying))
        {
            audioSource.clip = loop1;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
