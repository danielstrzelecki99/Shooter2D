using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI information;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;
    public Button passwordResetButton;

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
        else 
        {
            var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
    }

    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        information.text = "Registered and logged in";
        SceneManager.LoadScene("Loading");
    }

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

    public void OnLoginSuccess(LoginResult resault)
    {
        information.text = "Logged in";
        SceneManager.LoadScene("Loading");
    }

    void Start()
    {
        //Login();
        registerButton.onClick.AddListener(() => { RegisterButton();});
        loginButton.onClick.AddListener(() => { LoginButton();});
    }
    


    // void Login()
    // {
    //     var request = new LoginWithCustomIDRequest {
    //         CustomId = SystemInfo.deviceUniqueIdentifier,
    //         CreateAccount = true
    //     };
    //     PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    // }

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
