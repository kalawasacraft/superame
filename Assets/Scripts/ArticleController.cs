using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class ArticleController : MonoBehaviour
{
    public GameObject[] articles;
    public float waitTime = 11f;

    private GameObject _currentArticle;
    private float _currentTime;
    private AudioSource _audio;
    private bool _isFirstArticle;
    private LocalizedStringTable _stringTable = new LocalizedStringTable { TableReference = "LanguageText" };
    
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _isFirstArticle = true;
        _currentTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentArticle == null) {

            _currentTime += Time.deltaTime;
            if (_currentTime >= waitTime) {

                int numberArticle = GameManager.RandomNumber(0, articles.Length);
                string messageAlert = _stringTable.GetTable().GetEntry("langMessageObjectAppeared").GetLocalizedString();
                
                switch (numberArticle)
                {
                    case 0:
                        messageAlert += " (" + _stringTable.GetTable().GetEntry("langHelpMsgAttack").GetLocalizedString() + ")";
                        break;
                    case 1:
                        messageAlert += " (" + _stringTable.GetTable().GetEntry("langHelpMsgRecover").GetLocalizedString() + ")";
                        break;
                    case 2:
                        messageAlert += " (" + _stringTable.GetTable().GetEntry("langHelpMsgShield").GetLocalizedString() + ")";
                        break;
                    default: break;
                }

                if (!_isFirstArticle) {
                    UIManager.ShowAlertInNotification(messageAlert);
                }

                _audio.Play();

                _currentArticle = Instantiate(articles[numberArticle], transform.position, Quaternion.identity) as GameObject;
                _currentTime = 0f;
                _isFirstArticle = false;
            }
        }
    }
}
