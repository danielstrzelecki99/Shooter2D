using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class PlayFabManagerLogin : MonoBehaviour
{
    //UI fields
    [SerializeField] TextMeshProUGUI information;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button passwordResetButton;
    public Button switchToRegister;

    //Global variables of account statistics
    public static string username = "null";
    public static int level;
    public static int coins;
    public static int experience;
    public static int playedGames;
    public static int wins;
    public static int isLogged;

    //Login funcion
    public void LoginButton()
    {
        if(emailInput.text == "")
        {
            information.text = "The email address is empty!";
            return;
        }
        else if(passwordInput.text == "")
        {
            information.text = "The password is empty!";
            return;
        }
        else
        {
            var request = new LoginWithEmailAddressRequest {
                Email = emailInput.text,
                Password = passwordInput.text
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }
    }

    //Setting account status
    public void SetPlayerLoggedStatusToTrue(){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {StatisticName = "isLogged", Value = 1},
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatistics, OnError);
    }

    void OnUpdateStatistics(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Logged status has been updated");
    }

    //Getting username
    public void GetUsername()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result)
    {
        if(result.Data != null && result.Data.ContainsKey("Username")){
            username = result.Data["Username"].Value;
        }
        SceneManager.LoadScene("Loading");
    }

    //Check if accound is already logged
    public void GetPlayerLoggedStatus(){
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnLoggedStatusRecived, OnError);
    }

    void OnLoggedStatusRecived(GetPlayerStatisticsResult result){
        foreach(var stat in result.Statistics){
            if(stat.StatisticName == "isLogged"){
                isLogged = stat.Value;
            }
         }

        if (isLogged == 0) {
            information.text = "Logged in";
            GetUsername();
            GetStatistics();
            SetPlayerLoggedStatusToTrue();
        } else if (isLogged == 1) {
            information.text = "Someone is already logged to this account. Try again later.";
        }

    }

    //Getting account statistics
    public void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnStatisticsRecived, OnError);
    }

    void OnStatisticsRecived(GetPlayerStatisticsResult result)
    {
        Debug.Log("I got statistics!");
        foreach(var stat in result.Statistics){
            if(stat.StatisticName == "Level"){
                level = stat.Value;
            }
            if(stat.StatisticName == "Experience"){
                experience = stat.Value;
            }
            if(stat.StatisticName == "Coins"){
                coins = stat.Value;
            }
            if(stat.StatisticName == "PlayedGames"){
                playedGames = stat.Value;
            }
            if(stat.StatisticName == "Wins"){
                wins = stat.Value;
            }
        }
    }

    //Funcions after login
    public void OnLoginSuccess(LoginResult result)
    {
        GetPlayerLoggedStatus();
    }
    
    //PAssword reset
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInput.text,
            TitleId = "7DC52"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    public void OnPasswordReset(SendAccountRecoveryEmailResult resault)
    {
        information.text = "The reset email sent";
    }

    //Scene switch
    public void SwitchToRegister()
    {
        SceneManager.LoadScene("Registration");
    }

    //Buttons executions
    void Start()
    {
        loginButton.onClick.AddListener(() => { LoginButton();});
        passwordResetButton.onClick.AddListener(() => { ResetPasswordButton();});
        switchToRegister.onClick.AddListener(() => { SwitchToRegister();});
    }
    
    //Extra funcions
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
