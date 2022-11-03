using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Upgradable : MonoBehaviour
{
    [Header("values")]
    [SerializeField]
    float maxHealthIncrease = 10f;
    [SerializeField]
    float damageValueIncrease = 10f;
    [Header("Upgrade Prefabs")]
    [SerializeField]
    private GameObject[] statePrefabs;
    [SerializeField]
    private Sprite[] slidersBackground;
    [SerializeField]
    private Color[] slidersFill;
    [Header("Slider")]
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image sliderBackground;
    [SerializeField]
    private Image sliderFill;
    [SerializeField]
    private TextMeshProUGUI txt;
    [Header("The Base")]
    [SerializeField]
    BaseUpgrade BU;
    [Header("ForceField")]
    [SerializeField] private float upgradeRange = 2f;
    private bool playerInRange = false;
    [SerializeField] private GameObject forceField;
    [SerializeField] private GameObject saveAndLoad;


    [SerializeField]
    private int inputMoney;

    // comment the following SeralizedField after testing the class
    //[SerializeField]
    int level;
    
    private GameObject parent;
    private Damageable damageable;
    private Damager damager;


    private int payedMoney;
    private int maxMoney;
    private int levelCount;

    private void Awake()
    {
        saveAndLoad.GetComponent<SaveAndLoad>().BaseLoad();
        saveAndLoad.GetComponent<SaveAndLoad>().UpgradableLoad();

        saveAndLoad.GetComponent<SaveAndLoad>().OutPostLoad();

    }

    private void Start()
    {


        parent = gameObject;

        damageable = GetComponent<Damageable>();
        damager = GetComponent<Damager>();


        ChangeFace();
        
        UpdateMaxMoney();
        if (slider != null)
        {
            ChangeProgressFace();
            slider.gameObject.SetActive(false);

        }
        levelCount = statePrefabs.Length;

        forceField.transform.localScale = new Vector3(upgradeRange * 2, upgradeRange * 2, upgradeRange * 2);
        GetComponent<SphereCollider>().radius = upgradeRange;
    }
    //remove comment form codes below to test this class
    
    
    private void Update()
    {
        if (playerInRange && slider!=null )
        {
            inputMoney = Balance.GetBalance();
            UpdateHandler();
        }
    }
    private void UpdateHandler()
    {
        if (BU.CheckIndividualUpgrade(level,false) && inputMoney > 0 && level<=levelCount)
        {

            if (inputMoney > (maxMoney - payedMoney))
            {
                Balance.ChangeBalance((int)(inputMoney - (maxMoney - payedMoney)));
                inputMoney = (maxMoney - payedMoney);
            }
            else
            {
                Balance.ChangeBalance(0);
            }

            payedMoney += inputMoney;
            
            if (payedMoney >= maxMoney)
            {
                payedMoney -= maxMoney;
                BU.CheckIndividualUpgrade(level, true);
                Upgrade();
                if (level - 1 < statePrefabs.Length)
                {
                    ChangeProgressFace();

                }
            }

        }
        else if (inputMoney < 0)
        {
            payedMoney += inputMoney;
            if (payedMoney < 0)
            {
                payedMoney = 0;
                Downgrade();
                if (level - 1 >= 0)
                {
                    ChangeProgressFace();
                }
            }
        }

        if (slider!=null)
        {
            slider.value = payedMoney;
            txt.text = ((int)(payedMoney * 100) / maxMoney).ToString() + "%";


        }
    }
    public void Upgrade()
    {
        level++;
        UpdateMaxMoney();
        damageable.SetMaxHealth(damageable.GetMaxHealth() + maxHealthIncrease);
        damager.SetDamageValue(damager.GetDamageValue() + damageValueIncrease);
            
        if (level - 1 < statePrefabs.Length)
        {
            ChangeFace();
                
        }
        else
        {
            print("Upgradable: no more statePrefabs for this upgradableObject");
        }
        
    }
    public void Downgrade()
    {
        if (level <= 1)
        {
            print("this upgradableObject shall be destroyed");
        }
        else
        {
            level--;
            UpdateMaxMoney();
            damageable.SetMaxHealth(damageable.GetMaxHealth() - maxHealthIncrease);
            damager.SetDamageValue(damager.GetDamageValue() - damageValueIncrease);

            if (level - 1 >= 0)
            {
                BU.ChangeProgress(-level);
                ChangeFace();
            }
            else
            {
                print("Upgradable: no more option Downgrade for this upgradableObject");
            }
        }
    }
    private void ChangeFace()
    {
        //print(level+"name:"+gameObject.name);
        Vector3 position = parent.transform.position;
        Quaternion q = parent.transform.rotation;
        GameObject currentState = parent.transform.GetChild(0).gameObject;
        Destroy(currentState);
        GameObject newState = Instantiate(statePrefabs[level - 1], position, q);
        newState.transform.SetParent(parent.transform);
        newState.transform.SetAsFirstSibling();

    }
    private void ChangeProgressFace()
    {
        slider.maxValue = maxMoney;
        sliderFill.color = slidersFill[level - 1]; ;
        sliderBackground.sprite = slidersBackground[level - 1];
        slider.value = payedMoney;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.isTrigger) return;
        if (col.CompareTag("Player"))
        {
            playerInRange = true;
            if(slider!=null)
                slider.gameObject.SetActive(true);
            forceField.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.isTrigger) return;
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
            if(slider != null)
                slider.gameObject.SetActive(false);
            forceField.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void UpdateMaxMoney()
    {
        maxMoney = 100 * level;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetPayedMoney()
    {
        return payedMoney;
    }

    public void SetPayedMoney(int payedMoney)
    {
        this.payedMoney = payedMoney;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

}
