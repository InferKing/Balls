using UnityEngine;
using System.Collections;
using System;

public class PlayerData : MonoBehaviour
{
    public static Action<int> OnPlayerDataChange;
    public int score = 0;
    private int bestScore = 0;

    private void Start()
    {
        OnPlayerDataChange = AddScore;
    }
    //private void Update()
    //{
    //    if (!isStarted)
    //    {
    //        isStarted = true;
    //        StartCoroutine(CheckBalls(5));
    //    }
    //}

    private void AddScore(int x)
    {
        score += x;
        if (score > bestScore)
        {
            bestScore = score;
        }
        TextController.OnTextUpdated?.Invoke(score.ToString());
    }

    
}
