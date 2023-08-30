using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;

public class Login : MonoBehaviour
{

    [Header("Window")]
    public GameObject nameWindow;
    [Header("Edit Name")]
    public InputField nameInput;
    public Text errorTxt;
    public int Score;
    public Text scoretxt;




    // Start is called before the first frame update
    private void Start()
    {
        Logining();
    }
    public void Start(GetPlayerStatisticsResult result)
    {
        
        //Check each statistic in the list to see if it's the one you want
        foreach (var eachStat in result.Statistics)
        {
            if (eachStat.StatisticName.Equals("Score"))
            {
                Score = eachStat.Value;
                scoretxt.text = Score.ToString();
            }

        }
    }
    void Logining()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
            , 
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
                

    }

        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    }

    private void OnError(PlayFabError obj)
    {
        Debug.Log(obj+"Error 404!");
    }

    private void OnSucces(LoginResult obj)
    {
        Debug.Log("Login Succesfully");
        string name = null;
        if (obj.InfoResultPayload.PlayerProfile != null)
            name = obj.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
            nameWindow.SetActive(true);
        else
            nameWindow.SetActive(false);


    }

    private void OnError2(PlayFabError obj)
    {
        Debug.Log(obj+"error not sending data");
    }

    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics =new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="Countries",
                    Value= score
                    
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSucces, OnError2);
    }
    

    private void OnSuccess(GetPlayerStatisticsResult obj)
    {
        foreach (var eachStat in obj.Statistics)
        {
            if (eachStat.StatisticName.Equals("Countries"))
            {
                Score = eachStat.Value;
                scoretxt.text = Score.ToString();
            }

        }
    }

    private void OnSucces(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Player Update his Score");
    }
    public void SubmitButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdated, OnError3);
    }

    private void OnError3(PlayFabError obj)
    {
        errorTxt.text = obj.ToString();
    }

    void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        
        Start();
        Debug.Log("Your Name Has Been Updated !");
        nameWindow.SetActive(false);
    }
}
