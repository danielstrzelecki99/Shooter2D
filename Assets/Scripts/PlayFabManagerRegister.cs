using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayFabManagerRegister : MonoBehaviour
{
    //UI fields
    [SerializeField] TextMeshProUGUI information;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public Button registerButton;
    public Button switchToLogin;

    //Registration
    public void RegisterButton()
    {
        if(emailInput.text == "")
        {
            information.text = "The email address is empty!";
            return;
        }
        else if(passwordInput.text.Length < 6)
        {
            information.text = "The password is too short!";
            return;
        }
        else if(usernameInput.text == "")
        {
            information.text = "The username is empty!";
            return;
        }
        else if(usernameInput.text.Length > 11)
        {
            information.text = "The username is too long!";
            return;
        }
        else 
        {
            var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            Username = usernameInput.text,
            RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
    }

    //Setting stats in Data
    public void SetStatsToData(){
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {"Username", usernameInput.text}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("User data updated");
    }

    //Setting username
    public void SetPlayerName(){
        var request = new UpdateUserTitleDisplayNameRequest {
                DisplayName = usernameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult resault)
    {
        Debug.Log("Name updated");
        SetPlayerStatistics();
    }

    //Setting statis in statistics
    public void SetPlayerStatistics(){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {StatisticName = "Level", Value = 1},
                new StatisticUpdate {StatisticName = "PlayedGames", Value = 0},
                new StatisticUpdate {StatisticName = "Wins", Value = 0},
                new StatisticUpdate {StatisticName = "Experience", Value = 0},
                new StatisticUpdate {StatisticName = "Coins", Value = 0}
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatistics, OnError);
    }

    void OnUpdateStatistics(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Statistics added");
        SetStatsToData();
    }

    //Funcions after register
    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        information.text = "Account registered successfully! Sign in now!";
        SetPlayerName();
    }

    //Switch
    public void SwitchToLogin()
    {
        SceneManager.LoadScene("Login");
    }

    //Buttons executions
    void Start()
    {
        registerButton.onClick.AddListener(() => { RegisterButton();});
        switchToLogin.onClick.AddListener(() => { SwitchToLogin();});
    }
    
    //Exta funcions
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    void OnError(PlayFabError error)
    {
        information.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
