using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
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

        if (_vCam != null) {
            _vCam.Follow = _initPlayer.transform;
        }

        GameManager.RestartGame();
        GameManager.InitGame();

        StartCoroutine(CountdownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CountdownToStart()
    {
        int seconds = secondsCountdown;
        
        UIManager.SetActiveCountdownUI(true);

        while (seconds > 0) {

            StatesSoundController.CounterPlay();
            UIManager.UpdateCountdownUI(seconds.ToString());
            yield return new WaitForSeconds(1f);

            seconds--;
        }

        //SoundsManager.CounterPlay();
        StatesSoundController.StartPlay();
        UIManager.UpdateCountdownUI(_stringTable.GetTable().GetEntry("langGo").GetLocalizedString());
        GameManager.StartGame();
        
        yield return new WaitForSeconds(0.5f);        

        UIManager.SetActiveCountdownUI(false);
        UIManager.EnabledPause(true);
    }
}
