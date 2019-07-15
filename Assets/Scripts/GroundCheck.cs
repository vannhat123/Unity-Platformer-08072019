using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MovingPlat mov;
    public Player player;

    public Vector3 movp;
    // Start is called before the first frame update
    // Su dung ben Player thong qua nhau co cap nhat.
    // tao them 1 trigger
    void Start()
    {
        mov = GameObject.FindGameObjectWithTag("Movingplat").GetComponent<MovingPlat>();
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    // Va cham trigger cua boxcollieder thu 2
    // Muốn khi va chạm shoe tăng speed trong 5 giây. Nhưng ko thể sử dụng lặp 5s bằng Time.deltatime vì 
    // nó chỉ gọi 1 lần chứ ko đặt trong update. nên sử dụng 1 cách duy nhất.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger==false )
        player.grounded = true;

        if (other.CompareTag("heart"))
        {
            player.ourHealth = 5;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("shoe"))
        {
            Destroy(other.gameObject);
            player.maxspeed = 6;
            player.speed = 100f;
            StartCoroutine(timecount(5));
        }
        
    }

    // dang va cham
    // o movingplat lay toc do cua moving + them gia tri player.x hien tai thi se di theo.
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.isTrigger==false || other.CompareTag("water") )
           player.grounded = true;

        if (other.isTrigger == false && other.CompareTag("Movingplat"))
        {
            movp = player.transform.position;
            movp.x += mov.speed ;
            player.transform.position = movp;
        }
    }

    // ket thuc va cham.
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.isTrigger==false || other.CompareTag("water") )
        player.grounded = false;
    }

    // tao ham nay se cho 5 giay sau do tra cac gia tri nhu sau.
    IEnumerator timecount(float time)
    {
        yield return  new WaitForSeconds(time);
        player.maxspeed = 3f;
        player.speed = 50f;
        yield return 0;
    }
}
