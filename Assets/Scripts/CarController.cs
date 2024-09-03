using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    Transform player;
    public int speed = 10;
    public int distance = 100;
    public Animator FrontRight, FrontLeft, BackRight, BackLeft;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("PlayerController").transform;
        int r = Random.Range(1, 3);
        if (r == 1)
        {
            transform.position = new Vector3(-6, transform.position.y, transform.position.z);
        }
        else if (r == 2)
        {
            transform.position = new Vector3(6, transform.position.y, transform.position.z);
        }
        else if (r == 3)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if (!GameObject.Find("PlayerController").GetComponent<PlayerController>().IsDead)
        {
            if (Vector3.Distance(transform.position, player.position) <= distance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
                //
                FrontRight.SetTrigger("Move");
                FrontLeft.SetTrigger("Move");
                BackRight.SetTrigger("Move");
                BackLeft.SetTrigger("Move");

            }
            else
            {
                FrontRight.SetTrigger("Stop");
                FrontLeft.SetTrigger("Stop");
                BackRight.SetTrigger("Stop");
                BackLeft.SetTrigger("Stop");
            }
        }
    }
}
