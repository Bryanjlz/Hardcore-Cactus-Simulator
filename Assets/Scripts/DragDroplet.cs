using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDroplet : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = true;

    private Cactus cactusWatered;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
            
        }

        if (isBeingHeld == true)
        {
            startPosX = GameController.instance.mousex;
            startPosY = GameController.instance.mousey;

            if (cactusWatered != null)
            {
                cactusWatered.GetComponent<Cactus>().waterLevel += GameController.HOUR*4 + GameController.instance.inGameDeltaTime*30;
            }

            this.gameObject.transform.localPosition = new Vector3(startPosX, startPosY, 0);
        }
        else
        {
            gameObject.SetActive(false);
           
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Cactus otherItem = other.GetComponent<Cactus>();
        cactusWatered = otherItem;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Cactus otherItem = other.GetComponent<Cactus>();
        cactusWatered = otherItem;
        Debug.Log(otherItem);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        cactusWatered = null;
    }
}
