using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class StartGame : MonoBehaviour {

    [SerializeField] List<GameObject> gameplay_object = new List<GameObject>();
    [SerializeField] List<GameObject> cinematic_object = new List<GameObject>();

    [SerializeField] CameraController gameplay_camera;
    [SerializeField] CinemachineBrain cinematic_camera;

    // Use this for initialization
    void Start () {
		foreach(GameObject candidate in gameplay_object)
        {
            candidate.SetActive(true);
        }

        foreach (GameObject candidate in cinematic_object)
        {
            candidate.SetActive(false);
        }

        gameplay_camera.enabled = true;
        cinematic_camera.enabled = false;
    }

}
