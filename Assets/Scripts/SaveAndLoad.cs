using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    int wallSize = 14, outPostSize = 4, towerSize = 4, base_Size = 2;

    //***** first column is health and the secound is level
    public int[,] wall = new int[14,2];
    public int[,] outPost = new int[4,2];
    public int[,] tower = new int[4,2];
    public int[] base_ = new int[2];

    public float money, playerHealth, time;
    

    // Start is called before the first frame update
    // void Start()
    // {
    //     Save();
    //     Load();
    // }

    private void Load()
    {
        TurnToIntArray(wall, PlayerPrefs.GetString("Wall"));
        TurnToIntArray(outPost, PlayerPrefs.GetString("OutPost"));
        TurnToIntArray(tower, PlayerPrefs.GetString("Tower"));
        turnOneToIntArray(base_, PlayerPrefs.GetString("Base"));

        money = PlayerPrefs.GetFloat("Money");
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth");
        time = PlayerPrefs.GetFloat("Time");

        Print_();

        print("Load");
    }

    public void Save()
    {

        ChangeWallAmount();

        PlayerPrefs.SetString("Wall", TurnToString(wall));
        PlayerPrefs.SetString("OutPost", TurnToString(outPost));
        PlayerPrefs.SetString("Tower", TurnToString(tower));
        PlayerPrefs.SetString("Base", TurnOneDToString(base_));
        PlayerPrefs.SetFloat("Money", money);
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.SetFloat("Time", time);

        print("Save");
    }

    private string TurnToString(int[,] array)
    {
        string answer = "";

        for(int i = 0; i < array.GetLength(0); i++)
            for(int j = 0; j < 2; j++)
            {
                answer += array[i,j] + " ";
            }

        return answer;
    }

    private string TurnOneDToString(int[] array)
    {
        string answer = "";

        for(int i = 0; i < 2; i++)
            answer += array[i] + " ";

        return answer;
    }
    
    private void TurnToIntArray(int[,] array, string savedString)
    {
        string[] subStrings = savedString.Split();
        int counter = 0;

        for(int i = 0; i < array.GetLength(0); i++)
            for(int j = 0; j < 2; j++)
            {
                int.TryParse(subStrings[counter], out array[i,j]);
                counter++;
            }
    }

    private void turnOneToIntArray(int[] array, string savedString)
    {
        string[] subStrings = savedString.Split();
        
        int.TryParse(subStrings[0], out array[0]);
        int.TryParse(subStrings[1], out array[1]);
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
            Save();
        

        if(Input.GetMouseButtonDown(1))
            Load();
    }

    public void Print_()
    {
        print("walls: ");

        for(int i = 0; i < wallSize; i++)
            for(int j = 0; j < 2; j++)
                print("wall " + i + " " + j + " : " + wall[i,j]);

    }

    private void ChangeWallAmount()
    {
        for(int i = 0; i < wallSize; i++)
            for(int j = 0; j < 2; j++)
                wall[i,j] ++;
    }
    
}
