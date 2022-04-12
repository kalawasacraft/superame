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
    
    private const string projectId = "projectId"; // change to your projectId
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";
    private const string apiKey = "fsDF?EW?FER?e?F?QWD?F??F?EW??ewfew"; // change to yout api key Project

    // user for Authentication (example)
    private const string userEmail = "user@example.com";
    private const string userPassword = "12345678";

    // tables in Database
    private static readonly string mapsTable = (inProduction ? "maps" : "test_maps");
    private static readonly string recordsTable = (inProduction ? "records" : "test_records");
    private static readonly string playersTable = (inProduction ? "players" : "test_players");

    public static string GetDatabaseURL()
    {
        return databaseURL;
    }

    public static string GetApiKey()
    {
        return apiKey;
    }

    public static string GetUser()
    {
        return userEmail;
    }

    public static string GetPassword()
    {
        return userPassword;
    }

    public static string GetMapsTable()
    {
        return mapsTable;
    }

    public static string GetRecordsTable()
    {
        return recordsTable;
    }

    public static string GetPlayersTable()
    {
        return playersTable;
    }
}
