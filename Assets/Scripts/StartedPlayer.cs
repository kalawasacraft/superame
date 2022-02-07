using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartedPlayer : MonoBehaviour
{
    public GameObject positionPlayer;

    private CinemachineVirtualCamera _vCam;

    // Start is called before the first frame update
    void Start()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();

        int playerIndex = PlayerPrefs.GetInt("playerIndex");
        GameObject initPlayer = Instantiate(GameManager.Instance.players[playerIndex].player, positionPlayer.transform.position, Quaternion.identity);

        if (_vCam != null) {
            _vCam.Follow = initPlayer.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
