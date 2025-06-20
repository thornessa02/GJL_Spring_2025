using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSlider : MonoBehaviour
{
    [SerializeField] bool horizontal;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    bool isSelected;
    Vector2 MouseDelta;

    private void OnMouseDown()
    {
        isSelected=true;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        MouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (isSelected)
        {
            if (horizontal)
            {
                transform.position += new Vector3(MouseDelta.x, 0, 0);
                if (transform.position.x < maxDistance) transform.position = new Vector3(maxDistance, transform.position.y, transform.position.z);
                if (transform.position.x > minDistance) transform.position = new Vector3(minDistance, transform.position.y, transform.position.z);
            }
            else
            {
                gameObject.transform.position += new Vector3(0, 0, MouseDelta.y);
                if (transform.position.z < maxDistance) transform.position = new Vector3(transform.position.x,transform.position.y, maxDistance);
                if (transform.position.z > minDistance) transform.position = new Vector3(transform.position.x,  transform.position.y, minDistance);
            }   
        }
    }
}
