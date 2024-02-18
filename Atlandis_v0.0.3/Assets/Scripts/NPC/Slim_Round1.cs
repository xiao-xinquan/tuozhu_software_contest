using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slim_Round1 : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] npcText;

    private bool npcTrigger;
    private int currentDialogueIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (npcTrigger && Input.GetKeyDown(KeyCode.Space)) // Check if the player presses the space key
        {
            if (currentDialogueIndex < npcText.Length)
            {
                dialogueText.text = npcText[currentDialogueIndex];
                currentDialogueIndex++;
            }
            else
            {
                dialogueBox.SetActive(false);
                npcTrigger = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the NPC's trigger");
            dialogueBox.SetActive(true);
            dialogueText.text = npcText[currentDialogueIndex];
            npcTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            npcTrigger = false;
        }
    }
}
