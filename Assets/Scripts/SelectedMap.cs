using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectedMap : MonoBehaviour
{
    private int _mapIndex;
    [SerializeField] private Button _mapButton;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _mapIndex = PlayerPrefs.GetInt("mapIndex");
        
        if (_mapIndex >= _gameManager.maps.Count) {
            _mapIndex = 0;
        }

        ChangeMap();
    }

    private void ChangeMap()
    {
        PlayerPrefs.SetInt("mapIndex", _mapIndex);
        PlayerPrefs.Save();
        _mapButton.GetComponent<Image>().sprite = _gameManager.maps[_mapIndex].sprite;
    }

    public void NextMap()
    {
        _mapIndex = (_mapIndex + 1) % _gameManager.maps.Count;
        
        ChangeMap();
    }

    public void PreviousMap()
    {
        _mapIndex = (_mapIndex - 1 + _gameManager.maps.Count) % _gameManager.maps.Count;

        ChangeMap();
    }

    public void StartGame()
    {
        Invoke("InitMap", 0.2f);
    }

    private void InitMap()
    {
        string mapId = _gameManager.maps[_mapIndex].mapId;
        /*string playerName = "julitus";
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
        });*/

        /*DatabaseHandler.GetMaps(maps => {
            Debug.Log("ok!!!!");
            Debug.Log(maps.Count);
            foreach (var map in maps)
            {
                Debug.Log($"{map.Key} {map.Value.completed}");
            }
        });*/

        /*DatabaseHandler.GetTopRecords(mapId, 1, records => {
            Debug.Log("ok!!!!");
            Debug.Log(records.Count);
            
            // var e = records.GetEnumerator();
            // e.MoveNext();
            // Debug.Log(e.Current.Key);

            foreach (var record in records)
            {
                Debug.Log($"{record.Key} {record.Value.playerId} {record.Value.time}");
            }
        });*/

        SceneManager.LoadScene(_gameManager.maps[_mapIndex].positionBuildScene);
    }
}
