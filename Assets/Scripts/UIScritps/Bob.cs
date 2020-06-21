using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField]
    private bool up_down = false;
    [SerializeField]
    private bool left_right = false;

    [SerializeField]
    private bool moving_positive = true;
    private float delta_time = 0;

    [SerializeField]
    private float freq_direction_change;

    [SerializeField]
    private float bob_speed;

    // Update is called once per frame
    void Update()
    {
        delta_time += Time.deltaTime;

        if (delta_time > freq_direction_change)
        {
            moving_positive = !moving_positive;
            delta_time = 0;
        }

        if (moving_positive)
        {
            if (up_down)
            {
                transform.position += new Vector3(0, bob_speed);
            }
            else if(left_right)
            {
                transform.position += new Vector3(bob_speed, 0);
            }
        }
        else
        {
            if (up_down)
            {
                transform.position -= new Vector3(0, bob_speed);
            }
            else if (left_right)
            {
                transform.position -= new Vector3(bob_speed, 0);
            }
        }
    }
}