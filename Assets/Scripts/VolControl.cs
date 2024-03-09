using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

using System;
using System.Text;
using UnityEngine.Windows.Speech;
using UnityEngine.Events;
using System.Linq;

public class VolControl : MonoBehaviour
{
    Rigidbody2D rg;
    private Animator anim;
    private float dirX = 1f;
    float tempTime = 0;

    public float volume;
    public float movevalue = 0.05f;
    public float jumpvalue = 0.2f;

    public float jumpForce = 800;
    public float maxSpeed = 5;
    public float minRunSpeed = 0.5f;
    public float minJumpSpeed = 0.5f;
    public bool isGrounded;  
    public Transform groundCheck;  //检测是否在地面
    public LayerMask ground;  //地面层

    //新添加“向后转”语音指令
    #region Variables
    private Dictionary<string, UnityAction> keyValuePairs = new Dictionary<string, UnityAction>();
    private KeywordRecognizer m_Recognizer;
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.High;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dirX = 1f;
        
        //语音识别模块
        keyValuePairs.Add("向后转", turnaround);
        m_Recognizer = new KeywordRecognizer(keyValuePairs.Keys.ToArray(), m_confidenceLevel);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        volume = Mic_Input.volume;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        movevalue = dataController.speakVolume * 0.8f;
        jumpvalue = dataController.speakVolume * 1.2f;


        //原本此处的dirX在turnaround函数中被赋值，所以这里不需要再赋值

        if (volume > movevalue) {
            MoveForward();
            if (rg.velocity.x > maxSpeed)
            {
                rg.velocity = new Vector2(dirX * maxSpeed, rg.velocity.y);
            }
        }
        if (volume > jumpvalue && isGrounded)
        {
            if (Time.time - tempTime > 1.5f)  //防止飞出画面，缩短跳跃持续时间，第2关中可以跳到上方平台
            {
                Jump();
                tempTime = Time.time;
            }
        }
        UpdateAnimationState();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);
        keyValuePairs[args.text].Invoke();
    }

    private void turnaround()
    {
        if (dirX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        dirX = -1*dirX;
        
    }

    void Jump()
    {
        rg.AddForce(dirX * Vector2.up * jumpForce * volume);
    }

    void MoveForward()
    {
        rg.AddForce(dirX * Vector2.right * 5);
    }

    private void UpdateAnimationState()
    {
        if (Mathf.Abs(rg.velocity.x) > minRunSpeed)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

        if (Mathf.Abs(rg.velocity.y) > minJumpSpeed)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("running", false);
        }
        else
        {
            anim.SetBool("jumping", false);
        }
    }
}
