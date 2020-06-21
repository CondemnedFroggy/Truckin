using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAI : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10.0f;
    [SerializeField]
    private float acceleration = 0.01f;
    [SerializeField]
    private float currentSpeed = 0.0f;
    [SerializeField]
    private float lifetime = 3600.0f;
    [SerializeField]
    private float maxDistance = 0.0f;
    [SerializeField]
    private float distance = 0.0f;
    [SerializeField]
    private List<GameObject> blockers;

    private List<GameObject> points;
    private GameObject previousPoint;
    private GameObject currentPoint;
    private int nextPointIndex;
    [SerializeField]
    private float distanceToPoints = 1.0f;
    private float maxNormalisedRotationDifference = 10.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;

    public float distanceToRear = 0.0f;
    public float distanceToFront = 0.0f;

    private void Start()
    {
        points = transform.parent.GetComponentInChildren<ListOfTravelPoints>().GetListOfTravelPoints();

        previousPoint = null;
        currentPoint = points[0];

        if (points.Count > 1)
        {
            nextPointIndex = 1;
        }
        else
        {
            nextPointIndex = -1;
        }
    }

    void Update()
    {
        UpdateTravelPoints();

        transform.position += (Time.deltaTime * transform.forward * currentSpeed);

        distance = Vector3.Distance(transform.position, transform.parent.position);

        if (lifetime < 0.0f || distance > maxDistance)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }

        if (blockers.Count > 0)
        {
            foreach (GameObject blocker in blockers)
            {
                if (currentSpeed > 0.0f)
                {
                    currentSpeed -= currentSpeed * 0.05f;
                }
            }
        }
        else
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += (maxSpeed - currentSpeed) * acceleration;
            }
        }
    }

    private void UpdateTravelPoints()
    {        
        // if pos is within limit to currentPoint
        if (Vector3.Distance(transform.position, new Vector3(currentPoint.transform.position.x, transform.position.y, currentPoint.transform.position.z)) < distanceToPoints)
        {
            Debug.Log("Next Point " + nextPointIndex);

            //update points
            previousPoint = currentPoint;

            if (nextPointIndex != -1)
            {
                currentPoint = points[nextPointIndex];

                if (currentPoint.GetComponent<TravelPointsOverrides>())
                {
                    TravelPointsOverrides tpo = currentPoint.GetComponent<TravelPointsOverrides>();

                    if (tpo.distance > 0.0f)
                    {
                        distanceToPoints = tpo.distance;
                    }
                    else
                    {
                        distanceToPoints = 5.0f;
                    }
                }

                if (points.Count > nextPointIndex + 1)
                {
                    nextPointIndex++;
                }
            }
        }
        else
        {
            // if rotation is outside of limit to currentPoint
            if (Vector3.Dot(transform.TransformDirection(Vector3.forward), (currentPoint.transform.position - transform.position)) < maxNormalisedRotationDifference)
            {
                // roh-tah-tay
                Vector3 oldRotation = transform.rotation.eulerAngles;
                transform.LookAt(currentPoint.transform);
                Vector3 newRotation = new Vector3 (oldRotation.x, transform.rotation.eulerAngles.y, oldRotation.z);
                transform.eulerAngles = oldRotation;

                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, newRotation, Time.deltaTime * rotationSpeed);

                Debug.Log("Bryn was right, kinda");
            }

            // ellie was not right
            //Vector3 targetY = new Vector3(currentPoint.transform.position.x, transform.position.y, currentPoint.transform.position.z);           
            //Vector3 lookAt = Vector3.Lerp(transform.position, targetY, Time.deltaTime * rotationSpeed);
            //Debug.Log(gameObject + " " + lookAt);
            //transform.LookAt(lookAt, Vector3.up);
        }
    }

    public void SetSpeed(float value)
    {
        maxSpeed = value;
    }
    public void SetAcceleration(float value)
    {
        acceleration = value;
    }

    public void SetRotationSpeed(float value)
    {
        rotationSpeed = value;
    }

    public void SetDistanceToPoint(float value)
    {
        distanceToPoints = value;
    }

    public void SetTimeUntilDeath(float value)
    {
        lifetime = value;
    }

    public void SetMaxDistance(float value)
    {
        maxDistance = value;
    }

    public void AddBlocker(GameObject vehicle)
    {
        blockers.Add(vehicle);
    }

    public void RemoveBlocker(GameObject vehicle)
    {
        blockers.Remove(vehicle);
    }
}