using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    
	void Start ()
    {

    }
	
	void Update ()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 5;
        float scrollSpeed = 70;

        if (mousePosX < scrollDistance)
        {
            transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
        }

        if (mousePosX >= Screen.width - scrollDistance)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }

        if (mousePosY < scrollDistance)
        {
            transform.Translate(transform.forward * -scrollSpeed * Time.deltaTime);
        }

        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.Translate(transform.forward * scrollSpeed * Time.deltaTime);
        }
    }
}