using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    // Wheel collider 
    [HideInInspector]
    public WheelCollider frontL, frontR, rearL, rearR;
    [HideInInspector]
    public Transform t_frontL, t_frontR, t_rearL, t_rearR;
    [SerializeField]
    private GameObject Speedo;
    [SerializeField]
    private float speed, maxVelocity, maxRotation, brakeForce;
    private float horizontal, vertical, steeringAngle;
    public float carVelocity, carSidewaysVelocity;

    // Flip Back Over Mechanic
    public bool flipped = false;

    public Text flipText;

    // Audio 
    [HideInInspector]
    public AudioClip hornClip, idleClip, accelerationClip, decellarationClip;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject DriftAudioSource;

    //I know it should be in particle effects but eh
    [SerializeField]
    private GameObject[] trailWheels;

    // Cam
    public Camera cam;

    private void Awake()
    {
       audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        WheelPoses();
        Brakes();
        Audio();

        Speedo.GetComponent<Text>().text = (Mathf.RoundToInt(carVelocity)).ToString();

        // Press U to rotate car.
        if (Input.GetKey(KeyCode.U))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * 1000);
        }
    }

    private void LateUpdate()
    {
        ReverseCam();

        //Debug.Log(transform.rotation.eulerAngles.x + " " + transform.rotation.eulerAngles.z);

        //if ((transform.rotation.eulerAngles.x > 50.0f && transform.rotation.eulerAngles.x < 310.0f) ||
        //    (transform.rotation.eulerAngles.x < -50.0f && transform.rotation.eulerAngles.x > -310.0f) ||
        //    (transform.rotation.eulerAngles.z > 50.0f && transform.rotation.eulerAngles.z < 310.0f) ||
        //    (transform.rotation.eulerAngles.z < -50.0f && transform.rotation.eulerAngles.z > -310.0f))
        //{
        //    Debug.Log("Flipped");
        //    flipText.enabled = true;
        //    flipped = true;
        //}
        //else
        //{
        //    flipText.enabled = false;
        //    flipped = false;
        //}

        //if (flipped && Input.GetKey("x"))
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                       
        //    transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //}
    }

    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void Accelerate()
    {
        if (carVelocity <= 50.0f) // THIS DOES NOT CLAMP THE CAR'S SPEED
        {
            frontL.motorTorque = vertical * speed;
            frontR.motorTorque = vertical * speed;
        }

        // Car's world space velocity
        carVelocity = Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.forward);
        carVelocity = Mathf.Abs(carVelocity) * 3f;

        carSidewaysVelocity = Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.right);
        carSidewaysVelocity = Mathf.Abs(carSidewaysVelocity);

        float current_speed = Vector3.Magnitude(GetComponent<Rigidbody>().velocity);  // test current object speed

        if (current_speed > maxVelocity)

        {
            float brakeSpeed = current_speed - maxVelocity;  // calculate the speed decrease

            Vector3 normalisedVelocity = GetComponent<Rigidbody>().velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            GetComponent<Rigidbody>().AddForce(-brakeVelocity);  // apply opposing brake force
        }
    }

    private void Steer()
    {
        steeringAngle = maxRotation * horizontal;
        frontL.steerAngle = steeringAngle;
        frontR.steerAngle = steeringAngle;
    }

    private void Brakes()
    // Slows down the car using spacebar.
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rearR.brakeTorque = (brakeForce * 100000);
            rearL.brakeTorque = (brakeForce * 100000);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rearR.brakeTorque = 0;
            rearL.brakeTorque = 0;
        }

        if (vertical == 0)
        {
            rearR.brakeTorque = (brakeForce * 10000);
            rearL.brakeTorque = (brakeForce * 10000);
        }
        else
        {
            rearR.brakeTorque = 0;
            rearL.brakeTorque = 0;
        }
    }

    private void WheelPoses()
    // Assigns the wheels colliders and transforms to be rotated.
    {
        UpdateWheelPose(frontL, t_frontL);
        UpdateWheelPose(frontR, t_frontR);
        UpdateWheelPose(rearR, t_rearR);
        UpdateWheelPose(rearL, t_rearL);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    // Rotates the wheels when the car moves.
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    private void ReverseCam()
    {
        if (Input.GetKey(KeyCode.S))
        {
            cam.GetComponent<CameraController>().offset.z = 5.0f;
        }
        else
            cam.GetComponent<CameraController>().offset.z = -5.0f;
    }

    private void Audio()
    {  
        // Horn.
        if (Input.GetButton("Horn"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = hornClip;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip == hornClip && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        if (carVelocity < 1)
        {
            audioSource.pitch = 0.3f;
        }
        else
        {
            audioSource.pitch = carVelocity / 25;
        }

        if (carSidewaysVelocity > 2)
        {
            if(!DriftAudioSource.GetComponent<AudioSource>().isPlaying)
            {
                DriftAudioSource.GetComponent<AudioSource>().Play();
            }

            DriftAudioSource.GetComponent<AudioSource>().volume = ((carSidewaysVelocity / 20) - 0.2f);
        }
        else
        {
            DriftAudioSource.GetComponent<AudioSource>().Stop();
        }


        if (carSidewaysVelocity > 4)
        {
            foreach (GameObject wheel in trailWheels)
            {
                wheel.GetComponent<TrailRenderer>().emitting = true;
            }
            rearL.GetComponent<ParticleSystem>().Play();
            rearR.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            foreach (GameObject wheel in trailWheels)
            {
                wheel.GetComponent<TrailRenderer>().emitting = false;
            }
            rearL.GetComponent<ParticleSystem>().Stop();
            rearR.GetComponent<ParticleSystem>().Stop();
        }

        //// Acceleration.
        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (!audioSource.isPlaying || audioSource.clip == decellarationClip)
        //    {
        //        audioSource.clip = idleClip;
        //        audioSource.Play();
        //    }
        //}
        //else if(!Input.GetKey(KeyCode.W))
        //{
        //    if (audioSource.clip == idleClip && audioSource.isPlaying)
        //    {
        //        audioSource.pitch -= (Time.deltaTime * 0.1f);
        //    }
        //}
    }

}
