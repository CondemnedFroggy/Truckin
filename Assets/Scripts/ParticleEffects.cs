using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public ParticleSystem sparks;
    public CarMovement carController;
    public MeshCollider boxCollider;

    [SerializeField]
    private GameObject CrashAudioSource;

	// Use this for initialization
	void Start ()
    {

        carController = GetComponent<CarMovement>();
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Glass")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (carController.carVelocity > 0)
                {
                    Vector3 pos = contact.point;
                    Quaternion rot = Quaternion.FromToRotation(Vector3.back, contact.normal);
                    ParticleSystem temp = Instantiate(sparks, pos, rot);
                    temp.transform.parent = transform;
                    temp.loop = false;
                    //temp.Emit(1);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Glass")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (carController.carVelocity > 0.5)
                {
                    Vector3 pos = contact.point;
                    Quaternion rot = Quaternion.FromToRotation(Vector3.back, contact.normal);
                    ParticleSystem temp = Instantiate(sparks, pos, rot);
                    temp.transform.parent = transform;
                    temp.loop = false;
                    //temp.Emit(1);
                }
            }
            if (collision.gameObject.tag != "Glass")
            {
                CrashAudioSource.GetComponent<AudioSource>().Play();
                CrashAudioSource.GetComponent<AudioSource>().volume = carController.carVelocity / 100;
            }
        }
    }
}
