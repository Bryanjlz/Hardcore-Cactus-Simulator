using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDroplet : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = true;

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
}
