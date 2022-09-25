using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Sprite[] slidersBackground;

    [SerializeField]
    private Color[] slidersFill;

    [SerializeField]
    private TextMeshProUGUI txt;

    private int level;

    [SerializeField]
    private Image sliderBackground;
    [SerializeField]
    private Image sliderFill;

    private int payedMoney;
    private int maxMoney;
    private void Awake()
    {
        level = 1;
        UpdateMaxMoney();
        payedMoney = 0;
        slider.maxValue = maxMoney;
        txt.text = (100).ToString() + "%";
    }
    
    public void Upgrade()
    {
        if (payedMoney >= maxMoney)
        {
            payedMoney -= maxMoney;
            level++;
            UpdateMaxMoney();
            if (level - 1 < slidersBackground.Length)
            {
                ChangeFace();
            }


         }
    }

    public void Downgrade()
    {
        if (payedMoney < 0)
        {
            payedMoney = 0;
            level--;
            UpdateMaxMoney();
            if (level - 1 >= 0)
            {
                ChangeFace();
            }
        }
    }
    private void ChangeFace()
    {
        slider.maxValue = maxMoney;
        sliderFill.color = slidersFill[level - 1]; ;
        sliderBackground.sprite= slidersBackground[level - 1];

    }
    private void UpdatePayedMoney(int money) 
    {
        payedMoney += money;
        if (payedMoney >= maxMoney)
        {
            Upgrade();
        }
        else if (payedMoney < 0)
        {
            Downgrade();
        }
        slider.value = payedMoney;
        txt.text = (payedMoney / maxMoney).ToString() + "%";
    }
    private void UpdateMaxMoney()
    {
        maxMoney = 100 * level;
    }
}
