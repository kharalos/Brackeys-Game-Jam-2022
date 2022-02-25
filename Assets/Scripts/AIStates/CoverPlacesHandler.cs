using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPlacesHandler : MonoBehaviour
{
    public Transform[] coverPlaces;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        GameObject[] coverPlaceObjects = GameObject.FindGameObjectsWithTag("Cover");
        coverPlaces = new Transform[coverPlaceObjects.Length];
        for (int i = 0; i < coverPlaceObjects.Length; i++){
            coverPlaces[i] = coverPlaceObjects[i].transform;
        }
    }
}
