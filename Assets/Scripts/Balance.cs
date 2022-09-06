using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Balance 
{
    public static void ChangeBalance(int amount)
    {
        PlayerPrefs.SetInt("Balance", PlayerPrefs.GetInt("Balance") + amount);
    }

    public static int GetBalance()
    {
        return PlayerPrefs.GetInt("Balance");
    }
    
}
