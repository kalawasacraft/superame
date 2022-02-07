using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectedMap : MonoBehaviour
{
    private int _mapIndex;
    [SerializeField] private Image _mapImage;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        
        if (_mapIndex >= _gameManager.maps.Count) {
            _mapIndex = 0;
        }

        ChangeMap();
    }

    private void ChangeMap()
    {
        _mapImage.sprite = _gameManager.maps[_mapIndex].sprite;
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
        SceneManager.LoadScene(_gameManager.maps[_mapIndex].positionBuildScene);
    }
}