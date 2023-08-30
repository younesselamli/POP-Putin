using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using UnityEngine.UI;
using PlayFab.ClientModels;
using System;

public class LeaderB : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowParent;
    public ArabicFixer arabicFixer;
    string loggedInPlayFabId;

    private float time = 0.0f;
    public float interpolationPeriod = 0.9f;

    void Start()
    {
        Logining();
        arabicFixer =rowPrefab.GetComponentInChildren<ArabicFixer>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= interpolationPeriod)
        {
            time = 0.0f;
            GetLeaderBoard();

        }

    }
    void Logining()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true

        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    }

   

    private void OnSucces(LoginResult obj)
    {
        loggedInPlayFabId = obj.PlayFabId;
        Debug.Log("Login Succesfully");
        
    }
    void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Countries",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }

    private void OnError(PlayFabError obj)
    {
        Debug.Log(obj + "Login error");
    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        time += Time.deltaTime;
            if (time >= interpolationPeriod)
            {
                
            }
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            
                GameObject newGo = Instantiate(rowPrefab, rowParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            Image[] img = newGo.GetComponentsInChildren<Image>();
            texts[0].text = (item.Position + 1).ToString();
            texts[2].text = item.StatValue.ToString();
            //arabicFixer.Start();
            if (item.DisplayName != null)
            {
                texts[1].text = item.DisplayName;
                
            }

            else
            {
                texts[1].text = item.PlayFabId;
                texts[2].text = item.StatValue.ToString();
            }
                

            if (item.PlayFabId == loggedInPlayFabId)
            {
                img[0].color = Color.white;
                //texts[0].color = Color.blue;
                //texts[0].fontSize = 100;
                //texts[1].color = Color.gray;
                //texts[2].color = Color.green;
            }
            
        }
        
    }
}
