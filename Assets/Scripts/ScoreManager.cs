using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _playerScore;
    private int _AIScore;

    public Text playerSccoreTXT;
    public Text AIScoreTXT;

    [SerializeField] private int winningScore = 5; 

    public void PlayerGoal()
    {
        _playerScore++;
        playerSccoreTXT.text = _playerScore.ToString();
        CheckWinCondition();
    }

    public void AIGoal()
    {
        _AIScore++;
        AIScoreTXT.text = _AIScore.ToString();
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (_playerScore >= winningScore)
        {
            SceneManager.LoadScene(2); // Win sahnesi
        }
        else if (_AIScore >= winningScore)
        {
            SceneManager.LoadScene(3); 
        }
    }
}
