using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private int target_scene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    public void SetTargetScene(int scene_index)
    {
        target_scene = scene_index;
    }

    public void ChangeToTargetSceneAfterSeconds(float seconds)
    {
        StartCoroutine(ChangeScene(target_scene, seconds));
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    IEnumerator ChangeScene(int scene_index ,float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene_index);
    }
}
