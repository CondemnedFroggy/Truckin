using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionOnMouseOver : MonoBehaviour
{
    [SerializeField]
    private GameObject[] HoverOverIndicators;

    [SerializeField]
    private GameObject hover_over_sound;

    [SerializeField]
    private GameObject text_to_highlight;


    public void MouseOver()
    {
        foreach (GameObject indicator in HoverOverIndicators)
        {
            indicator.SetActive(true);
        }

        hover_over_sound.GetComponent<AudioSource>().Play();

        text_to_highlight.GetComponent<Text>().color += new Color(0.1f, 0, 0);
    }

    public void MouseExit()
    {
        foreach (GameObject indicator in HoverOverIndicators)
        {
            indicator.SetActive(false);
        }
        text_to_highlight.GetComponent<Text>().color -= new Color(0.1f, 0, 0);
    }
}