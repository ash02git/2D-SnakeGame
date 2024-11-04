using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score;
    private int multiplier;
    private TextMeshProUGUI score_text;

    private void Awake()
    {
        score = 0;
        multiplier = 1;
        score_text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateScore(int value)
    {
        score = score +value*multiplier;
        RefreshUI();
    }

    public void MultiplierUpdate(int value)
    {
        multiplier = value;
    }
    private void RefreshUI()
    {
        score_text.text = "Score : "+score;
    }
    
}
