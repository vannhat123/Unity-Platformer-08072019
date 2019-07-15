using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemaster : MonoBehaviour
{
    public int points = 0;
    public int highscore;
    public Text highText;
    public Text inputText;
    public Text pointext;
    
    // Start is called before the first frame update
    // PlayerPrefs luu gia tri don gian.
    // If activesScereen.buildIndex gia tri man dau tien.
    // xoa gia tri points va cho ve 0.
    // Khi bat dau thi se hien thi gia tri highscore la diem cao nhat.
    // highscore luc dau dat gia tri la 0.
    // Ở màn 0 thì sẽ xóa points để giá trị 0. nghĩa là khi chêt sẽ quay lại màn 0.
    // còn else là ko chết thì sẽ lưu lại giá trị points để tính điểm tieepsp.
    void Start()
    {
        highText.text = ("HighScore: " + PlayerPrefs.GetInt("highscore"));
        highscore = PlayerPrefs.GetInt("highscore", 0);

        if (PlayerPrefs.HasKey("points"))
        {
            Scene activeScreen = SceneManager.GetActiveScene();
            if (activeScreen.buildIndex == 0)
            {
                PlayerPrefs.DeleteKey("points");
                points = 0;
            }
            else
            {
                points = PlayerPrefs.GetInt("points");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointext.text = ("Point: " + points);
    }
}
