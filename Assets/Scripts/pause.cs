using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    [SerializeField]
    GameObject Menu;

    bool paused=false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause_game();
    }
    public void pause_game()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            Menu.SetActive(false);
            paused = false;
            Debug.Log("Resumed");
        }
        else
        {
            Time.timeScale = 0f;
            Menu.SetActive(true);
            paused = true;
            Debug.Log("Paused");
        }
    }

    public void options()
    {
        Debug.Log("Comming Soon!");
    }

    public void quit()
    {
        Debug.Log("Quitting");
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
