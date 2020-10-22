using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRTSController : MonoBehaviour
{
    public Controls controls;
    [SerializeField]
    private Transform selectionAreaTransform;
    private Vector3 startPositionMouse;
    private List<Unit> selectedUnits;
    
    private void Awake(){
        selectedUnits = new List<Unit>();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Press left mouse button down
        if (Input.GetMouseButtonDown(0)) {
            selectionAreaTransform.gameObject.SetActive(true);
            startPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Unselect all units if command is not pressed down
            if (!(Input.GetKey(controls.leftCommand) || Input.GetKey(controls.rightCommand))) {
                foreach(Unit unit in selectedUnits) {
                    unit.setSelectedVisible(false);
                    }
                selectedUnits.Clear();
            }
        }

        // Hold down left mouse button
        if (Input.GetMouseButton(0)) {
            // get current mouse position
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Identify position of lowerLeft of select area
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPositionMouse.x, currentMousePosition.x),
                Mathf.Min(startPositionMouse.y, currentMousePosition.y)
            );

            // Identify position of uperRight of select area
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPositionMouse.x, currentMousePosition.x),
                Mathf.Max(startPositionMouse.y, currentMousePosition.y)
            );

            // Posititon and scale area selection area
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        // Lift left mouse button up
        if (Input.GetMouseButtonUp(0)) {
            selectionAreaTransform.gameObject.SetActive(false);

            // Identify all units covered by draggin area
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPositionMouse, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            // Select all units within collider 
            foreach (Collider2D collider2D in collider2DArray) {
                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null) {
                    unit.setSelectedVisible(true);
                    selectedUnits.Add(unit);
                }
            }

            // if command is pressed, make sure that after adding the new units, we delete all duplicates!
            if (Input.GetKey(controls.leftCommand) || Input.GetKey(controls.rightCommand)) {
                selectedUnits = selectedUnits.Distinct().ToList();
            }
        }

        // Press right mouse button down
        if (Input.GetMouseButtonDown(1)) {
            // get current mouse position
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // list of positions for my group of units
            List<Vector3> targetPositionList = GetMultipleCirclesPositionListAround(currentMousePosition, new float[] {1f, 2f, 3f, 4f, 5f}, new int[] {5, 10, 15, 20, 25});

            // make units move to their respective position
            int targetPositionListIndex = 0;
            foreach(Unit unit in selectedUnits) {
                unit.moveToPosition(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex++;
            }
        }
    }

    // creates a list of positions on several circles around a central position
    // distanceArray[index] and positionCountArray[index] are used for the same circle
    // distanceArray.Length == positionCountArray.Length
    private List<Vector3> GetMultipleCirclesPositionListAround(Vector3 startPosition, float[] distanceArray, int[] positionCountArray) {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < distanceArray.Length; i++) {
            List<Vector3> positionRange = GetPositionListAround(startPosition, distanceArray[i], positionCountArray[i]);
            positionList.AddRange(positionRange);
        }
        return positionList;
    }

    // create a list of positions in one circle around a central position
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount) {
        List<Vector3> positionList = new List<Vector3>();

        // defining the angle for each unit
        float singleAngle = 360 / positionCount;

        for (int i = 0; i < positionCount; i++) {
            float angle = i * singleAngle;
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vector, float angle) {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
}
