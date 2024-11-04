using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreControllerMultiplayer : MonoBehaviour
{
    public string playerText;
    private int score;
    private TextMeshProUGUI score_text;

    private void Awake()
    {
        score = 0;
        score_text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateScore(int value)
    {
        score = score + value;
        RefreshUI();
    }

    private void RefreshUI()
    {
        score_text.text = playerText + score;
    }
}
