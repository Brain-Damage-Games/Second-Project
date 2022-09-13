using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerUpgrade : MonoBehaviour
{

    [SerializeField]
    private GameObject[] upgradableObjects;

    private bool doAction = false;


    // comment the following SeralizedField after testing the class
    [SerializeField]
    private int level;
    public int progress;
    private int maxProgress;
    private int lastMaxProgress;
    
    private void Awake()
    {
        level = 1;
        progress = 0;
        maxProgress = 4;
        lastMaxProgress = 0;
        Upgradable.BaseUpgradeEvent += CheckIndividualUpgrade;
        Upgradable.DownGrade += ChangeProgress;
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
    public void CheckForUpgrade()
    {
        if(progress == maxProgress)
        {
            doAction = true;
        }
    }
    public bool CheckIndividualUpgrade(int level)
    {
        bool doAct = false;

        if ((level < this.level) || (level == this.level && progress >= lastMaxProgress))
        {
            doAct = true;
            ChangeProgress(level);
            CheckForUpgrade();
        }    

        return doAct;
    }
    private void ChangeProgress(int level) 
    {
        progress += level;
    }
    private void CalculateMaxProgress()
    {
        lastMaxProgress = maxProgress;
        maxProgress = level * (level + 1) * 2;
    }
    public int GetLevel()
    {
        return level;
    }
}
