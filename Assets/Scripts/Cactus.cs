﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    //Constants
    
    private const float PERFECT_WATER = 3 * GameController.MONTH;
    private const float DEATH_WATER_ERROR = PERFECT_WATER * 0.5f;
    private const float DEATH_WATER_UPPER = PERFECT_WATER + DEATH_WATER_ERROR;
    private const float DEATH_WATER_LOWER = 0;
    private const float PERFECT_TEMP = 273 + 20;
    private const float DEATH_TEMP_ERROR = PERFECT_TEMP * 0.05f;
    private const float DEATH_TEMP_LOWER = PERFECT_TEMP - DEATH_TEMP_ERROR;
    private const float DEATH_TEMP_UPPER = PERFECT_TEMP + DEATH_TEMP_ERROR;
    private const float PERFECT_GROWTH = 1;

    //Status
    [SerializeField]
    int growthState;
    [SerializeField]
    float growth;
    [SerializeField]
    float growthRate;
    private float tempGrowthReduction;
    private float waterGrowthReduction;
    [SerializeField]
    float[] growthCheckpoints = new float[8];
    [SerializeField]
    float waterLevel;
    [SerializeField]
    bool isAlive;
    [SerializeField]
    bool isAscended;
    private int temp;
    private float timeElapsed;
    private float accumulatedTime;

    //Cactus sprites
    [SerializeField]
    Sprite[] cacti = new Sprite[9];

    //Cacti hitbox changes
    [SerializeField]
    float[] radius = new float[9];
    [SerializeField]
    float[] yPos = new float[9];
    [SerializeField]
    public CircleCollider2D circleCollider;
    [SerializeField]
    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        waterLevel = PERFECT_WATER;
        isAlive = true;

        growthCheckpoints[0] = GameController.MONTH * 6;
        growthCheckpoints[1] = GameController.YEAR;
        growthCheckpoints[2] = GameController.YEAR * 3;
        growthCheckpoints[3] = GameController.YEAR * 4;
        growthCheckpoints[4] = GameController.YEAR * 5;
        growthCheckpoints[5] = GameController.YEAR * 6;
        growthCheckpoints[6] = GameController.YEAR * 7;
        growthCheckpoints[7] = GameController.YEAR * 8;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Room stats
        timeElapsed = GameController.instance.inGameDeltaTime;
        accumulatedTime += timeElapsed;
        if (accumulatedTime > 1 && isAlive) {
            temp = GameController.instance.temperature;

            //Calculate growth rate
            tempGrowthReduction = Mathf.Abs((temp - PERFECT_TEMP) / DEATH_TEMP_ERROR) * 0.5f;
            waterGrowthReduction = Mathf.Abs((waterLevel - PERFECT_WATER) / DEATH_WATER_ERROR) * 0.5f;
            growthRate = PERFECT_GROWTH - tempGrowthReduction - waterGrowthReduction;

            //Update cactus stats
            waterLevel -= accumulatedTime;
            growth += accumulatedTime * growthRate;

            //Check for evolution
            if (growth >= growthCheckpoints[growthState] && growthState < 8) {
                growthState += 1;

                //change sprite and hitbox
                circleCollider.radius = radius[growthState];
                circleCollider.offset = new Vector2(circleCollider.offset.x, yPos[growthState]);
                spriteRenderer.sprite = cacti[growthState];
            }
            //Check for death
            if (waterLevel >= DEATH_WATER_UPPER || waterLevel <= DEATH_WATER_LOWER || temp <= DEATH_TEMP_LOWER || temp >= DEATH_TEMP_UPPER) {
                isAlive = false;

            }

            accumulatedTime = 0;
        }
        //death animation
        if (!isAlive) {
            Color current = spriteRenderer.color;
            float r = current.r;
            float g = current.g;
            float b = current.b;
            if (r > 150/255f) {
                r -= 1f/255f;
            }
            if (g > 113 / 255f) {
                g -= 1f / 256f;
            }
            if (b > 14 / 256f) {
                b -= 1f / 256f;
            }
            print(r + " " + g + " " + b);
            spriteRenderer.color = new Color(r, g, b);
        }
    }
}
