using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    Vector3 mousePos;
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
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            thisWater.GetComponent<DragDroplet>().isBeingHeld = true;
            thisWater.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
            thisWater.SetActive(true);
        }
    }
}
