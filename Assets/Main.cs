using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private FirebaseAuth mFirebaseAuth;
    private FirebaseUser mFirebaseUser;

    public FirebaseAuthentication firebaseAuthetntication;
    public GameObject panelLogin;

	// Use this for initialization
	void Awake () {
        mFirebaseAuth = FirebaseAuth.DefaultInstance;
        mFirebaseUser = mFirebaseAuth.CurrentUser;
        if(mFirebaseUser == null)
        {
            firebaseAuthetntication.gameObject.SetActive(true);
            panelLogin.SetActive(true);
            return;
        }

    }

    public void SignOut()
    {
        mFirebaseAuth.SignOut();
        firebaseAuthetntication.gameObject.SetActive(true);
        panelLogin.SetActive(true);
    }

}
