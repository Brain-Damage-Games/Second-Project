 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private int value;

    public void SetValue(int value)
    {
        this.value = value;
    }
    public int GetValue()
    {
        return value;
    }
}
