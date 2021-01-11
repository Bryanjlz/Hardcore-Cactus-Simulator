using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject waterDroplet;
    private GameObject thisWater;

    // Start is called before the first frame update
    void Start()
    {
        thisWater = Instantiate(waterDroplet);
        thisWater.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            thisWater.GetComponent<DragDroplet>().isBeingHeld = true;
            thisWater.transform.localPosition = new Vector3(GameController.instance.mousex, GameController.instance.mousex, 0);
            thisWater.GetComponent<DragDroplet>().JANK = 0;
            thisWater.SetActive(true);
        }
    }
}
