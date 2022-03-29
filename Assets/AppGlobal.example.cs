// Create cs file AppGlobal and copy this code
// In realtime database of Firebase create these tables:
/*
    maps: {
        map01: {
            completed: 0
        },
        .
        .
        .
    }

    test_maps: {
        map01: {
            completed: 0
        },
        .
        .
        .
    }

    players: {
        map01: {
            player_0: {
                completed: 0
            },
            .
            .
            .
        },
        .
        .
        .
    }
    
    test_players: {
        map01: {
            player_0: {
                completed: 0
            },
            .
            .
            .
        },
        .
        .
        .
    }
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppGlobalExample
{
    private static bool inProduction = false; // false: testing,  true: production
    private const string projectId = "projectId-default";
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

    public static string GetDatabaseURL()
    {
        return databaseURL;
    }

    public static bool GetInProduction()
    {
        return inProduction;
    }
}
