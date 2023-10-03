using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallActiveTrig : MonoBehaviour
{
    public GameObject fadeinBGM;
    public GameObject fadeoutBGM;
    private bool musicForFadeIn = false;
    private bool musicForFadeOut = false;

    private void Update()
    {
        if (musicForFadeIn)
        {
            BGM_Manager fadeInManager = fadeinBGM.GetComponent<BGM_Manager>();
            fadeInManager.PlayMusic();
            Debug.Log("PlayMusic");
            musicForFadeIn = false;
        }

        if(musicForFadeOut)
        {
            BGM_Manager fadeOutManager = fadeinBGM.GetComponent<BGM_Manager>();
            fadeOutManager.FadeOut(2f);
            fadeoutBGM.SetActive(false);
            Debug.Log("fadeoutBGM");
            musicForFadeOut = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeinBGM != null)
            {
                musicForFadeIn = true;
            }

            if (fadeoutBGM != null)
            {
                musicForFadeOut = true;
            }
        }
    }
}
