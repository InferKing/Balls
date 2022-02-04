using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Material[] ballMaterials;
    [SerializeField] private GameObject[] pointsOfSpawn;
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int maxBalls;
    [SerializeField] private Data playerDataClass;
    public static Action<GameObject,GameObject> OnBallDown;
    public static Action<int> OnBallDestroyed;
    private List<int> countOfBalls;
    private bool isEnd = false;
    private int count = 3;
    
    private void Start()
    {
        countOfBalls = new List<int>();

        OnBallDown = SpawnBalls;
        OnBallDestroyed = AddCount;

        playerDataClass.LoadGameData(); // default or from file
        TextController.OnTextUpdated?.Invoke(playerDataClass.scoreToSer.ToString());
        TextController.OnTextUpdated?.Invoke("0");

        SpawnPlatforms();

        
    }

    private void Update()
    {
        if (playerController._isTouched && !isEnd)
        {
            isEnd = true;
            StartCoroutine(CheckBalls(1.5f));
        }
    }

    private void SpawnBalls(GameObject platform, GameObject ball)
    {
        GameObject gObj = null;
        MultiBehaviour multi = platform.GetComponent<MultiBehaviour>();
        if (count + 3 <= maxBalls)
        {
            count += multi.multi;
            for (int i = 0; i < multi.multi; i++)
            {
                gObj = Instantiate(ball);
                gObj.GetComponent<BallBehaviour>().indexes.Add(platform.GetInstanceID());
                if (ball.GetComponent<BallBehaviour>().GetStatus())
                {
                    gObj.transform.position = platform.transform.position;
                }
                else
                {
                    gObj.transform.position = ball.transform.position;
                }
                gObj.GetComponent<MeshRenderer>().material = ballMaterials[UnityEngine.Random.Range(0, ballMaterials.Length)];
            }
        }
        else
        {
            PlayerData.OnPlayerDataChange?.Invoke(multi.multi);
        }
    }

    private void SpawnPlatforms()
    {
        foreach (var item in pointsOfSpawn)
        {
            if (UnityEngine.Random.Range(0,2) == 0)
            {
                GameObject gObj = null;
                gObj = Instantiate(platforms[UnityEngine.Random.Range(0, platforms.Length)]);
                gObj.transform.position = item.transform.position;
            }
        }
    }

    private IEnumerator CheckBalls(float duration)
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Ball");
            Debug.Log($"Balls on scene before check win/lose: {gameObjects.Length}");
            StartCoroutine(CheckStackedBalls(gameObjects.Length));
            if (gameObjects.Length == 0)
            {
                ShowWinLoseText();
                yield return new WaitForSeconds(3);
                RestartButton.RestartGame();
            }

        }
    }

    private IEnumerator CheckStackedBalls(int length)
    {
        countOfBalls.Add(length);
        if (countOfBalls.Count == 3)
        {
            int c = 0, tempItem = countOfBalls[0];
            foreach (var item in countOfBalls)
            {
                if (item == tempItem)
                {
                    c++;
                }
            }
            if (c == 3)
            {
                PlayerData.OnPlayerDataChange?.Invoke(length);
                ShowWinLoseText();
                yield return new WaitForSeconds(3);
                RestartButton.RestartGame();
            }
            else
            {
                countOfBalls.RemoveAt(0);
            }
        }
        yield return null;
    }

    private void ShowWinLoseText()
    {
        if (playerDataClass.scoreToSer < playerData.score)
        {
            playerDataClass.scoreToSer = playerData.score;
            playerDataClass.SaveGameData();
            TextController.OnWinLoseTextUpdated?.Invoke(true);
        }
        else
        {
            TextController.OnWinLoseTextUpdated?.Invoke(false);
        }
    }
    private void AddCount(int x)
    {
        count -= x;
    }
}
