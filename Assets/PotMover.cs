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

    private bool alreadyDead;

    // Start is called before the first frame update
    void Start()
    {
        originalX = this.gameObject.transform.localPosition.x;
        originalY = this.gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;

            // Go back to original position
            if (true)
            {
                this.gameObject.transform.localPosition = new Vector3(originalX, originalY, 0);
            }
        }

        if (isBeingHeld == true)
        {
            startPosX = GameController.instance.mousex;
            startPosY = GameController.instance.mousey;

            Debug.Log(originalX + ", " + originalY);
            this.gameObject.transform.localPosition = new Vector3(startPosX - shiftedX, startPosY - shiftedY, 0);
        }
    }

    private void OnMouseDown()
    {
        startPosX = GameController.instance.mousex;
        startPosY = GameController.instance.mousey;

        originalX = this.gameObject.transform.localPosition.x;
        originalY = this.gameObject.transform.localPosition.y;

        shiftedX = startPosX - this.gameObject.transform.localPosition.x;
        shiftedY = startPosY - this.gameObject.transform.localPosition.y;

        isBeingHeld = true;
        Debug.Log("why");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        OutTheWindow otherItem = other.collider.GetComponent<OutTheWindow>();


        if (otherItem != null)
        {
            Destroy(gameObject);
            alreadyDead = true;
            if (alreadyDead)
            {
                GameController.instance.plants -= 1;
            }
            Debug.Log(GameController.instance.plants);
        }

        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);
    }
}
