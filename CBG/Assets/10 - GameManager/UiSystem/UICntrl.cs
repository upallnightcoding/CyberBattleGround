using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICntrl : MonoBehaviour
{
    public TMP_Text roundsText;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateNumberRounds(int numberRounds)
    {
        roundsText.text = $"{numberRounds}";
    }

    public void UpdateHealth(float value)
    {
        healthSlider.value = value;
    }

    private void OnEnable()
    {
        EventCntrl.Instance.OnUpdateNumberRounds += UpdateNumberRounds;
    }

    private void OnDisable()
    {
        EventCntrl.Instance.OnUpdateNumberRounds -= UpdateNumberRounds;
    }
}
