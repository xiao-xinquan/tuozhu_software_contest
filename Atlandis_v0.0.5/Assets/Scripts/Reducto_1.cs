using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using UnityEngine.Windows.Speech;
using UnityEngine.Events;
using System.Linq;


public class Reducto : MonoBehaviour
{
    #region Variables
    private Dictionary<string, UnityAction> keyValuePairs = new Dictionary<string, UnityAction>();
    private KeywordRecognizer m_Recognizer;
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.High;
    #endregion
    public GameObject bombEffect;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    private void Start()
    {
        keyValuePairs.Add("Reducto", bombsetactive);
        keyValuePairs.Add("粉碎", bombsetactive);
        m_Recognizer = new KeywordRecognizer(keyValuePairs.Keys.ToArray(), m_confidenceLevel);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        bombEffect.SetActive(false);
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);
        keyValuePairs[args.text].Invoke();
    }

    private void bombsetactive()
    {
        _spriteRenderer.enabled = false; 
        m_Recognizer.Stop();
        bombEffect.SetActive(true);
        Destroy(gameObject,0.2f);
        Destroy(bombEffect,0.5f);
    }

}
