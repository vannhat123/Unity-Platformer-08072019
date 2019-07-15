using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    public float lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // other.isTrigger kiem tra neu ko phai trigger thi thuc hien.
    // sau khi ban vao nhan vat xong thi tu dong pha huy bullet.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger ==false)
        {
            if (other.CompareTag("Player"))
            {
                other.SendMessageUpwards("Damage", 1);
            }
            Destroy(gameObject);
        }
        
    }

    // Sau 2 giay xoa vien bullet.
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
