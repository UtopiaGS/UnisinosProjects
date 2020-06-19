using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Globalization;

public class LeaderboardManager : MonoBehaviour
{
    private List<int> ScoresOnLeaderboard = new List<int>();
    private List<string> NamesOnLeaderboard = new List<string>();
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
                PlayerPrefs.SetInt(Constants.LeaderboardTagScore + (i + 1).ToString(), i);

                PlayerNamePositions[i].text = PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString();
                PlayerScorePositions[i].text = PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1).ToString()).ToString();
                LeaderboardScores.Add(PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString(), PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1)));
                NamesOnLeaderboard.Add(PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString());
                ScoresOnLeaderboard.Add(PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1)));
            }
            else {
               PlayerNamePositions[i].text = PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString();
               PlayerScorePositions[i].text = PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1).ToString()).ToString();
                try
                {
                    LeaderboardScores.Add(PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString(), PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1)));
                    NamesOnLeaderboard.Add(PlayerPrefs.GetString(Constants.LeaderboardTagName + (i + 1).ToString()).ToString());
                    ScoresOnLeaderboard.Add(PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (i + 1)));
                }
                catch {
                    Debug.Log("Name already added on dictionary");
                }
              
            }

        }

        CheckPlayerPositionAndAdd(14, "ahsefsesefeae");
        CheckPlayerPositionAndAdd(215, "sefse");
        CheckPlayerPositionAndAdd(10, "hue");
        CheckPlayerPositionAndAdd(9, "br");
        CheckPlayerPositionAndAdd(13, "aheae");
       CheckPlayerPositionAndAdd(4, "aheae");
        CheckPlayerPositionAndAdd(2, "aheae");

    }


    public void CheckPlayerPositionAndAdd(int playerScore, string name) {
        if (playerScore >= PlayerPrefs.GetInt(Constants.LeaderboardTagScore + (_numberOfPositions - 1))){
            try
            {
                LeaderboardScores.Add(name, playerScore);
            }
            catch
            {
                Debug.Log("Taken");
            }
            //NamesOnLeaderboard.Add(name);
            //ScoresOnLeaderboard.Add(playerScore);

            List<KeyValuePair<string, int>> myList = LeaderboardScores.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

           
            Debug.Log("My List count is: " + myList.Count);
                       

            for (int i = 0; i < myList.Count; i++)
            {
                if (i >= _numberOfPositions)
                    return;
                Debug.Log("I is: " + i);
                Debug.Log("Check " + myList[i] + " at pos " + i);
                PlayerNamePositions[i].text = myList[i].Key;
                PlayerScorePositions[i].text = (myList[i].Value).ToString();

                PlayerPrefs.SetString(Constants.LeaderboardTagName + (i + 1).ToString(), myList[i].Key);
                PlayerPrefs.SetInt(Constants.LeaderboardTagName + (i + 1).ToString(), myList[i].Value);
            }


        }

        
    
    
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
