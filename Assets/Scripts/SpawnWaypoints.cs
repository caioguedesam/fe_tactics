using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaypoints : MonoBehaviour
{
    public GameObject waypoint;
    private List<GameObject> waypointList = new List<GameObject>();
    public LayerMask waypointLayer;

    private Vector2 currentCursorPosition;
    private Vector2 oldCursorPosition;

    private void Start()
    {
        currentCursorPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private bool UpdateCursorPosition()
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
        if(newPosition != currentCursorPosition)
        {
            oldCursorPosition = currentCursorPosition;
            currentCursorPosition = newPosition;
            return true;
        }
        return false;
    }

    private bool CheckExistingWaypoint()
    {
        if (Physics2D.OverlapCircle(new Vector2(oldCursorPosition.x, oldCursorPosition.y), 0.5f, waypointLayer))
            return true;
        return false;
    }

    private void Update()
    {
        if(UpdateCursorPosition() && !CheckExistingWaypoint())
        {
            GameObject newWaypoint = Instantiate(waypoint, oldCursorPosition, Quaternion.identity);
            waypointList.Add(newWaypoint);

            // Add pathing
        }

        if(Input.GetMouseButtonDown(0))
        {
            foreach(GameObject waypointObj in waypointList)
            {
                Destroy(waypointObj);
            }
        }
    }
}
