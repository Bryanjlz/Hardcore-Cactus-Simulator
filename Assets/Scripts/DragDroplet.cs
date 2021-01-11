using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDroplet : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = true;

    // Jank
    private bool moreJank;
    private Cactus cactusWatered;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
            Debug.Log("first");
        }

        if (isBeingHeld == true)
        {
            startPosX = GameController.instance.mousex;
            startPosY = GameController.instance.mousey;

            this.gameObject.transform.localPosition = new Vector3(startPosX, startPosY, 0);
        }
        else
        {
            Debug.Log("got here");
            if (moreJank)
            {
                cactusWatered.GetComponent<Cactus>().waterLevel += GameController.MONTH;
                Debug.Log(cactusWatered.GetComponent<Cactus>().waterLevel);
                moreJank = false;
            } else {
                gameObject.SetActive(false);
            }
           
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Cactus otherItem = other.GetComponent<Cactus>();
        cactusWatered = otherItem;
        moreJank = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moreJank = false;
        cactusWatered = null;
    }
}
