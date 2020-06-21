using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> GameSections;

    public void LoadSection(int sectionIndex)
    {
        GameSections[sectionIndex].SetActive(true);
    }

    public void UnloadSection(int sectionIndex)
    {
        GameSections[sectionIndex].SetActive(false);
    }
}
