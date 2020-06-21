using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);

	}

    private void Update()
    {
        if (GetComponent<AudioSource>().volume < 0.3f)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime * 0.01f;
        }
    }
}
