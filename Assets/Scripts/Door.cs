using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public int levelLoad = 1;

    public Gamemaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<Gamemaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            savescore();
            gm.inputText.text = (" Press E to Enter");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                savescore();
                SceneManager.LoadScene(levelLoad);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.inputText.text = (" ");
        }
    }

    // Luu lai points man 0.
    void savescore()
    {
        PlayerPrefs.SetInt("points", gm.points);
    }
}
