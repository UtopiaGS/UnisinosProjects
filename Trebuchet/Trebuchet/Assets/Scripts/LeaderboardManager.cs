using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    private Dictionary<string, int> LeaderboardScores = new Dictionary<string, int>();
    public List<Text> PlayerNamePositions = new List<Text>();
    public List<Text> PlayerScorePositions = new List<Text>();
    // Start is called before the first frame update
    private int _numberOfPositions = 5;
    void Start()
    {
        for (int i = 0; i < _numberOfPositions; i++)
        {
            if (!PlayerPrefs.HasKey(Constants.LeaderboardTagName + (i + 1).ToString()))
            {
                string name = Constants.LeaderboardTagName + (i + 1).ToString();
                PlayerPrefs.SetString(Constants.LeaderboardTagName + (i + 1).ToString(), "Empty " + (i + 1).ToString());
                PlayerPrefs.SetInt(Constants.LeaderboardTagScore + (i + 1).ToString(), 0);

                PlayerNamePositions[i].text = PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString();
                PlayerScorePositions[i].text = PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1).ToString()).ToString();
                LeaderboardScores.Add(PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString(), PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1)));
            }
            else {
                PlayerNamePositions[i].text = PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString();
                PlayerScorePositions[i].text = PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1).ToString()).ToString();
                LeaderboardScores.Add(PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString(), PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1)));
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
