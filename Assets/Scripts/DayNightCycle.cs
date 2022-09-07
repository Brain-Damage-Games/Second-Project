using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField, Range(0, 24)] 
    private float currentTime;
    [SerializeField] 
    private Light worldLight;
    [SerializeField] 
    private bool night;
    [SerializeField] 
    private Spawner[] outposts;

    [SerializeField]
    private LightingPreset preset;

    // the length of day (in minutes) 
    [SerializeField]
    private float dayLength = 2f;
    
    [SerializeField]
    private float nightPercent = 16;

    [SerializeField]
    private float maxSpawnRate = 1;

    private int daysPassed;
    private float dayStart = 6f;
    private float nightStart = 18f;


    private float dayTime;
    private float nightTime;

    private void Awake()
    {
        //day starts from 6 am
        currentTime = dayStart;
        daysPassed = 0;
        dayTime = dayLength * (1 - nightPercent / 100) *60;
        nightTime = dayLength * (nightPercent/100) * 60;

        if (nightPercent == 0)
            nightTime = 0.1f;
        else if (nightPercent == 100)
            dayTime = 0.1f;
        SetDay();
    }

    private void Update()
    {
        DayCycle();
    }

    private void DayCycle()
    {
        if (Application.isPlaying)
        {
            if (!IsNight())
            {
                if (night)
                    SetDay();

                currentTime += Time.deltaTime * 12 / dayTime;
            }
            else
            {
                if (!night)
                    SetNight();

                else
                    currentTime += Time.deltaTime * 12 / nightTime;
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
    private void SetNight()
    {
        night = true;
    }
    private void SetDay()
    {
        night = false;
    }
    public bool IsNight()
    {
        if (currentTime >= dayStart && currentTime <= nightStart)
            return false;
        else
            return true;

    }
}
