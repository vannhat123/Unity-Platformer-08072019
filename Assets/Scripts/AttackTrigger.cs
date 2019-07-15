using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public int dmg = 20;
   
    // Nếu collider va chạm không phải là trigger và other va chạm là Enemy thì nó sẽ...
  // SendMessageUpwards gui? gia tri dmg vao Function Damage();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger != true && other.CompareTag("Enemy"))
        {
            other.SendMessageUpwards("Damage", dmg);
        }
    }
}
