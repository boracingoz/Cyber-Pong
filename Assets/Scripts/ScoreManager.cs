using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _playerScore;
    private int _AIScore;

    public Text playerSccoreTXT;
    public Text AIScoreTXT;

    public void PlayerGoal()
    {
        _playerScore++;
        playerSccoreTXT.text = _playerScore.ToString();
    }

    public void AIGoal()
    {
        _AIScore++;
        AIScoreTXT.text = _AIScore.ToString();
    }
}