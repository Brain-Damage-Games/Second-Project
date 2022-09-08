using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayNightCycle : MonoBehaviour
{
    
    
    [SerializeField] 
    private Light worldLight;

    [SerializeField] 
    private Spawner[] outposts;

    [SerializeField]
    private LightingPreset preset;

    [SerializeField]
    private float defualtMaxSpawnRate = 1;

    [SerializeField]
    private float fixedSpawnRatePercent = 50;

    [Header("Day Info")]

    // the length of day (in minutes) 
    [SerializeField]
    private float dayLength = 2f;
    
    [SerializeField]
    private float nightPercent = 16;

    [SerializeField, Range(0, 24)]
    private float currentTime;

    [SerializeField]
    private bool night;




    private int daysPassed;
    private float dayStart = 6f;
    private float nightStart = 18f;
    private float dayTime;
    private float nightTime;
    private float maxSpawnRate;
    private float spawnRate;
    private float spawnTimeCount = 0;

    private void Awake()
    {
        spawnRate = 0;
        daysPassed = -1;
        currentTime = dayStart;
        maxSpawnRate = defualtMaxSpawnRate;

        nightTime = dayLength * (nightPercent / 100) * 60;
        dayTime = dayLength * (1 - nightPercent / 100) *60;

        if (nightPercent == 0)
            nightTime = 0.1f;
        else if (nightPercent == 100)
            dayTime = 0.1f;

        night = false;
    }

    private void FixedUpdate()
    {
        DayCycle();
    }

    private void DayCycle()
    {
        if (Application.isPlaying)
        {
            if (currentTime >= dayStart && currentTime <= nightStart)
            {
                if (night)
                {
                    night = false;
                    daysPassed += 1;
                    SetMaxSpawnRate();
                }

                currentTime += Time.deltaTime * 12 / dayTime;
            }
            else
            {
                if (!night)
                {
                    night = true;
                    spawnTimeCount = 0;
                }

                currentTime += Time.deltaTime * 12 / nightTime;
                spawnTimeCount += Time.deltaTime;
                NightSpawn();

            }
            currentTime %= 24;
            ChangeLight(currentTime / 24f);
        }
        else
        {
            ChangeLight(currentTime / 24f);
        }
    }
    private void ChangeLight(float timePercent)
    {

        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        worldLight.color = preset.directionalColor.Evaluate(timePercent);

        worldLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));

    }

    private void NightSpawn()
    {
        foreach(Spawner s in outposts)
        {
            SetSpawnRate();
            s.SetSpawnRate(spawnRate);

            if (!s.GetSpawnStatus())
            {
                s.Spawn();
            }
        }
    }
    private void SetSpawnRate()
    {
        float deltaTime = ((1 - (fixedSpawnRatePercent / 100)) / 2) * nightTime;

        print("rate:"+ spawnRate+ "spawnTime:"+ spawnTimeCount+ "deltaTime:"+ deltaTime+"MaxSpawnRate"+maxSpawnRate);
        if (spawnTimeCount <= deltaTime )
        { 
            spawnRate = (spawnTimeCount / deltaTime) * maxSpawnRate;
        }
        else if (spawnTimeCount >= nightTime - deltaTime)
        {
            spawnRate = maxSpawnRate-(((spawnTimeCount-(nightTime - deltaTime)) / deltaTime) * maxSpawnRate);
        }
    }
    private void SetMaxSpawnRate()
    {
        maxSpawnRate = defualtMaxSpawnRate + daysPassed * 2;
    }
    public bool IsNight()
    {
        return night;
    }
    /*private void SetNight()
    {
        night = true;
    }
    private void SetDay()
    {
        night = false;
        daysPassed += 1;
    }*/
}
