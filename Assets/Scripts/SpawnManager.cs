using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float timeBetweenSpawns = 0.0f;
    public float carMaxSpeed = 0.0f;
    public float carAcceleration = 0.01f;
    public float carRotationSpeed = 0.1f;
    public float carDistanceToPoint = 1.0f;
    public bool carHasLifetime = false;
    public float carLifetime = 0.0f;
    public bool carHasMaxDistance = false;
    public float maxDistance = 0.0f;
    public float separation = 0.0f;

    private GameObject player;
    private GameObject vehicle;
    private VehicleAI ai;
    private GameObject tempVehicle;
    [SerializeField]
    private float timeSinceLastSpawn = 10000.0f;
    private List<GameObject> VehicleTypes;
    [SerializeField]
    private float distanceToLastSpawned = 10000.0f;
    [SerializeField]
    private float necessaryDistanceToSpawn = 0.0f;
    private bool nextVehiclePicked = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        vehicle = null;
        tempVehicle = null;

        if (GetComponentInChildren<ListOfVehicleTypes>())
        {
            VehicleTypes = GetComponentInChildren<ListOfVehicleTypes>().GetList();
        }
        else
        {
            Debug.Log("No List of Vehicles Attached");
        }
    }

    void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (!nextVehiclePicked)
        {
            tempVehicle = VehicleTypes[Random.Range(0, VehicleTypes.Count)];

            if (vehicle != null)
            {
                necessaryDistanceToSpawn = ai.distanceToRear + separation + tempVehicle.GetComponent<VehicleAI>().distanceToFront;
            }
            else
            {
                necessaryDistanceToSpawn = 0.0f;
            }

            nextVehiclePicked = true;
        }

        if (timeSinceLastSpawn > timeBetweenSpawns && distanceToLastSpawned > necessaryDistanceToSpawn)
        {
            vehicle = Instantiate(tempVehicle, transform);

            ai = vehicle.GetComponent<VehicleAI>();


            if (carMaxSpeed > 0.0f)
            {
                ai.SetSpeed(carMaxSpeed);
            }

            if (carAcceleration > 0.0f)
            {
                ai.SetAcceleration(carAcceleration);
            }

            if (carRotationSpeed > 0.0f)
            {
                ai.SetRotationSpeed(carRotationSpeed);
            }

            if (carDistanceToPoint > 0.0f)
            {
                ai.SetDistanceToPoint(carDistanceToPoint);
            }

            if (carHasLifetime)
            {
                ai.SetTimeUntilDeath(carLifetime);
            }

            if (carHasMaxDistance)
            {
                ai.SetMaxDistance(maxDistance);
            }
            
            timeSinceLastSpawn = 0.0f;

            nextVehiclePicked = false;
        }

        //Debug.Log(vehicle);

        if (vehicle != null)
        {
            distanceToLastSpawned = Vector3.Distance(vehicle.transform.position, transform.position);
        }
        else
        {
            distanceToLastSpawned = 10000.0f;
        }
	}
}
