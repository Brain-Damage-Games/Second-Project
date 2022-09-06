using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField]
    private int level;

    private Damageable damageable;

    private Damager damager;

    [SerializeField]
    private GameObject[] statePrefabs;


    private GameObject castle;

    [SerializeField]
    float maxHealthIncrease = 10f;

    [SerializeField]
    float damageValueIncrease = 10f;


    private void Awake()
    {
        level = 1;
        castle = gameObject;
        damageable = GetComponent<Damageable>();
        damager = GetComponent<Damager>();
    }

    //remove comment form codes below to test this class
    /*
    public bool update = false;
    private void Update()
    {
        if (update) 
        {
            Upgrade();
            update = false;
        }
    }
    */
    public void Upgrade() 
    {
        level++;

        float currentMaxHealth = damageable.GetMaxHealth();
        float currentDamageValue = damager.GetDamageValue();

        damageable.SetMaxHealth(currentMaxHealth + maxHealthIncrease);
        damager.SetDamageValue(currentDamageValue + damageValueIncrease);

        if (level - 2 < statePrefabs.Length)
        {
            Vector3 position = castle.transform.position;
            GameObject currentState = castle.transform.GetChild(0).gameObject;
            Destroy(currentState);
            GameObject newState = Instantiate(statePrefabs[level - 2], position, Quaternion.identity);
            newState.transform.SetParent(castle.transform);
            newState.transform.SetAsFirstSibling();
        }
        else
        {
            print("PlayerUpgrade: no more statePrefabs");
        }
    }
    public int GetLevel()
    {
        return level;
    }
}
