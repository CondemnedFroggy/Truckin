using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadManagerGO;

    private LoadManager loadManager;

    [SerializeField]
    private List<int> SectionsToLoad;

    [SerializeField]
    private List<int> SectionsToUnload;

    void Start()
    {
        loadManager = LoadManagerGO.GetComponent<LoadManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            foreach (var section in SectionsToLoad)
            {
                loadManager.LoadSection(section);
            }
            foreach (var section in SectionsToUnload)
            {
                loadManager.UnloadSection(section);
            }
        }
    }


}
