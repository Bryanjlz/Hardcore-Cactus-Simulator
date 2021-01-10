using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    //Constants
    
    private const int PERFECT_WATER = 3 * GameController.MONTH;
    private const int DEATH_WATER_ERROR = (int)(PERFECT_WATER * 0.5f);
    private const int DEATH_WATER_UPPER = PERFECT_WATER + DEATH_WATER_ERROR;
    private const int DEATH_WATER_LOWER = 0;
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
    [SerializeField]
    int waterLevel;
    [SerializeField]
    bool isAlive;
    private int temp;

    //Cactus sprites
    [SerializeField]
    Sprite[] cacti = new Sprite[9];

    // Start is called before the first frame update
    void Start()
    {
        waterLevel = PERFECT_WATER;
        temp = GameController.instance.temperature;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        float tempGrowthReduction = ((temp - PERFECT_TEMP) / DEATH_TEMP_ERROR) * 0.5f;
        float waterGrowthReduction = ((waterLevel - PERFECT_WATER) / DEATH_WATER_ERROR) * 0.05f;
        growthRate = PERFECT_GROWTH - tempGrowthReduction - waterGrowthReduction;
        if (waterLevel >= DEATH_WATER_UPPER || waterLevel <= DEATH_WATER_LOWER || temp <= DEATH_TEMP_LOWER || temp >= DEATH_TEMP_UPPER) {
            isAlive = false;
        }
        
    }
}
