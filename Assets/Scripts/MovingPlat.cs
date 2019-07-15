using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour
{
    public float speed = 0.02f, changeDirection = -1;

    private Vector3 Move;

    public PauseMenu pauseplat;
    // Start is called before the first frame update
    void Start()
    {
         Move = transform.position;
         pauseplat = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PauseMenu>();
    }

    // Update is called once per frame
    // nếu pause thì tọa độ MovePlat ko thay đổi, nếu hết pause thì mới chạy.
    void Update()
    {
        if (pauseplat.pause)
        {
            transform.position = transform.position;
        }

        if (pauseplat.pause == false)
        {
            Move.x += speed;
            transform.position = Move;
        }
       
    }

    // if- neu va cham voi 1 collision khac ten la Ground
     void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("ground"))
        {
            speed *= changeDirection;
        }
    }
}
