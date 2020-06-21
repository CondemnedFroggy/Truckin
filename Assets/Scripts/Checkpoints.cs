using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public int checkpoint_ID;

	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            switch (checkpoint_ID)
            {
                // Starting position checkpoint.
                case 1:
                    break;

                case 2:
                    break;

                // Game over position checkpoint.
                case 3:
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>().ChangeScene(3);
                    GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().GameOver();
                    break;
            }
        }
    }
}
