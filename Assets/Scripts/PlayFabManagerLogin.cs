using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayFabManagerLogin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI information;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button passwordResetButton;
    public Button switchToRegister;
    public string username = "null";

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

    public void GetUsername(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result){
        Debug.Log("I got data!");
        if(result.Data != null && result.Data.ContainsKey("Username")){
            username = result.Data["Username"].Value;
        }
    }

    public void OnLoginSuccess(LoginResult result)
    {
        information.text = "Logged in";
        SceneManager.LoadScene("Loading");
        GetUsername();
        Debug.Log(username);

    }

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

    public void SwitchToRegister()
    {
        SceneManager.LoadScene("Registration");
    }

    void Start()
    {
        loginButton.onClick.AddListener(() => { LoginButton();});
        passwordResetButton.onClick.AddListener(() => { ResetPasswordButton();});
        switchToRegister.onClick.AddListener(() => { SwitchToRegister();});
    }
    
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
