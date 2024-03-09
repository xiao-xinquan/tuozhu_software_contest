using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Count : MonoBehaviour
{
    private int crystals = 0;
    [SerializeField] private int maxCrystals = 3;
    [SerializeField] private TextMeshProUGUI crystalText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Crystal")
        {
            crystals++;
            crystalText.text = "Crystals: " + crystals + "/" + maxCrystals;
            Debug.Log("Crystals: " + crystals);
        }
    }
}
