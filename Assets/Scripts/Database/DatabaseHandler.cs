using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using Proyecto26;

public static class DatabaseHandler
{
    private const string projectId = "superame-c867e-default-rtdb";
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostMapCallback();
    public delegate void PostRecordCallback();
    public delegate void GetMapCallback(Map map);
    public delegate void GetRecordCallback(Record record);
    public delegate void GetMapsCallback(Dictionary<string, Map> maps);
    public delegate void GetTopRecordsCallback(Dictionary<string, Record> records);

    /// <summary>
    /// Retrieves a record from the Firebase Database, given their id
    /// </summary>
    /// <param name="mapId"> Id of the map that will be uploaded </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetMap(string mapId, GetMapCallback callback)
    {
        RestClient.Get<Map>($"{databaseURL}maps/{mapId}.json").Then(map => { 
            callback(map);
        });
    }

    /// <summary>
    /// Gets all maps from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetMaps(GetMapsCallback callback)
    {
        RestClient.Get($"{databaseURL}maps.json").Then(response =>
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
    /// Gets all maps from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetTopRecords(string mapId, int limit, GetTopRecordsCallback callback)
    {
        RestClient.Get($"{databaseURL}records/{mapId}.json?orderBy=\"time\"&limitToFirst={limit}").Then(response =>
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
        RestClient.Put<Map>($"{databaseURL}maps/{mapId}.json", map).Then(response => { 
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
        RestClient.Patch<Map>($"{databaseURL}maps/{mapId}.json", map).Then(response => { 
            Debug.Log("The map was successfully uploaded to the database");
            callback(); 
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
        //RestClient.Put<Record>($"{databaseURL}maps/{mapId}/records/{playerName}.json", record).Then(response => { 
        RestClient.Put<Record>($"{databaseURL}records/{mapId}/{playerName}.json", record).Then(response => { 
            Debug.Log("The record was successfully uploaded to the database");
            callback(); 
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
        //RestClient.Get<Record>($"{databaseURL}maps/{mapId}/records/{playerName}.json").Then(record => { 
        RestClient.Get<Record>($"{databaseURL}records/{mapId}/{playerName}.json").Then(record => { 
            callback(record);
        }).Catch(err => {

            PostRecord(record, mapId, playerName, () => {});
        });
    }
}
