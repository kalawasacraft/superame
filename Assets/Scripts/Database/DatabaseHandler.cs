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

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostMapCallback();
    public delegate void PostRecordCallback();
    public delegate void PostPlayerCallback();
    public delegate void GetMapCallback(Map map);
    public delegate void GetRecordCallback(Record record);
    public delegate void GetPlayerCallback(Player player);
    public delegate void GetMapsCallback(Dictionary<string, Map> maps);
    public delegate void GetTopRecordsCallback(Dictionary<string, Record> records);
    public delegate void GetPlayersCallback(Dictionary<string, Player> players);

    /// <summary>
    /// Retrieves a record from the Firebase Database, given their id
    /// </summary>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetMap(string mapId, GetMapCallback callback)
    {
        RestClient.Get<Map>($"{databaseURL}{mapsTable}/{mapId}.json").Then(map => { 
            callback(map);
        }).Catch(err => {

            //GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    /// <summary>
    /// Gets all maps from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
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

    /// <summary>
    /// Retrieves a record from the Firebase Database, given their id
    /// </summary>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="playerId"> Id of the player that will be uploaded </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
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

    /// <summary>
    /// Gets all maps from the Firebase Database
    /// </summary>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
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

    /// <summary>
    /// Adds a map to the Firebase Database
    /// </summary>
    /// <param name="map"> Map object that will be uploaded </param>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    
    /// Create map only manually...!
    
    /*public static void PostMap(Map map, string mapId, PostMapCallback callback)
    {
        RestClient.Put<Map>($"{databaseURL}{mapsTable}/{mapId}.json", map).Then(response => { 
            Debug.Log("The map was successfully uploaded to the database");
            callback(); 
        });
    }*/

    /// <summary>
    /// Adds a map to the Firebase Database
    /// </summary>
    /// <param name="map"> Map object that will be uploaded </param>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    public static void PatchMap(Map map, string mapId, PostMapCallback callback)
    {      
        RestClient.Patch<Map>($"{databaseURL}{mapsTable}/{mapId}.json", map).Then(response => { 
            //Debug.Log("The map was successfully uploaded to the database");
            callback(); 
        });
    }

    /// <summary>
    /// Adds a map to the Firebase Database
    /// </summary>
    /// <param name="player"> Player object that will be uploaded </param>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="playerId"> Id of the player that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    public static void PatchPlayer(Player player, string mapId, int playerId, PostPlayerCallback callback)
    {
        string stringPlayerId = "player_" + playerId.ToString();
        RestClient.Patch<Player>($"{databaseURL}{playersTable}/{mapId}/{stringPlayerId}.json", player).Then(response => { 
            //Debug.Log("The map was successfully uploaded to the database");
            callback(); 
        });
    }

    /// <summary>
    /// Gets all maps from the Firebase Database
    /// </summary>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
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

    //public static void GetRecordByMapId

    /// <summary>
    /// Adds a record to the Firebase Database
    /// </summary>
    /// <param name="record"> Record object that will be uploaded </param>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="playerName"> Id of the player that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    public static void PostRecord(Record record, string mapId, string playerName, PostRecordCallback callback)
    {
        RestClient.Put<Record>($"{databaseURL}{recordsTable}/{mapId}/{playerName}.json", record).Then(response => { 
            //Debug.Log("The record was successfully uploaded to the database");
            callback();
        }).Catch(err => {

            //GameManager.ShowWaitLoad(false);
            Debug.Log(err);
        });
    }

    /// <summary>
    /// Retrieves a record from the Firebase Database, given their id
    /// </summary>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="playerName"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetRecord(string mapId, string playerName, Record record, GetRecordCallback callback)
    {
        RestClient.Get<Record>($"{databaseURL}{recordsTable}/{mapId}/{playerName}.json").Then(record => { 
            callback(record);
        }).Catch(err => {

            PostRecord(record, mapId, playerName, () => {});
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
}
