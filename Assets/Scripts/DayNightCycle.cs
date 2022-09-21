using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayNightCycle : MonoBehaviour
{

    [Header("Light Info")]
    [SerializeField] 
    private Light worldLight;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private LightingPreset preset;

    [Header("Spawn Info")]
    [SerializeField]
    private float defualtMaxSpawnRate = 1;

    [SerializeField]
    private float fixedSpawnRatePercent = 50;

    [SerializeField]
    private Spawner[] outposts;

    [Header("Day Info")]

    // the length of day (in minutes) 
    [SerializeField]
    private float dayLength = 2f;
    
    [SerializeField]
    private float nightPercent = 16;

    [SerializeField, Range(0, 24)]
    private float currentTime;
    private bool night;
    private int daysPassed;
    private float dayStart = 6f;
    private float nightStart = 18f;
    private float dayTime;
    private float nightTime;
    private float maxSpawnRate;
    private float spawnRate;
    private float spawnTimeCount = 0;
    
    public event Action onDay, onNight;
    private void Awake()
    {
        FirstInfo();
    }

    private void FixedUpdate()
    {
        DayCycle();
    }

    private void FirstInfo()
    {

        spawnRate = 0;
        daysPassed = 0;
        // currentTime = dayStart;
        maxSpawnRate = defualtMaxSpawnRate;

        nightTime = dayLength * (nightPercent / 100) * 60;
        dayTime = dayLength * (1 - nightPercent / 100) * 60;

        if (nightPercent == 0)
            nightTime = 0.1f;
        else if (nightPercent == 100)
            dayTime = 0.1f;

        night = false;

        // comment it if you want the time to start over each time
        GetSavedData();
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

        //SaveDayTime();
        //here i called it just to check it works
    }
    private void ChangeLight(float timePercent)
    {

        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        worldLight.color = preset.directionalColor.Evaluate(timePercent);

        worldLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        moonLight.transform.localRotation = Quaternion.Euler(new Vector3(360f-((timePercent * 360f) - 90f), 180f-170f, 0));

    }

    private void NightSpawn()
    {
        foreach(Spawner s in outposts)
        {
            SetSpawnRate();
            s.SetSpawnRate(spawnRate);

            if (!s.GetSpawnStatus())
            {
                s.StartSpawning();
            }
        }
    }
    private void SetSpawnRate()
    {
        float deltaTime = ((1 - (fixedSpawnRatePercent / 100)) / 2) * nightTime;


        if (spawnTimeCount <= deltaTime )
        { 
            spawnRate = (spawnTimeCount / deltaTime) * maxSpawnRate;
        }
        else if (spawnTimeCount >= nightTime - deltaTime)
        {
            spawnRate = maxSpawnRate - (((spawnTimeCount-(nightTime - deltaTime)) / deltaTime) * maxSpawnRate);
        }
    }

    // call it where ever you want to save time and the days passed
    public void SaveDayTime()
    {
        PlayerPrefs.SetFloat("currentTime", currentTime);
        PlayerPrefs.SetInt("daysPassed", daysPassed);
        PlayerPrefs.Save();
    }
    public void GetSavedData()
    {
        if(PlayerPrefs.HasKey("currentTime") && PlayerPrefs.HasKey("daysPassed"))
            SetTime(PlayerPrefs.GetFloat("currentTime"), PlayerPrefs.GetInt("daysPassed"));
    }
    private void SetTime(float lastTime,int daysPassed)
    {
        this.daysPassed = daysPassed;
        if(lastTime > nightStart)
        {
            spawnTimeCount = (lastTime - nightStart) * nightTime / 12;
            night = true;
        }
        else if(lastTime < dayStart)
        {
            spawnTimeCount = (lastTime + 24 - nightStart) * nightTime / 12;
            night = true;

        }

        currentTime = lastTime;
    }
    
    private void SetMaxSpawnRate()
    {
        maxSpawnRate = defualtMaxSpawnRate + daysPassed * 2;
    }
    public bool IsNight()
    {
        return night;
    }

   
}
