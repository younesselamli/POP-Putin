using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Text DisplayName;
    public Text Info;
    public InputField EditName;
    public ArabicFixer arabicFixer;

    private void Start()
    {
        Logining();
        
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
                GetPlayerProfile = true
            }

        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnLoginError);
    }

    private void OnLoginError(PlayFabError obj)
    {
        throw new NotImplementedException();
    }

    private void OnLoginSucces(LoginResult obj)
    {
        DisplayName.text = obj.InfoResultPayload.PlayerProfile.DisplayName;

        arabicFixer.Start();

    }
    public void Submit()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = EditName.text,

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdated, OnEditError);
    }

    private void OnEditError(PlayFabError obj)
    {
        Info.text = "Use Differnt name".ToString();
    }

    void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Your Name Has Been Updated !");
        Info.text = "Your Name Has Been Updated !".ToString();
        Info.color = Color.green;
        Start();
        
}
}
