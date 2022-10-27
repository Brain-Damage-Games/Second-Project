using UnityEngine;
using System.Collections.Generic;
using System;

public class BaseUpgrade : MonoBehaviour
{

    [SerializeField]
    private GameObject[] upgradableObjects;

    [SerializeField]
    private GameObject saveAndLoad;

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
        saveAndLoad.GetComponent<SaveAndLoad>().BaseLoad();
        objectCounts = upgradableObjects.Length;
        CalculateMaxProgress();
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
        maxProgress = (level * (level + 1)) * objectCounts / 2;
        lastMaxProgress = ((level-1) * level) * objectCounts / 2;
    }
    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }
    public void SetProgress(int progress)
    {
        this.progress = progress;
    }

    public int GetProgress()
    {
        return progress;
    }
}
