using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;


public class FinalScore : MonoBehaviour
{

    public Text finalScoreText;

    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            int score = ScoreManager.Instance.GetScore(); 
            finalScoreText.text = "Final Score: " + score.ToString(); 
        }
    }
}
