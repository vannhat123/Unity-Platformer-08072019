using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothtimeX, smoothtimeY;

    public Vector2 veloctiy;

    public GameObject player;

    public Vector2 minpos, maxpos;

    public bool bound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    // Mathf.SmoothDamp-là từ tọa độ ban đầu của camera chuyển qua tọa độ nhìn của player
    // smoothtime độ trơn giống như tốc độ để camera nhìn theo, để 0.1 là đẹp.
    // cuối cùng là gán tọa độ posX posY cho tọa độ camera.
    // velocity là vận tốc lúc đầu là không. khi di chuyển sẽ tạo ra.
    // if(bound) tạo ra điều kiện nếu chọn bound thì sẽ tạo ra giới hạn camera theo nhân vật.
    // tieepsp theo math.Clamp tạo ra giới hạn camera ở minpos và maxpos.
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(this.transform.position.x, player.transform.position.x, ref veloctiy.x,
            smoothtimeX);
        float posY = Mathf.SmoothDamp(this.transform.position.y, player.transform.position.y, ref veloctiy.y,
            smoothtimeY);
        transform.position = new Vector3(posX,posY,transform.position.z);

        if (bound)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minpos.x, maxpos.x),
                    Mathf.Clamp(transform.position.y, minpos.y, maxpos.y),transform.position.z
                );
        }
    }
    
    
}
