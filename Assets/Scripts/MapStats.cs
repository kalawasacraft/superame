using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using TMPro;

public class MapStats : MonoBehaviour
{
    //public int numberTopByMap = 7;
    [SerializeField] private GameObject _mapButton;
    [SerializeField] private GameObject _mapStatsWindow;
    [SerializeField] private Image _imageMap;
    [SerializeField] private TMPro.TMP_Text _counterMap;
    public GameObject firstSelected;
    public List<GameObject> rowsTopPlayers;
    public List<GameObject> columnsCharacters;

    private Vector2 _sizeColumn = new Vector2(20f, 45f);
    
    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void ShowStats()
    {
        Invoke("CallShowStats", 0.2f);
    }

    public void CallShowStats()
    {
        _mapStatsWindow.SetActive(true);

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelected, new BaseEventData(eventSystem));

        LoadStats();
    }

    private void LoadStats()
    {
        GameManager.ShowWaitLoad(true);

        int mapIndex = PlayerPrefs.GetInt("mapIndex");
        string mapId = _gameManager.maps[mapIndex].mapId;

        _imageMap.sprite = _gameManager.maps[mapIndex].sprite;

        _counterMap.SetText("");

        foreach (GameObject row in rowsTopPlayers) {
            SetRowTopPlayer(row, "---", "-", -1);
        }

        foreach (GameObject column in columnsCharacters) {
            SetColumnCharacter(column, 0, 0, -1);
        }

        DatabaseHandler.GetMap(mapId, map => {

            _counterMap.SetText("~ " + map.completed.ToString() + " " +
                _stringTable.GetTable().GetEntry("langCounterMap").GetLocalizedString());
        });

        DatabaseHandler.GetTopRecords(mapId, rowsTopPlayers.Count, records => {
            
            List<List<string> > topPlayers = new List<List<string> > ();

            foreach (var record in records) {
                topPlayers.Add(new List<string>() {record.Value.time.ToString("0.00"), record.Key, record.Value.playerId.ToString()});
            }

            topPlayers.Sort(delegate (List<string> x, List<string> y) {
                float xValue = float.Parse(x[0]);
                float yValue = float.Parse(y[0]);

                if (xValue == yValue) {    
                    return (x[1].CompareTo(y[1]) < 0 ? -1 : 1);
                }
                return (xValue < yValue ? -1 : 1);
            });

            for (int i = 0; i < rowsTopPlayers.Count; i++) {
                if (i < records.Count) {
                    SetRowTopPlayer(rowsTopPlayers[i], topPlayers[i][1], topPlayers[i][0], int.Parse(topPlayers[i][2]));
                }
            }

            GameManager.ShowWaitLoad(false);
        });

        DatabaseHandler.GetPlayers(mapId, players => {
            
            List<List<string> > topCharacters = new List<List<string> > ();

            foreach (var player in players) {
                topCharacters.Add(new List<string>() {player.Value.completed.ToString(), player.Key});
            }

            topCharacters.Sort(delegate (List<string> x, List<string> y) {
                int xValue = int.Parse(x[0]);
                int yValue = int.Parse(y[0]);

                if (xValue == yValue) {    
                    return (x[1].CompareTo(y[1]) < 0 ? -1 : 1);
                }
                return (xValue > yValue ? -1 : 1);
            });

            for (int i = 0; i < players.Count; i++) {
                SetColumnCharacter(columnsCharacters[i], int.Parse(topCharacters[i][0]), int.Parse(topCharacters[0][0]), int.Parse(topCharacters[i][1].Split("_")[1]));
            }
        });
    }

    private void SetRowTopPlayer(GameObject row, string nickname, string score, int playerId)
    {
        row.GetComponentsInChildren<TMP_Text>()[0].SetText(nickname);
        row.GetComponentsInChildren<TMP_Text>()[1].SetText(score);

        Image tempImage = row.transform.Find("Player Face").GetComponent<Image>();

        if (playerId < 0) {   
            tempImage.color = new Color(1f, 1f, 1f, 0f);
            tempImage.sprite = null;
        } else {
            tempImage.color = new Color(1f, 1f, 1f, 1f);
            tempImage.sprite = _gameManager.players[playerId].face;
        }
    }

    private void SetColumnCharacter(GameObject column, int amount, int maxAmount, int playerId)
    {
        float heightColumn = 0f;
        if (maxAmount > 0) {
            heightColumn = (amount * _sizeColumn.y) / maxAmount;
        }

        column.transform.Find("Column Value").GetComponent<RectTransform>().sizeDelta = new Vector2(_sizeColumn.x, heightColumn);
        column.GetComponentsInChildren<TMP_Text>()[0].SetText(playerId < 0 ? "" : "~ " + amount.ToString());
        Image tempImage = column.transform.Find("Player Face").GetComponent<Image>();
        
        if (playerId < 0) {
            tempImage.color = new Color(1f, 1f, 1f, 0f);
            tempImage.sprite = null;

        } else {
            tempImage.color = new Color(1f, 1f, 1f, 1f);
            tempImage.sprite = _gameManager.players[playerId].face;
        }
        
    }

    public void Quit()
    {
        Invoke("QuitAction", 0.2f);
    }

    private void QuitAction()
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_mapButton, new BaseEventData(eventSystem));
        _mapStatsWindow.SetActive(false);
    }
}
