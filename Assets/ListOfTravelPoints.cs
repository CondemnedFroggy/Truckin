using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfTravelPoints : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> points;

    private void Start()
    {
        points = new List<GameObject>();
               
        foreach (Transform tp in transform)
        {
            points.Add(tp.gameObject);
        }
    }

    public List<GameObject> GetListOfTravelPoints()
    {
        return points;
    }
}
