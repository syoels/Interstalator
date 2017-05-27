using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class CameraFollowMouse : MonoBehaviour {

    [Tooltip("Distance between mouse and screen edge before camera starts to move")]
    public int edgeDistance = 10;
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

//        Vector3 mousePosition = cameraComp.ScreenToWorldPoint(new Vector3(
//                                    Input.mousePosition.x,
//                                    Input.mousePosition.y,
//                                    transform.position.z
//                                ));
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x < edgeDistance) {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        } else if (mousePosition.x > Screen.width - edgeDistance) {
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }

        if (mousePosition.y < edgeDistance) {
            transform.position += Vector3.down * Time.deltaTime * movementSpeed;
        } else if (mousePosition.y > Screen.height - edgeDistance) {
            transform.position += Vector3.up * Time.deltaTime * movementSpeed;
        }
    }

}
}