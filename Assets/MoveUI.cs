using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    [SerializeField]
    private int move_speed;

    [SerializeField]
    private Vector3 move_direction;

    [SerializeField]
    private bool moving = false;

    [SerializeField]
    private Transform target_pos;



	
	// Update is called once per frame
	void Update ()
    {
		if(moving)
        {
            if (target_pos && (Vector3.Distance(transform.position, target_pos.position) > 1))
            {
                GetComponent<RectTransform>().position = Vector3.MoveTowards(GetComponent<RectTransform>().position, target_pos.position, move_speed);
            }
            else
            {
                transform.position += (move_direction) * move_speed * Time.deltaTime;
            }
        }
	}

    public void Move(bool _moving)
    {
        moving = _moving;
    }
}
