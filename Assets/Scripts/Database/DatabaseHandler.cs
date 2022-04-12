using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Proyecto26;

public static class DatabaseHandler
{
    private static readonly string databaseURL = AppGlobal.GetDatabaseURL();
    private static readonly string mapsTable = AppGlobal.GetMapsTable();
    private static readonly string recordsTable = AppGlobal.GetRecordsTable();
    private static readonly string playersTable = AppGlobal.GetPlayersTable();

    private static readonly string apiKey = AppGlobal.GetApiKey();
    private static readonly string authUser = AppGlobal.GetUser();
    private static readonly string authPassword = AppGlobal.GetPassword();

    private static fsSerializer serializer = new fsSerializer();

    public delegate void SignInCallback();
    public static string idToken;

    public delegate void PostUserCallback();
    public delegate void PostMapCallback();
    public delegate void PostRecordCallback();
    public delegate void PostPlayerCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetMapCallback(Map map);
    public delegate void GetRecordCallback(Record record);
    public delegate void GetPlayerCallback(Player player);
    public delegate void GetMapsCallback(Dictionary<string, Map> maps);
    public delegate void GetTopRecordsCallback(Dictionary<string, Record> records);
    public delegate void GetPlayersCallback(Dictionary<string, Player> players);

    public static void GetMap(string mapId, GetMapCallback callback)
    {
        RestClient.Get<Map>($"{databaseURL}{mapsTable}/{mapId}.json").Then(map => { 
            callback(map);
        }).Catch(err => {

            //GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    public static void GetMaps(GetMapsCallback callback)
    {
        RestClient.Get($"{databaseURL}{mapsTable}.json").Then(response =>
        {
            var responseJson = response.Text;
            
            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Map>), ref deserialized);

            var maps = deserialized as Dictionary<string, Map>;

            callback(maps);
        }).Catch(err => {

            Debug.Log(err);
        });
    }

    public static void GetPlayer(string mapId, int playerId, GetPlayerCallback callback)
    {
        string stringPlayerId = "player_" + playerId.ToString();
        RestClient.Get<Player>($"{databaseURL}{playersTable}/{mapId}/{stringPlayerId}.json").Then(player => { 
            callback(player);
        }).Catch(err => {

            GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    public static void GetPlayers(string mapId, GetPlayersCallback callback)
    {
        RestClient.Get($"{databaseURL}{playersTable}/{mapId}.json").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            var players = new Dictionary<string, Player>();

            if (!data.IsNull) {
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, Player>), ref deserialized);

                players = deserialized as Dictionary<string, Player>;
            }

            callback(players);
        }).Catch(err => {

            GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    public static void GetRecord(string mapId, string playerName, Record record, GetRecordCallback callback)
    {
        RestClient.Get<Record>($"{databaseURL}{recordsTable}/{mapId}/{playerName}.json").Then(record => { 
            callback(record);
        }).Catch(err => {

            PostRecord(record, mapId, playerName, () => {});
        });
    }

    public static void GetTopRecords(string mapId, int limit, GetTopRecordsCallback callback)
    {
        RestClient.Get($"{databaseURL}{recordsTable}/{mapId}.json?orderBy=\"time\"&limitToFirst={limit}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            var records = new Dictionary<string, Record>();

            if (!data.IsNull) {
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, Record>), ref deserialized);

                records = deserialized as Dictionary<string, Record>;
            }

            callback(records);
        }).Catch(err => {

            GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    public static void PatchMap(Map map, string mapId, PostMapCallback callback)
    {      
        RestClient.Patch<Map>($"{databaseURL}{mapsTable}/{mapId}.json", map).Then(response => { 
            callback(); 
        }).Catch(err => {
            Debug.Log(err);
        });
    }  

    public static void PatchPlayer(Player player, string mapId, int playerId, PostPlayerCallback callback)
    {
        string stringPlayerId = "player_" + playerId.ToString();
        RestClient.Patch<Player>($"{databaseURL}{playersTable}/{mapId}/{stringPlayerId}.json", player).Then(response => { 
            callback(); 
        }).Catch(err => {
            Debug.Log(err);
        });
    }    

    public static void PostRecord(Record record, string mapId, string playerName, PostRecordCallback callback)
    {
        RestClient.Put<Record>($"{databaseURL}{recordsTable}/{mapId}/{playerName}.json", record).Then(response => { 
            callback();
        }).Catch(err => {

            //GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    // Example of function usage in MonoBehaviour Class

    /*

    void QueryTests()
    {
        string mapId = _gameManager.maps[_mapIndex].mapId;
        string playerName = "julitus";
        var testRecord = new Record(1, 23.011f);
        
        DatabaseHandler.GetRecord(mapId, playerName, testRecord, record => {
            
            if (record.time > testRecord.time) {
                DatabaseHandler.PostRecord(testRecord, mapId, playerName, () => {

                });
            }
        });

        DatabaseHandler.GetMap(mapId, map => {

            var testMap = new Map(map.completed + 1);
            DatabaseHandler.PatchMap(testMap, mapId, () => {

            });
        });

        DatabaseHandler.GetMaps(maps => {
            Debug.Log("ok!!!!");
            Debug.Log(maps.Count);
            foreach (var map in maps)
            {
                Debug.Log($"{map.Key} {map.Value.completed}");
            }
        });

        DatabaseHandler.GetTopRecords(mapId, 1, records => {
            Debug.Log("ok!!!!");
            Debug.Log(records.Count);
            
            // var e = records.GetEnumerator();
            // e.MoveNext();
            // Debug.Log(e.Current.Key);

            foreach (var record in records)
            {
                Debug.Log($"{record.Key} {record.Value.playerId} {record.Value.time}");
            }
        });
    }
    
    */

    // Examples use function PatchPlayerAuth and PatchMapAuth

    public static void SignIn(SignInCallback callback)
    {
        var payLoad = $"{{\"email\":\"{authUser}\",\"password\":\"{authPassword}\",\"returnSecureToken\":true}}";
        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key={apiKey}",
            payLoad).Then(
            response =>
            {
                var responseJson = response.Text;

                // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
                // to serialize more complex types (a Dictionary, in this case)
                var data = fsJsonParser.Parse(responseJson);
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, string>), ref deserialized);

                var authResponse = deserialized as Dictionary<string, string>;
                idToken = authResponse["idToken"];

                callback();
            }).Catch(err => {
                Debug.Log(err);
            });
    }

    public static void PatchMapAuth(Map map, string mapId, PostMapCallback callback)
    {
        SignIn(() => {
                RestClient.Patch<Map>($"{databaseURL}{mapsTable}/{mapId}.json?auth={idToken}", map).Then(response => { 
                    callback(); 
                }).Catch(err => {
                    Debug.Log(err);
                });
            }
        );
    }

    public static void PatchPlayerAuth(Player player, string mapId, int playerId, PostPlayerCallback callback)
    {
        SignIn(() => {
                string stringPlayerId = "player_" + playerId.ToString();
                RestClient.Patch<Player>($"{databaseURL}{playersTable}/{mapId}/{stringPlayerId}.json?auth={idToken}", player).Then(response => { 
                    callback(); 
                }).Catch(err => {
                    Debug.Log(err);
                });
            }
        );
    }

    // Examples use AuthHandler.cs and Main.cs

    public static void PostUser(User user, string userId, PostUserCallback callback, string idToken)
    {
        RestClient.Put<User>($"{databaseURL}users/{userId}.json?auth={idToken}", user).Then(response => { callback(); });
    }

    public static void GetUser(string userId, GetUserCallback callback, string idToken)
    {
        RestClient.Get<User>($"{databaseURL}users/{userId}.json?auth={idToken}").Then(user => { callback(user); });
    }
}
