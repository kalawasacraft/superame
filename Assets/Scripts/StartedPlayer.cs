using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartedPlayer : MonoBehaviour
{
    public GameObject positionPlayer;

    private CinemachineVirtualCamera _vCam;
    private GameObject _initPlayer;

    void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("playerIndex");
        _initPlayer = Instantiate(GameManager.Instance.players[playerIndex].player, positionPlayer.transform.position, Quaternion.identity);

        if (_vCam != null) {
            _vCam.Follow = _initPlayer.transform;
        }

        GameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
