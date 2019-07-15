using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 50f, maxspeed = 3, maxjumb=4, jumPow = 220f;
    public bool grounded = true, faceright = true, doublejump = false;
    
    public Rigidbody2D r2;

    public Animator anim;

    public int ourHealth;
    public Gamemaster gm;
    
    private int maxHealth= 5;

    private AudioSource audioSource;

    public AudioClip playerCoinsSound;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<Gamemaster>();
        ourHealth = maxHealth;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    // r2.velocity.x toc do hien tai cua nguoi choi
    // mathf.abs tra ve gia tri duong.
    // neu an space thi se nhay len va chuyen ve grounded false;
    // vector2.up tac dung 1 luc theo phuong y nhay len.
    // 37, khi nhay xong cho doubleJump true de thuc hien tiep o Else.
    // 46. r2.velocity.x ko thay doi toa do x khi nhay lan 2. de y:0 de AddForce cong voi lan thu 1.
    // sau khi nhay xong phai doublejump false ko no se nhay lien tuc.
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                doublejump = true;
                r2.AddForce(Vector2.up*jumPow);
            }
            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up*jumPow*0.9f);
                }
            }
        }
    }

    // Update moi 0.2S xem nguoi choi chon di huong nao.
    // Vector2.right anh huong theo truc. X.
    // them dieu kien neu toc do nho hoac lon hon 3 thi se doi toc do la 3.
    // h>0 di ben tay  phai, !faceright la huong trai.
    // h<0 di ben tay trai, faceright huong ben phai thi se goi Flip
    // 54-58. de re.velocity thay vi postion. vi khi nhay len se ko bi keo dai khi di chuyen x duoc.
    // r2.velocity.x toc do theo phuong x.
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right)*speed*h);

        if (r2.velocity.x > maxspeed)
        {
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        }
        if (r2.velocity.x < -maxspeed)
        {
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);
        }

        if (r2.velocity.y > maxjumb)
        {
            r2.velocity = new Vector2(r2.velocity.x, maxjumb);
        }
        if (r2.velocity.x < -maxjumb)
        {
            r2.velocity = new Vector2(r2.velocity.x, -maxjumb);
        }
        
        
        if (h>0 && !faceright)
        {
            Flip();
        }

        if (h < 0 && faceright)
        {
            Flip();
        }
        
        // nếu người chơi đứng dưới đất thì tạo 1 vector mới thay đổi giá trị hiện tại 70%.
        // giảm ma sát.
        // giảm trơn thì tạo plaform ở mặt đất. chọn dấu tích ở Boxcollider "Used By Efector". sau đó bỏ chọn use one way ở platform.
        if (grounded)
        {
            r2.velocity = new Vector2(r2.velocity.x* 0.7f, r2.velocity.y*1f);
        }

        if (ourHealth<=0)
        {
            Deadth();
        }
    }

    // thay doi scale o tranfrom. *-1 de doi huong.
    // transform.localScale la gia tri 1 cua Scale;
    public void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    // Gia tri highscore được tạo bên Gamemaster.
    // Khi nguoi choi Death thi so sanh gia tri highscore neu nho hon gm.points la diem cua nguoi choi
    // thi cho gia tri highscore= gm.points.
    public void Deadth()
    {
        SceneManager.LoadScene(0);

        if (PlayerPrefs.GetInt("highscore") < gm.points)
        {
            PlayerPrefs.SetInt("highscore", gm.points);
        }
    }

    public void Damage(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
    }

    // Hàm này để xử lí khi player va chạm vào Spike ( bình thường sẽ vẫn ở trong và không bị gây dame nữa)
    // truyền vận tốc cho nó khi ở trong để khi vào spike tự thay đổi vận tốc nhỏ.
    // sau đó AddForce tác dụng lực. hướng x ngược lại. còn y thì đẩy lực lên trên 1 chút.
    public void Knockback(float Knockpowm, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x*-100, Knockdir.y*Knockpowm));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coins"))
        {
            audioSource.clip = playerCoinsSound;
            audioSource.Play();
            Destroy(other.gameObject);
            gm.points += 1;
        }
    }
}
