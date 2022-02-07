using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectedPlayer : MonoBehaviour
{
    private int _playerIndex;
    [SerializeField] private Image _playerImage;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _playerIndex = PlayerPrefs.GetInt("playerIndex");

        if (_playerIndex >= _gameManager.players.Count) {
            _playerIndex = 0;
        }

        ChangePlayer();
    }

    private void ChangePlayer()
    {
        PlayerPrefs.SetInt("playerIndex", _playerIndex);
        _playerImage.sprite = _gameManager.players[_playerIndex].sprite;
    }

    public void NextPlayer()
    {
        _playerIndex = (_playerIndex + 1) % _gameManager.players.Count;
        
        ChangePlayer();
    }

    public void PreviousPlayer()
    {
        _playerIndex = (_playerIndex - 1 + _gameManager.players.Count) % _gameManager.players.Count;

        ChangePlayer();
    }

    /*public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

}
