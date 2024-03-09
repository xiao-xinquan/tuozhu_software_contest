using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using UnityEngine.Windows.Speech;
using UnityEngine.Events;
using System.Linq;


public class Time_Slowdown : MonoBehaviour
{
    public float timeScaleTargetValue = 0.3f;
    public float timeScaleOriginalValue = 1.0f;
    public float slowdownSpeedFactor = 2f;
    public bool isSlowingDown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        keyValuePairs.Add("解除", bombsetactive);
        m_Recognizer = new KeywordRecognizer(keyValuePairs.Keys.ToArray(), m_confidenceLevel);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        bombEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSlowingDown)
        {
            Time.timeScale -= slowdownSpeedFactor * Time.deltaTime;
            if (Time.timeScale <= timeScaleTargetValue)
            {
                Time.timeScale = timeScaleTargetValue;
                return;
            }
        }
        else
        {
            Time.timeScale += slowdownSpeedFactor * Time.deltaTime;
            if (Time.timeScale >= timeScaleOriginalValue)
            {
                Time.timeScale = timeScaleOriginalValue;
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            isSlowingDown = true;
    }

    #region Variables
    private Dictionary<string, UnityAction> keyValuePairs = new Dictionary<string, UnityAction>();
    private KeywordRecognizer m_Recognizer;
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.High;
    #endregion
    public GameObject bombEffect;
    private SpriteRenderer _spriteRenderer;
    

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);
        keyValuePairs[args.text].Invoke();
    }

    public void bombsetactive()
    {
        _spriteRenderer.enabled = false; 
        m_Recognizer.Stop();
        bombEffect.SetActive(true);
        isSlowingDown = false;
        Destroy(gameObject,0.2f);
        Destroy(bombEffect,0.5f);
    }    
}
