using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICntrl : MonoBehaviour
{
    [SerializeField] private TMP_Text roundsText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider coolDownSlider;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(75);
    }

    public void UpdateNumberRounds(int numberRounds)
    {
        roundsText.text = $"{numberRounds}";
    }

    public void UpdateHealth(int value)
    {
        healthSlider.value = value;
    }

    public void UpdateCoolDown(float value)
    {
        coolDownSlider.value = value;
    }

    private void OnEnable()
    {
        EventCntrl.Instance.OnUpdateNumberRounds += UpdateNumberRounds;
        EventCntrl.Instance.OnUpdateCoolDown += UpdateCoolDown;
    }

    private void OnDisable()
    {
        EventCntrl.Instance.OnUpdateNumberRounds -= UpdateNumberRounds;
        EventCntrl.Instance.OnUpdateCoolDown -= UpdateCoolDown;
    }
}
