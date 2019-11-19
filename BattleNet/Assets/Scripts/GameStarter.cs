using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public const string SceneGame = "Game";
    // Start is called before the first frame update
   
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneGame);
    }
}
