using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    GameObject startmenu, lvlselc, loadingscreen;
    public void quit()
    {
        Debug.Log("Exitting");
        Application.Quit(0);
    }

    public void options()
    {
        Debug.Log("Coming Soon!");
    }

    public void lvl_selec()
    {
        startmenu.SetActive(false);
        lvlselc.SetActive(true);
    }
    public void startgame(int n)
    {
        lvlselc.SetActive(false);
        loadingscreen.SetActive(true);
        SceneManager.LoadSceneAsync(n);
    }
}
