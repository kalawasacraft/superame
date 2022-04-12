using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    /// <summary>
    /// This class is just the implementation of all the functions shown in AuthHandler
    /// - It will sign up a user to Firebase Auth
    /// - It will sign in a user to Firebase Auth
    /// </summary>

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnAppStart()
    {
        //Debug.Log("init app");
        //AuthHandler.SignUp("user@example.com", "12345678", new User("user", "user", 20));
        //AuthHandler.SignIn("user@example.com", "12345678");
    }
}
