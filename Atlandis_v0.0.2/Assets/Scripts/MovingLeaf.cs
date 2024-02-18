using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using UnityEngine.Windows.Speech;
using UnityEngine.Events;
using System.Linq;

public class MovingLeaf : MonoBehaviour
{
    #region Variables
    private Dictionary<string, UnityAction> keyValuePairs = new Dictionary<string, UnityAction>();
    private KeywordRecognizer m_Recognizer;
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.High;
    #endregion
    private void Start()
    {
        keyValuePairs.Add("Freeze", freezeEffect);
        keyValuePairs.Add("冻结", freezeEffect);
        m_Recognizer = new KeywordRecognizer(keyValuePairs.Keys.ToArray(), m_confidenceLevel);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 1f;
    private int pointIndex = 1;  //点的取值
    private float waitTime = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[pointIndex].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (pointIndex == 0)
                {
                    pointIndex = 1;
                }
                else
                {
                    pointIndex = 0;
                }
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);
        keyValuePairs[args.text].Invoke();
    }

    public void freezeEffect()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        m_Recognizer.Stop();
        Destroy(gameObject,2f);
    }
}