using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectedMap : MonoBehaviour
{
    private int _mapIndex;
    [SerializeField] private Button _mapButton;
    [SerializeField] private TMPro.TMP_Text _nameMap;
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

    public void ChangeMap()
    {
        PlayerPrefs.SetInt("mapIndex", _mapIndex);
        PlayerPrefs.Save();
        
        _mapButton.GetComponent<Image>().sprite = _gameManager.maps[_mapIndex].sprite;
        _nameMap.SetText(_gameManager.maps[_mapIndex].nameMap);
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
        SceneManager.LoadScene(_gameManager.maps[_mapIndex].positionBuildScene);
    }
}
