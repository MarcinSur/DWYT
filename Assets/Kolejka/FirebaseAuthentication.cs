using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using System;
using UnityEngine.UI;
using Firebase.Unity.Editor;
using Firebase.Database;
using Assets.Kolejka.Scripts;

public class FirebaseAuthentication : MonoBehaviour
{

    public Text debugText;
    public Kolejka.Events.Event onSignInEvent;
    public InputField inptMessage;

    private FirebaseAuth auth;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            debugText.text = "Start";
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
                debugText.text = "dependencyStatus";
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependecies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        auth.IdTokenChanged += IdTokenChanged;

        debugText.text = "InitializeFirebase";
        //AppOptions ops = new Firebase.AppOptions();
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://kolejka-3eb2b.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

        StartCoroutine(StartListener());

        //likesRef.KeepSynced(true);

        AuthStateChanged(this, null);
    }

    IEnumerator StartListener()
    {
        yield return new WaitForEndOfFrame();
        FirebaseDatabase.DefaultInstance
            .GetReference("messages")
            .ChildAdded += AddChildTodatabse;
    }

    private void AddChildTodatabse(object sender, ChildChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("AddChildTodatabse" + e.DatabaseError.Message);
            return;
        }

        Debug.LogWarning("AddChildToDatabase");

        var msgDict = (IDictionary<string, object>)e.Snapshot.Value;
        Message msg = new Message(msgDict);
        panelChat.CreateRow(msg);
    }

    public PanelChat panelChat;
    public void TestAddChildToPanel()
    {
        for (int i = 0; i < 10; i++)
        {
            Message msg = new Message("aaa" + i, "b", "c");
            panelChat.CreateRow(msg);
        }
    }
    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        if (e.Snapshot != null && e.Snapshot.ChildrenCount > 0)
        {
            foreach (DataSnapshot snapshot in e.Snapshot.Children)
            {
                var msgDict = (IDictionary<string, object>)snapshot.Value;
                Message msg = new Message(msgDict);
                panelChat.CreateRow(msg);
            }
        }
    }

    public void AddMessageToDatabase()
    {

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Message user = new Message("m", inptMessage.text, inptMessage.text);

        string json = JsonUtility.ToJson(user);

        reference.Child("messages").Push().SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                debugText.text = "msg push failed";
                return;
            }
            debugText.text = " msg push successful";
        });
    }


    private void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth.IdTokenChanged -= IdTokenChanged;
        auth = null;
    }

    public void CreateUserAsync(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                debugText.text = "CreateUserWithEmailAndPasswordAsync canceled";
                return;
            }
            if (task.IsFaulted)
            {
                debugText.text = "CreateUserWithEmailAndPasswordAsync faulted " + task.Exception;
                return;
            }

            FirebaseUser newUser = task.Result;
        });
    }

    public void ResendVerificationEmail()
    {
        FirebaseUser newUser = auth.CurrentUser;
        newUser.SendEmailVerificationAsync().ContinueWith(emailTask =>
        {
            if (emailTask.IsCanceled)
            {
                debugText.text = "ResendVerificationEmail emailTask.IsCanceled";
            }
            if (emailTask.IsFaulted)
            {
                debugText.text = "ResendVerificationEmail emailTask.IsFaulted";
            }
            if (emailTask.IsCompleted)
            {
                Debug.Log("ResendVerificationEmail email send to " + newUser.Email);
                debugText.text = "ResendVerificationEmail email send to " + newUser.Email;
            }

        });
    }

    public void SignInWithEmailAndPassword(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                debugText.text = "SignInWithEmailAndPassword IsCanceled";
            }
            if (task.IsFaulted)
            {
                debugText.text = "SignInWithEmailAndPassword IsFaulted";
            }
            if (task.IsCompleted)
            {
                debugText.text = "SignInWithEmailAndPassword LOGIN = " + auth.CurrentUser.Email;
            }
        });
    }

    private void IdTokenChanged(object sender, EventArgs e)
    {
        Debug.Log("IdTokenChanged");
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        FirebaseUser user = null;
        debugText.text = "AuthStateChanged ";

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                debugText.text = "Signed out " + user.IsEmailVerified;
            }
            user = auth.CurrentUser;
            if (signedIn)
            {

                debugText.text = "Signed in " + user.IsEmailVerified;
                onSignInEvent.Raise();
            }
        }
        debugText.text = "AuthStateChanged END " + auth.CurrentUser.Email;
    }


}
