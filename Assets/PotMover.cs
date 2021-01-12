using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotMover : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private float shiftedX;
    private float shiftedY;
    private float originalX;
    private float originalY;
    public bool isBeingHeld = false;

    // Jank
    private int JANK;
    public Cactus connectedCactus;

    // Start is called before the first frame update
    void Start()
    {
        originalX = this.gameObject.transform.localPosition.x;
        originalY = this.gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !connectedCactus.isAscended)
        {
            isBeingHeld = false;

            GameController.instance.holdingCactus = false;
            this.gameObject.transform.localPosition = new Vector3(originalX, originalY, 0);
        }

        if (isBeingHeld == true)
        {
            startPosX = GameController.instance.mousex;
            startPosY = GameController.instance.mousey;
            GameController.instance.holdingCactus = true;

            this.gameObject.transform.localPosition = new Vector3(startPosX - shiftedX, startPosY - shiftedY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (!connectedCactus.isAscended) {
            startPosX = GameController.instance.mousex;
            startPosY = GameController.instance.mousey;

            originalX = this.gameObject.transform.localPosition.x;
            originalY = this.gameObject.transform.localPosition.y;

            shiftedX = startPosX - this.gameObject.transform.localPosition.x;
            shiftedY = startPosY - this.gameObject.transform.localPosition.y;

            isBeingHeld = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        JANK++;
        OutTheWindow otherItem = other.collider.GetComponent<OutTheWindow>();

        if (otherItem != null && JANK <= 1)
        {
            Destroy(gameObject);
            GameController.instance.holdingCactus = false;
            GameController.instance.plants -= 1;
            GameController.instance.timeMultiplier = GameController.instance.CalculateTimeMultiplier(GameController.instance.plants);
        }   
    }   
}
