using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDroplet : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = true;
    Vector3 mousePos;

    private void Update()
    {
        Debug.Log(isBeingHeld);

        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
        }

        if (isBeingHeld == true)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            startPosX = mousePos.x;
            startPosY = mousePos.y;

            this.gameObject.transform.localPosition = new Vector3(startPosX, startPosY, 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
