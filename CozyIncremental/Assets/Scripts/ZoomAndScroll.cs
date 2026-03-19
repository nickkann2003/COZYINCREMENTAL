using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class ZoomAndScroll : MonoBehaviour
{
    // a class file that makes its attached object able to be zoomed in and out on, and moveable in 4 directions using the mouse
    [SerializeField]
    private float zoomSpeed = 1f;
    [SerializeField]
    private float scrollSpeed = 1f;
    [SerializeField]
    private float minZoom = 0.5f;
    [SerializeField]
    private float maxZoom = 5f;


    [SerializeField]
    private float minX = -10f;
    private float minXInitial;
    [SerializeField]
    private float maxX = 10f;
    private float maxXInitial;
    [SerializeField]
    private float minY = -10f;
    private float minYInitial;
    [SerializeField]
    private float maxY = 10f;
    private float maxYInitial;

    private Vector2 margins = new Vector2(10.5f, 5.25f);

    [SerializeField]
    private float zoomLerpSpeed = 0.1f;
    [SerializeField]
    private float scrollLerpSpeed = 0.1f;

    public GameObject topRightMarker;
    public GameObject botLeftMarker;

    private void Update()
    {
        // Zoom in and out with the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        
        minX = minXInitial * transform.localScale.x + margins.x;
        minY = minYInitial * transform.localScale.y + margins.y;
        maxX = maxXInitial * transform.localScale.x - margins.x;
        maxY = maxYInitial * transform.localScale.x - margins.y;
        
        if (scrollInput != 0)
        {
            float targetZoom = Mathf.Clamp(transform.localScale.x + scrollInput * zoomSpeed, minZoom, maxZoom);
            Vector3 prevScale = transform.localScale;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetZoom, targetZoom, targetZoom), zoomLerpSpeed);


            // while zooming in or out, parent object should stay centered on the position of the mouse, so that you can zoom in on a specific part of the skill tree, and not just the center of the screen
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movement = (transform.position - mouseWorldPoint) * (Mathf.Abs(prevScale.x - transform.localScale.x));
            movement.z = 0;
            transform.position += movement;




            // scroll position needs to check its clamp when zoom is changed
            Vector3 targetPosition = new Vector3(
                               Mathf.Clamp(transform.position.x, minX, maxX),
                                              Mathf.Clamp(transform.position.y, minY, maxY),
                                                             transform.position.z
                                                                        );
            transform.position = targetPosition;
        }

        // Scroll with the right mouse button
        if (Input.GetMouseButton(0))
        {
            float moveX = -Input.GetAxis("Mouse X") * scrollSpeed;
            float moveY = -Input.GetAxis("Mouse Y") * scrollSpeed;
            Vector3 targetPosition = new Vector3(
                               Mathf.Clamp(transform.position.x - moveX, minX, maxX),
                                              Mathf.Clamp(transform.position.y - moveY, minY, maxY),
                                                             transform.position.z
                                                                        );
            transform.position = Vector3.Lerp(transform.position, targetPosition, scrollLerpSpeed);
        }
    }

    private void OnValidate()
    {
        minXInitial = -topRightMarker.transform.position.x;
        minYInitial = -topRightMarker.transform.position.y;
        maxXInitial = -botLeftMarker.transform.position.x;
        maxYInitial = -botLeftMarker.transform.position.y;
    }

}
