using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{

    private int score = 0;
    private Text scoreField;

    // Use this for initialization
    void Start()
    {
        scoreField = GetComponent<Text>();
        Reset();
    }


    public void Score(int points)
    {
        score += points;
        UpdateScoreField();
    }

    public void Reset()
    {
        score = 0;
        UpdateScoreField();
    }

    public void SetScore(int points)
    {
        score = points;
    }

    void UpdateScoreField()
    {
        scoreField.text = score.ToString();
    }
}