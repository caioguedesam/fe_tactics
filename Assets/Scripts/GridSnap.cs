using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// GridSnap:
/// This script implements the grid system and snaps the cursor game object to it. As simple as that.
///
/// Caio Guedes, 10/2019.

public class GridSnap : MonoBehaviour
{
    // Size of grid to snap
    public float snapValue = 1;

    // Rounding function
    private float Round(float input, float roundThreshold)
    {
        // Input relative to the snapValue threshold
        // example: if input = 1 and snapValue = 2, inputRelativeToThreshold = 0.5
        // as in 1 is 0.5 times 2. Or, if input = 4, inputRelativeToThreshold = 2 and so on...
        float inputRelativeToThreshold = input / roundThreshold;
        // Effectively rounding to appropriate value in relation to threshold
        return roundThreshold * Mathf.Round(inputRelativeToThreshold);
    }

    // This updates the cursor position if the mouse is on a value over the threshold
    private void ChangeCursorPosition(float x, float y)
    {
        Vector2 cursorPosition = new Vector2(Round(x, snapValue), Round(y, snapValue));
        transform.position = new Vector3(cursorPosition.x, cursorPosition.y, transform.position.z);
    }

    void Update()
    {
        Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ChangeCursorPosition(mouseVector.x, mouseVector.y);
    }
}
