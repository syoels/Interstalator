using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class MapBuilder : MonoBehaviour {

    public GameObject floorPrefab;

    // Use this for initialization
    void Start() {
        LoadMap();
    }

    void LoadMap() {
        // Create a row of floor tiles
        for (int i = -5; i <= 5; i++) {
            Instantiate(floorPrefab, new Vector3(i-0.5f, -1-0.5f, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// Calculates the bottom left corner of a given game object to place them in the correct position
    /// </summary>
    /// <param name="prefab">Prefab.</param>
    void CalculateBottomLeftCorner(GameObject prefab) {
        
    }
}
}