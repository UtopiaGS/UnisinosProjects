using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public const string SceneGame = "Game";
    public const string SceneMenu = "Menu";
    // Start is called before the first frame update

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneGame);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneMenu);
    }
}
