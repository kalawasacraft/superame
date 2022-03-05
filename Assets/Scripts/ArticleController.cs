using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleController : MonoBehaviour
{
    public GameObject[] articles;
    public float waitTime = 11f;

    private GameObject _currentArticle;
    private float _currentTime;
    private AudioSource _audio;
    
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentArticle == null) {

            _currentTime += Time.deltaTime;
            if (_currentTime >= waitTime) {
                _audio.Play();

                _currentArticle = Instantiate(articles[GameManager.RandomNumber(0, articles.Length)], transform.position, Quaternion.identity) as GameObject;
                _currentTime = 0f;
            }
        }
    }
}
