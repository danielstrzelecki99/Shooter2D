using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabManagerRegister : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI information;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public Button registerButton;
    public Button switchToLogin;

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

    public void SetPlayerName(){
        var request = new UpdateUserTitleDisplayNameRequest {
                DisplayName = usernameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult resault)
    {
        Debug.Log("Name updated");
    }

    public void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        information.text = "Registered and logged in";
        SceneManager.LoadScene("Loading");
        SetPlayerName();
    }

    public void SwitchToLogin()
    {
        SceneManager.LoadScene("Login");
    }

    void Start()
    {
        registerButton.onClick.AddListener(() => { RegisterButton();});
        switchToLogin.onClick.AddListener(() => { SwitchToLogin();});
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
