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

    // World size vars
    public Renderer rend;
    private Vector3 currentWorldSize;
    private Vector3 baseWorldSize;

    private void Start()
    {
        baseWorldSize = rend.bounds.size;
    }

    private void Update()
    {
        currentWorldSize = new Vector3(baseWorldSize.x * transform.lossyScale.x, baseWorldSize.y * transform.lossyScale.y, 1);

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

            Vector3 newWorldSize = new Vector3(baseWorldSize.x * transform.lossyScale.x, baseWorldSize.y * transform.lossyScale.y, 1);
            float scaleDif = newWorldSize.magnitude - currentWorldSize.magnitude;

            float left = transform.position.x - currentWorldSize.x / 2f;
            float top = transform.position.y - currentWorldSize.y / 2f;



            // while zooming in or out, parent object should stay centered on the position of the mouse, so that you can zoom in on a specific part of the skill tree, and not just the center of the screen
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movement = (transform.position - mouseWorldPoint) * (Mathf.Abs(prevScale.x - transform.localScale.x));
            movement.z = 0;

            float xRatio = Mathf.Abs(mouseWorldPoint.x - left)/currentWorldSize.x;
            xRatio -= 0.5f;
            float yRatio = Mathf.Abs(mouseWorldPoint.y - top) / currentWorldSize.y;
            yRatio -= 0.5f;

            movement.x = scaleDif * -xRatio;
            movement.y = scaleDif * -yRatio;
            

            transform.position += movement*0.705f;
            minX = minXInitial * transform.localScale.x + margins.x;
            minY = minYInitial * transform.localScale.y + margins.y;
            maxX = maxXInitial * transform.localScale.x - margins.x;
            maxY = maxYInitial * transform.localScale.x - margins.y;

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
