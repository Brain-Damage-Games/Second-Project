using UnityEngine;
using System.Collections.Generic;
using System;

public class BaseUpgrade : MonoBehaviour
{

    [SerializeField]
    private GameObject[] upgradableObjects;

    private bool doAction = false;


    // comment the following SeralizedField after testing the class
    //[SerializeField]
    private int level;
    public int progress;
    public int maxProgress;
    private int lastMaxProgress;
    private int objectCounts;
    
    private void Awake()
    {
        level = 1;
        progress = 0;
        objectCounts = upgradableObjects.Length;
        maxProgress = objectCounts;
        lastMaxProgress = 0;
    }


    private void Update()
    {
        if (doAction)
        {
            Upgrade();
            doAction = false;
        }
    }
    
    private void Upgrade() 
    {
        level++;
        gameObject.GetComponent<Upgradable>().Upgrade();
        CalculateMaxProgress();
        progress = lastMaxProgress;
    }
    private void CheckForUpgrade()
    {
        if(progress == maxProgress)
        {
            doAction = true;
        }
    }
    public bool CheckIndividualUpgrade(int level,bool addProgress)
    {
        bool doAct = false;

        if ((level < this.level) || (level == this.level && progress >= lastMaxProgress))
        {
            doAct = true;
            if (addProgress) 
            {
                ChangeProgress(level);
                CheckForUpgrade();
            }
        }    

        return doAct;
    }
    public void ChangeProgress(int level) 
    {
        progress += level;
    }
    private void CalculateMaxProgress()
    {
        lastMaxProgress = maxProgress;
        maxProgress = (level * (level + 1)) * objectCounts / 2;
    }
    public int GetLevel()
    {
        return level;
    }
}
