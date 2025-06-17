using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BpmTextValue : MonoBehaviour
{
    [SerializeField] Slider bpm;
    TMP_Text value;
    private void Start()
    {
        value = GetComponent<TMP_Text>();
    }
    public void UpdateText()
    {
        value.text = bpm.value.ToString();
    }
}
