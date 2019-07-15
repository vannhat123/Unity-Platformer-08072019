using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackdelay = 0.4f;

    public bool attacking = false;

    public Animator anim;

    public Collider2D trigger;

    private AudioSource audioSource;
    public AudioClip playerAttackSound;
    private Player player;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    // If 1- nếu ấn nút z  và !attacking thì thay đổi giá trị attacking để thực hiện lệnh dưới.
    // đặt trigger.enabled= true để trigger hoạt động.
    // if2 sẽ chạy sau khi có điều kiện attacking đúng.
    // nếu attackdelay>0 thì cho - Time.delaTime để giá trị nó giảm đi.
    // Sau đó chạy xuongs lệnh else kết thúc attack và cho attacking= false/. mục địch if-else tiếp là để attack trong 0.3s rồi quay lại ban đầu.
    // trigger.enabled để đổi kiếm thành trigger thì mới chém được.
    // player.grounded xac dinh nhan vat o mat dat thi Z moi xay ra.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !attacking && player.grounded ) 
        {
            audioSource.clip = playerAttackSound;
            audioSource.Play();
            attacking = true;
            trigger.enabled = true;
            attackdelay = 0.3f;
        }

        if (attacking)
        {
            if (attackdelay > 0)
            {
                attackdelay -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                trigger.enabled = false;
            }
        }
        anim.SetBool("Attacking", attacking);
        
    }
}
