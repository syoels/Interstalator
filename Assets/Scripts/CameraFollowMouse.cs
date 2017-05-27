using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class CameraFollowMouse : MonoBehaviour {

    [Tooltip("Distance between mouse and screen edge before camera starts to move")]
    public int edgeDistancePercentage = 15;
    public int movementSpeed = 1;


    private Camera cameraComp;

    void Awake() {
        cameraComp = GetComponent<Camera>();
    }


    /// <summary>
    /// Change camera position based on mouse
    /// </summary>
    void LateUpdate() {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0) {
            cameraComp.orthographicSize += mouseScroll;
        }


        Vector3 mousePosition = Input.mousePosition;

        // Screen size may vary so edge distance is in percentages
        int widthDistance = Screen.width / 100 * edgeDistancePercentage;
        int heightDistance = Screen.height / 100 * edgeDistancePercentage;

        if (mousePosition.x < widthDistance) {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        } else if (mousePosition.x > Screen.width - widthDistance) {
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }

        if (mousePosition.y < heightDistance) {
            transform.position += Vector3.down * Time.deltaTime * movementSpeed;
        } else if (mousePosition.y > Screen.height - heightDistance) {
            transform.position += Vector3.up * Time.deltaTime * movementSpeed;
        }
    }

}
}