using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    //Constants
    
    private const float PERFECT_WATER = 6 * GameController.MONTH;
    private const float DEATH_WATER_ERROR = PERFECT_WATER * 2f;
    private const float DEATH_WATER_UPPER = PERFECT_WATER + DEATH_WATER_ERROR;
    private const float DEATH_WATER_LOWER = 0;
    private const float PERFECT_TEMP = 68;
    private const float DEATH_TEMP_ERROR = PERFECT_TEMP * 2;
    private const float DEATH_TEMP_LOWER = PERFECT_TEMP - DEATH_TEMP_ERROR;
    private const float DEATH_TEMP_UPPER = PERFECT_TEMP + DEATH_TEMP_ERROR;
    private const float PERFECT_GROWTH = 1f;
    private const float ASCENSION_SPEED = 2f;
    //Status
    [SerializeField]
    int growthState;
    [SerializeField]
    float growth;
    [SerializeField]
    float growthRate;
    private float tempGrowthBonus;
    private float waterGrowthBonus;
    [SerializeField]
    float[] growthCheckpoints = new float[8];
    [SerializeField]
    public float waterLevel;
    [SerializeField]
    bool isAlive;
    [SerializeField]
    bool isAscended;
    bool initAscendFlag = true;
    [SerializeField]
    GameObject ascendParticles;

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

    // Water Level
    public TMPro.TMP_Text waterText;

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

        waterText.text = (Math.Round((waterLevel / PERFECT_WATER) * 100).ToString() + "%");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            waterText.text = (Math.Ceiling((waterLevel / PERFECT_WATER) * 100).ToString() + "%");
        } else
        {
            waterText.text = ("RIP");
        }

        //Get Room stats
        timeElapsed = GameController.instance.inGameDeltaTime;
        accumulatedTime += timeElapsed;
        if (accumulatedTime > 1 && isAlive && !isAscended) {
            temp = (int) GameController.instance.temperature;

            // Bonuses
            tempGrowthBonus = Mathf.Abs((temp - PERFECT_TEMP) / PERFECT_TEMP);
            
            waterGrowthBonus = Mathf.Abs((waterLevel - PERFECT_WATER) / PERFECT_WATER);

            growthRate = PERFECT_GROWTH + (Mathf.Max((0.5f - waterGrowthBonus), 0) + Mathf.Max((0.5f - tempGrowthBonus), 0));

            //Update cactus stats
            waterLevel -= accumulatedTime;
            growth += accumulatedTime * growthRate;

            //Check for evolution
            if (growth >= growthCheckpoints[growthState] && growthState < 8)
            {
                growthState += 1;

                if (growthState == 8)
                {
                    isAscended = true;
                }

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
            spriteRenderer.color = new Color(r, g, b);
        }

        //Ascension animation
        if (isAscended) {
            Transform t = gameObject.transform;
            Instantiate(ascendParticles, t).transform.SetParent(null);
            if (initAscendFlag) {
                initAscendFlag = false;
                GameController.score++;
            }
            gameObject.transform.position = new Vector3(t.position.x, t.position.y + ASCENSION_SPEED * Time.deltaTime, t.position.z);
            if (t.position.y > 5.2) {
                Destroy(gameObject);
            }
        }
    }
}
