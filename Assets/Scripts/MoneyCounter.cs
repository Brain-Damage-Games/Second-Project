using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MoneyCounter : MonoBehaviour
{
    TextMesh textMesh ; 
    private int moneyAmount = 0  ;
    public void Counter()
    {
       moneyAmount ++;
    }
    void Start()
    {
        textMesh = GetComponent<TextMesh>(); 
    }

    void Update()
    {
        textMesh.text = moneyAmount.ToString(); 
    }
}
