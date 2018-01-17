using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRegister : MonoBehaviour {

    public InputField emailInput;
    public InputField passwordInput;
    public Button registerButton;

    public FirebaseAuthentication fireBaseAuthentication;

    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        fireBaseAuthentication.CreateUserAsync(email, password);
    }

    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        fireBaseAuthentication.SignInWithEmailAndPassword(email, password);
    }
}
