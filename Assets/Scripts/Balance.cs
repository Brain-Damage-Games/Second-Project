using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Balance 
{
    //private static bool i = false;
    public static void ChangeBalance(int amount)
    {
        PlayerPrefs.SetInt("Balance", PlayerPrefs.GetInt("Balance") + amount);
    }

    public static int GetBalance()
    {

        return PlayerPrefs.GetInt("Balance");
        /*if (i)
            return 0;
        else
        {

            Debug.Log("print");
            i = true;
            return 200;
        }*/
        //return 10; 
        
    }

    public static void SetBalance(int balance)
    {
        PlayerPrefs.SetInt("Balance", balance);

    }
    
}
