using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlat : MonoBehaviour
{

    public Rigidbody2D r2;

    private float timedelay =1 ;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // không thể gọi hàm fall() đơn giản phải thêm StartCoroutine vì nó là IEnumerator.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            StartCoroutine(fall());
        }
    }

    // phải tạo IEnumertor thì mới sử dụng được dealy 2s.
    // sau 2s thay đổi bodyType thành Dynamic.
    IEnumerator fall()
    {
        yield return new WaitForSeconds(timedelay);
        r2.bodyType = RigidbodyType2D.Dynamic;
        yield return 0;
    }
    
}
