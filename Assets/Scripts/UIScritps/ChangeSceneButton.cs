using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(int scene_index)
    {
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>().ChangeScene(scene_index);
    }

}
