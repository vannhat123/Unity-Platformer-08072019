using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Player player;

    public int damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    // sau khi dẫm phải mũi nhọn sẽ bị nhảy lên 1 lực 350f. và tọa độ giữ nguyên.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.Damage(damage);
            player.Knockback(350f,player.transform.position);
        }
    }
}
