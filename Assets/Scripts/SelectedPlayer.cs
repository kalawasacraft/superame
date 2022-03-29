using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectedPlayer : MonoBehaviour
{
    private int _playerIndex;
    [SerializeField] private GameObject _character;
    private GameManager _gameManager;
    private Animator _animatorCharacter;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _animatorCharacter = _character.GetComponent<Animator>();

        _playerIndex = PlayerPrefs.GetInt("playerIndex");

        if (_playerIndex >= _gameManager.players.Count) {
            _playerIndex = 0;
        }

        ChangePlayer();
    }

    public void ChangePlayer()
    {
        PlayerPrefs.SetInt("playerIndex", _playerIndex);
        PlayerPrefs.Save();
        Debug.Log(_playerIndex.ToString() + "-" + _gameManager.players[_playerIndex].animationIndex.ToString());
        _animatorCharacter.Play("Player"+_gameManager.players[_playerIndex].animationIndex.ToString());
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
}
