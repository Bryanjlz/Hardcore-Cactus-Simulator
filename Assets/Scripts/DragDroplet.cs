using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDroplet : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = true;

    // Jank
    public int JANK;
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

            this.gameObject.transform.localPosition = new Vector3(startPosX, startPosY, 0);
        }
        else
        {
            
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        JANK++;
        Cactus otherItem = other.GetComponent<Cactus>();

        if (otherItem != null && JANK <= 1)
        {
            gameObject.SetActive(false);
            isBeingHeld = false;
            Debug.Log("Watered");
        }
    }
}
