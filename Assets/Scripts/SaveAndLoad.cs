using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    int wallSize = 14, outPostSize = 4, towerSize = 4, upgradableSize = 18, base_Size = 1;

    //[SerializeField]
    //private GameObject[] walls = new GameObject[14];
    [SerializeField]
    private GameObject[] outPosts = new GameObject[4];
    //[SerializeField]
    //private GameObject[] towers = new GameObject[4];

    [SerializeField]
    private GameObject[] upgradables = new GameObject[18];
    [SerializeField]
    private GameObject theBase;
    [SerializeField]
    private GameObject timeGO, playerHealthGO;


    //***** first column is health and the secound is level
    //public int[,] wall = new int[14,3];
    public int[,] outPost = new int[4,2];
    //public int[,] tower = new int[4,3];
    private int[,] upgradable = new int[18, 3];
    //third one is progress
     public int[] base_ = new int[3];

    public float playerHealth, time;
    

    // Start is called before the first frame update
    // void Start()
    // {
    //     Save();
    //     Load();
    // }

    /*private void WallLoad()
    {
        TurnToIntArray(wall, PlayerPrefs.GetString("Wall"));
        StoreInGameObjectForWalls(); 
        //this method is for test.
        // Print_();

        // print("Load");
    }*/

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
        if (PlayerPrefs.HasKey("OutPost"))
        {
            TurnToIntArray(outPost, PlayerPrefs.GetString("OutPost"));
            StoreInGameObjectForOutposts();
        }
        else
        {
            for (int i = 0; i < outPostSize; i++)
            {
                outPosts[i].GetComponent<Damageable>().SetHealth(outPosts[i].GetComponent<Damageable>().GetMaxHealth());
                outPosts[i].GetComponent<OutpostUpgrade>().SetLevel(1);
            }
        }
    }

    /*public void TowerLoad()
    {
        TurnToIntArray(tower, PlayerPrefs.GetString("Tower"));
        StoreInGameObjectFortowers();
    }*/

    public void UpgradableLoad()
    {
        if (PlayerPrefs.HasKey("upgradable"))
        {
            TurnToIntArray(upgradable, PlayerPrefs.GetString("upgradable"));
            StoreInGameObjectForUpgradables();
        }
        else
        {
            for (int i = 0; i < upgradableSize; i++)
            {
                upgradables[i].GetComponent<Damageable>().SetHealth(GetComponent<Damageable>().GetMaxHealth());
                upgradables[i].GetComponent<Upgradable>().SetLevel(1);
                upgradables[i].GetComponent<Upgradable>().SetPayedMoney(0);
            }
        }
    }

    public void BaseLoad()
    {
        if (PlayerPrefs.HasKey("Base"))
        {
            turnOneToIntArray(base_, PlayerPrefs.GetString("Base"));
            StoreInGameObjectForbase();
        }
        else
        {
            theBase.GetComponent<Damageable>().SetHealth(theBase.GetComponent<Damageable>().GetMaxHealth());
            theBase.GetComponent<BaseUpgrade>().SetLevel(1);
            theBase.GetComponent<BaseUpgrade>().SetProgress(0);
        }
    }



    /*public void WallSave()
    {
        StoreInArrayForWalls();
        PlayerPrefs.SetString("Wall", TurnToString(wall));
    }*/

    public void UpgradableSave()
    {
        StoreInArrayForUpgradable();
        PlayerPrefs.SetString("upgradable", TurnToString(upgradable));
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
    
    /*public void TowerSave()
    {
        StoreInArrayForTowers();
        PlayerPrefs.SetString("Tower", TurnToString(tower));
    }*/

    public void BaseSave()
    {
        StoreInArrayForBase();
        PlayerPrefs.SetString("Base", TurnOneDToString(base_));
    }

    private string TurnToString(int[,] array)
    {
        string answer = "";

        for(int i = 0; i < array.GetLength(0); i++)
            for(int j = 0; j < 3; j++)
            {
                answer += array[i,j] + " ";
            }

        return answer;
    }

    private string TurnOneDToString(int[] array)
    {
        string answer = "";

        for(int i = 0; i < 3; i++)
            answer += array[i] + " ";

        return answer;
    }
    
    private void TurnToIntArray(int[,] array, string savedString)
    {
        string[] subStrings = savedString.Split();
        int counter = 0;

        for(int i = 0; i < array.GetLength(0); i++)
            for(int j = 0; j < 3; j++)
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
        int.TryParse(subStrings[2], out array[2]);
    }

    private void Update() 
    {
        // if(Input.GetMouseButtonDown(0))
        //     Save();
        

        // if(Input.GetMouseButtonDown(1))
        //     Load();
    }

    /*public void Print_()
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
    }*/

    //***** this methods will get the informathion from the gameObjects and store them in related arrays.
    /*private void StoreInArrayForWalls()
    {
        //***** walls:
        for(int i = 0; i < wallSize; i++)
        {
            wall[i,0] = walls[i].GetComponent<Damageable>().GetExactHealth();
            wall[i,1] = walls[i].GetComponent<Upgradable>().GetLevel();
        }        
    }*/
    private void StoreInArrayForOutPosts()
    {
        //***** outPosts:
        for(int i = 0; i < outPostSize; i++)
        {
            outPost[i,0] = outPosts[i].GetComponent<Damageable>().GetExactHealth();
            outPost[i,1] = outPosts[i].GetComponent<OutpostUpgrade>().GetLevel();
        }
    }
    /*private void StoreInArrayForTowers()
    {
        //***** towers:
        for(int i = 0; i < towerSize; i++)
        {
            tower[i,0] = towers[i].GetComponent<Damageable>().GetExactHealth();
            tower[i,1] = towers[i].GetComponent<Upgradable>().GetLevel();
        }
    }*/
    
    private void StoreInArrayForUpgradable()
    {
        for (int i = 0; i < upgradableSize; i++)
        {
            upgradable[i, 0] = upgradables[i].GetComponent<Damageable>().GetExactHealth();
            upgradable[i, 1] = upgradables[i].GetComponent<Upgradable>().GetLevel();
            upgradable[i, 2] = upgradables[i].GetComponent<Upgradable>().GetPayedMoney();
        }
    }
    private void StoreInArrayForBase()
    {
        //***** base:
        base_[0] = theBase.GetComponent<Damageable>().GetExactHealth();
        base_[1] = theBase.GetComponent<BaseUpgrade>().GetLevel();
        base_[2] = theBase.GetComponent<BaseUpgrade>().GetProgress();
    }


    //***** this methods will store arrays informathion in related gameObjects.
    /*private void StoreInGameObjectForWalls()
    {
        //***** walls:
        for(int i = 0; i < wallSize; i++)
        {
            walls[i].GetComponent<Damageable>().SetHealth(wall[i,0]);
            walls[i].GetComponent<Upgradable>().SetLevel(wall[i,1]);
        }
    }*/
    private void StoreInGameObjectForOutposts()
    {
        //***** outPosts:
        for(int i = 0; i < outPostSize; i++)
        {
            outPosts[i].GetComponent<Damageable>().SetHealth(outPost[i,0]);
            outPosts[i].GetComponent<OutpostUpgrade>().SetLevel(outPost[i,1]);
        }
    }
    /* private void StoreInGameObjectFortowers()
     {
         //***** towers:
         for(int i = 0; i < towerSize; i++)
         {
             towers[i].GetComponent<Damageable>().SetHealth(outPost[i,0]);
             towers[i].GetComponent<Upgradable>().SetLevel(outPost[i,1]);
         }
     }*/

    private void StoreInGameObjectForUpgradables()
    {
        for (int i = 0; i < upgradableSize; i++)
        {
            upgradables[i].GetComponent<Damageable>().SetHealth(upgradable[i, 0]);
            upgradables[i].GetComponent<Upgradable>().SetLevel(upgradable[i, 1]);
            upgradables[i].GetComponent<Upgradable>().SetPayedMoney(upgradable[i, 2]);
        }
    }
    private void StoreInGameObjectForbase()
    {
        //***** base:
        theBase.GetComponent<Damageable>().SetHealth(base_[0]);
        theBase.GetComponent<BaseUpgrade>().SetLevel(base_[1]);
        theBase.GetComponent<BaseUpgrade>().SetProgress(base_[2]);
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
