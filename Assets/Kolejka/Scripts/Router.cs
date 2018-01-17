using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class Router : MonoBehaviour
{
    private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    public static DatabaseReference Users()
    {
        return baseRef.Child(FirebasePath.USER);
    }
    public static DatabaseReference UsersWithID(string uid)
    {
        return baseRef.Child(FirebasePath.USER).Child(uid);
    }
}
