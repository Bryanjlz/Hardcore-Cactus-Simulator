using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactiController : MonoBehaviour
{

    // this one
    public static CactiController instance;

    //Constants
    const float HOR_DIST = 1.4f;
    const float VERT_DIST = 1f;
    const float Y_START = -1.4f;
    float[] xStart = { -6f, -6.8f, -7.7f };

    //Cactus array
    GameObject[][] cacti = new GameObject[3][];

    //Cactus prefab
    [SerializeField]
    GameObject cactusPrefab;

    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        //Instantiate array
        for (int i = 0; i < 3; i++) {
            cacti[i] = new GameObject[10];
        }
    }

    public void AddCactus () {
        if (GameController.instance.plants < 30) {
            int row = Random.Range(0, 3);
            int col = Random.Range(0, 10);
            while (cacti[row][col] != null) {
                row = Random.Range(0, 3);
                col = Random.Range(0, 10);
            }
            var go = Instantiate(cactusPrefab, new Vector3(xStart[row] + col * HOR_DIST, Y_START - row * VERT_DIST, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
            go.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2 * row + 1;
            go.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2 * row;
            cacti[row][col] = go;
        }
        

    }
}
