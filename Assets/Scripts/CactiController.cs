using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiController : MonoBehaviour
{
    //Constants
    const float HOR_DIST = 1.4f;
    const float VERT_DIST = 1f;
    const float Y_START = -2.5f;
    float[] xStart = { -6f, -6.8f, -7.7f };

    //Cactus array
    GameObject[][] cacti = new GameObject[3][];

    //Cactus prefab
    [SerializeField]
    GameObject cactusPrefab;

    //internal cactus count
    int cactusCount;

    void Start()
    {
        //Instantiate array
        for (int i = 0; i < 3; i++) {
            cacti[i] = new GameObject[10];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cactusCount < GameController.instance.plants) {
            AddCactus();
            cactusCount++;
        }
    }

    private void AddCactus () {
        if (GameController.instance.plants < 30) {
            int row = Random.Range(0, 2);
            int col = Random.Range(0, 9);
            while (cacti[row][col] != null) {
                row = Random.Range(0, 2);
                col = Random.Range(0, 9);
            }
            Instantiate(cactusPrefab, new Vector3(xStart[row] + col * HOR_DIST, Y_START + row * VERT_DIST, 0), Quaternion.identity);
        }
        

    }
}
