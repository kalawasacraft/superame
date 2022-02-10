using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using Cinemachine;

public class StartedPlayer : MonoBehaviour
{
    public GameObject positionPlayer;
    public int secondsCountdown = 3;

    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };
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
        _initPlayer.GetComponent<PlayerController>().setEnablePlayer(false);

        if (_vCam != null) {
            _vCam.Follow = _initPlayer.transform;
        }

        GameManager.InitGame();
        StartCoroutine(CountdownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CountdownToStart()
    {
        while (secondsCountdown > 0) {

            UIManager.UpdateCountdownUI(secondsCountdown.ToString());
            yield return new WaitForSeconds(1f);

            secondsCountdown--;
        }

        UIManager.UpdateCountdownUI(_stringTable.GetTable().GetEntry("langGo").GetLocalizedString());

        yield return new WaitForSeconds(1f);

        GameManager.StartGame();

        _initPlayer.GetComponent<PlayerController>().setEnablePlayer(true);

        UIManager.SetActiveCountdownUI(false);
    }
}
