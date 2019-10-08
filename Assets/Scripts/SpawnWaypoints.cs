using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// SpawnWaypoints:
/// This script manages the waypoints left by the cursor while clicking a character.
/// This means it also renders appropriate arrow sprites based on last input direction.
/// 
/// It's under construction, so a lot of it is still not final and fully functional.
/// 
/// Caio Guedes, 10/2019

public class SpawnWaypoints : MonoBehaviour
{
    // Waypoint object, layer and list
    public GameObject waypoint;
    private List<GameObject> waypointList = new List<GameObject>();
    public LayerMask waypointLayer;

    // Arrow sprites
    public Sprite arrowHead;
    public Sprite arrowBody;
    public Sprite arrowCorner;

    // Waypoint positions
    private Vector2 currentCursorPosition;
    private Vector2 oldCursorPosition;

    private void Start()
    {
        currentCursorPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // UpdateCursorPosition: This checks the current cursor position for updates and stores it 
    // in appropriate variables.
    // Returns: true if cursor changed position, false otherwise.
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

    // CheckExistingWaypoint: checks for waypoints in given tile. Uses OverlapCircle for detection.
    // Returns: true if there already is a waypoint in tile, false otherwise.
    private bool CheckExistingWaypoint()
    {
        if (Physics2D.OverlapCircle(new Vector2(oldCursorPosition.x, oldCursorPosition.y), 0.5f, waypointLayer))
            return true;
        return false;
    }

    // UpdateWaypointArrow: changes waypoint arrow sprite as soon as new neighboring waypoint pops up.
    // TODO
    private void UpdateWaypointArrow(GameObject wayPointOld, GameObject wayPointNew)
    {
        //todo
        wayPointOld.GetComponent<SpriteRenderer>().sprite = arrowBody;
    }

    private void ChangeWaypointArrowDirection(GameObject wayPointOld, GameObject wayPointNew)
    {
        Vector2 distance = wayPointOld.transform.position - wayPointNew.transform.position;
        // The four corners
        if(distance.x < 0 && distance.y < 0)
        {
            // Going left then down
        }
    }

    private void Update()
    {
        if(UpdateCursorPosition() && !CheckExistingWaypoint())
        {
            GameObject newWaypoint = Instantiate(waypoint, oldCursorPosition, Quaternion.identity);
            waypointList.Add(newWaypoint);

            // Arrow sprite handling
            newWaypoint.GetComponent<SpriteRenderer>().sprite = arrowHead;
            Vector2 directionToCursor = currentCursorPosition - new Vector2(newWaypoint.transform.position.x, newWaypoint.transform.position.y);

            if (directionToCursor.x < 0)
                newWaypoint.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (directionToCursor.x > 0)
                newWaypoint.transform.rotation = Quaternion.Euler(0, 0, -90);
            else if (directionToCursor.y < 0)
                newWaypoint.transform.rotation = Quaternion.Euler(0, 0, 180);
            else if (directionToCursor.y > 0)
                newWaypoint.transform.rotation = Quaternion.Euler(0, 0, 0);

            if(waypointList.Count > 1)
            {
                UpdateWaypointArrow(waypointList[waypointList.Count - 2], newWaypoint);
            }

            Vector3 angle = Quaternion.ToEulerAngles(newWaypoint.transform.rotation);
            Debug.Log("Last waypoint rotation: (0, 0, " + Mathf.Rad2Deg * angle.z + ")");

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
