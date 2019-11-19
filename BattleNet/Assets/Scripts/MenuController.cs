using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public const string SceneMenu = "Menu";
    public const string SceneGame = "Game";
    public static MenuController instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void BackToMainMenu() {
        SceneManager.LoadScene(SceneMenu);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneGame);
    }
}
