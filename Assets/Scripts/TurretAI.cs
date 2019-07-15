using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{

    public int curHealth = 100;

    public float distance;

    public float wakerange= 5.75f;

    public float shottinterval;

    public float bulletspped = 5;

    public float bullettimer;

    public bool awake =false;

    public bool lookkingRight = true;

    public GameObject bullet;

    public Transform target;

    public Animator anim;

    public Transform shootpointL, shootpointR;

    private AudioSource audioSource;
    public AudioClip turretSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookRight", lookkingRight);

        RangeCeck();
        
        if (target.transform.position.x > transform.position.x)
        {
            lookkingRight = true;
        }
        
        if (target.transform.position.x < transform.position.x)
        {
            lookkingRight = false;
        }

        if (curHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    void RangeCeck()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < wakerange)
        {
            awake = true;
        }
        if (distance> wakerange)
        {
            awake = false;
        }
    }

    // sau thoi gian bullettimer se thu hien tao ra Bullet
    // va velocity = khoang cach x toc do.
    public  void Attack(bool attackright)
    {
        bullettimer += Time.deltaTime;
        if (bullettimer >= shottinterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (attackright)
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootpointR.transform.position, shootpointR.transform.rotation);
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletspped;

                bullettimer = 0;
            }
            
            if (!attackright)
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootpointL.transform.position, shootpointL.transform.rotation);
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletspped;

                bullettimer = 0;
            }
        }
    }

    public void Damage(int dmg)
    {
        audioSource.clip = turretSound;
        audioSource.Play();
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("redflash");
    }
}
