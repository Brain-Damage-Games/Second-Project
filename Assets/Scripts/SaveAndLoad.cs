using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    int wallSize = 14, outPostSize = 4, towerSize = 4, base_Size = 1;

    [SerializeField]
    private GameObject[] walls = new GameObject[14];
    [SerializeField]
    private GameObject[] outPosts = new GameObject[4];
    [SerializeField]
    private GameObject[] towers = new GameObject[4];
    [SerializeField]
    private GameObject theBase;
    [SerializeField]
    private GameObject timeGO, playerHealthGO;            


    //***** first column is health and the secound is level
    private int[,] wall = new int[14,2];
    private int[,] outPost = new int[4,2];
    private int[,] tower = new int[4,2];
    private int[] base_ = new int[2];

    public float playerHealth, time;
    

    // Start is called before the first frame update
    // void Start()
    // {
    //     Save();
    //     Load();
    // }

    private void WallLoad()
    {
        TurnToIntArray(wall, PlayerPrefs.GetString("Wall"));
        StoreInGameObjectForWalls(); 
        //this method is for test.
        // Print_();

        // print("Load");
    }

    // public void MoneyLoad()
    // {
    //     money = PlayerPrefs.GetFloat("Money");                                         //***** not compelet
    //     //moneyGO.GetComponent<Balance>().
    // }

    public void PlayerHealthLoad()
    {
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth");
        playerHealthGO.GetComponent<Damageable>().SetHealth(playerHealth);
    }

    public void TimeLoad()
    {
        time = PlayerPrefs.GetFloat("Time");
        timeGO.GetComponent<DayNightCycle>().SetCurrentTime(time);
    }

    public void OutPostLoad()
    {
        TurnToIntArray(outPost, PlayerPrefs.GetString("OutPost"));
        StoreInGameObjectForOutposts();
    }

    public void TowerLoad()
    {
        TurnToIntArray(tower, PlayerPrefs.GetString("Tower"));
        StoreInGameObjectFortowers();
    }

    public void BaseLoad()
    {
        turnOneToIntArray(base_, PlayerPrefs.GetString("Base"));
        StoreInGameObjectForbase();
    }



    public void WallSave()
    {
        StoreInArrayForWalls();
        PlayerPrefs.SetString("Wall", TurnToString(wall));
    }

    // public void MoneySave()
    // {
    //     //StoreInVariableFromGameObject                                               //***** not complete.....
    //     PlayerPrefs.SetFloat("Money", money);
    // }

    public void PlayerHealthSave()
    {
        playerHealth = playerHealthGO.GetComponent<Damageable>().GetHealth();
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
    }

    public void TimeSave()
    {
        time = timeGO.GetComponent<DayNightCycle>().GetCurrentTime();
        PlayerPrefs.SetFloat("Time", time);
    }

    public void OutPostSave()
    {
        StoreInArrayForOutPosts();
        PlayerPrefs.SetString("OutPost", TurnToString(outPost));
    }
    
    public void TowerSave()
    {
        StoreInArrayForTowers();
        PlayerPrefs.SetString("Tower", TurnToString(tower));
    }

    public void BaseSave()
    {
        StoreInArrayForBase();
        PlayerPrefs.SetString("Base", TurnOneDToString(base_));
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
        // if(Input.GetMouseButtonDown(0))
        //     Save();
        

        // if(Input.GetMouseButtonDown(1))
        //     Load();
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

    //***** this methods will get the informathion from the gameObjects and store them in related arrays.
    private void StoreInArrayForWalls()
    {
        //***** walls:
        for(int i = 0; i < wallSize; i++)
        {
            wall[i,0] = walls[i].GetComponent<Damageable>().GetExactHealth();
            wall[i,1] = walls[i].GetComponent<Upgradable>().GetLevel();
        }        
    }
    private void StoreInArrayForOutPosts()
    {
        //***** outPosts:
        for(int i = 0; i < outPostSize; i++)
        {
            outPost[i,0] = outPosts[i].GetComponent<Damageable>().GetExactHealth();
            outPost[i,1] = outPosts[i].GetComponent<OutpostUpgrade>().GetLevel();
        }
    }
    private void StoreInArrayForTowers()
    {
        //***** towers:
        for(int i = 0; i < towerSize; i++)
        {
            tower[i,0] = towers[i].GetComponent<Damageable>().GetExactHealth();
            tower[i,1] = towers[i].GetComponent<Upgradable>().GetLevel();
        }
    }
    private void StoreInArrayForBase()
    {
        //***** base:
        base_[0] = theBase.GetComponent<Damageable>().GetExactHealth();
        base_[1] = theBase.GetComponent<BaseUpgrade>().GetLevel();
    }


    //***** this methods will store arrays informathion in related gameObjects.
    private void StoreInGameObjectForWalls()
    {
        //***** walls:
        for(int i = 0; i < wallSize; i++)
        {
            walls[i].GetComponent<Damageable>().SetHealth(wall[i,0]);
            walls[i].GetComponent<Upgradable>().SetLevel(wall[i,1]);
        }
    }
    private void StoreInGameObjectForOutposts()
    {
        //***** outPosts:
        for(int i = 0; i < outPostSize; i++)
        {
            outPosts[i].GetComponent<Damageable>().SetHealth(outPost[i,0]);
            outPosts[i].GetComponent<OutpostUpgrade>().SetLevel(outPost[i,1]);
        }
    }
    private void StoreInGameObjectFortowers()
    {
        //***** towers:
        for(int i = 0; i < towerSize; i++)
        {
            towers[i].GetComponent<Damageable>().SetHealth(outPost[i,0]);
            towers[i].GetComponent<OutpostUpgrade>().SetLevel(outPost[i,1]);
        }
    }
    private void StoreInGameObjectForbase()
    {
        //***** base:
        theBase.GetComponent<Damageable>().SetHealth(base_[0]);
        theBase.GetComponent<BaseUpgrade>().SetLevel(base_[1]);
    }


    // public void SetMoney(float money)
    // {
    //     this.money = money;
    // }

    public void SetPlayerHealth(float playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    public void SetTime(float time)
    {
        this.time = time;
    }
    
}
