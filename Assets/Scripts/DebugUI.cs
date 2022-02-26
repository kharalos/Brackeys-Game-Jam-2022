using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugUI : MonoBehaviour
{

    int fps;
    int verts;

    private void Start() {
        StartCoroutine(MyMethod());
    }

    IEnumerator MyMethod() {
        while (true) {
            fps = (int)(1f / Time.unscaledDeltaTime);
            #if UNITY_EDITOR
            verts = UnityStats.vertices/1000;
            #endif
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnGUI() {
        GUI.Label(new Rect(1000, 10, 100, 20), $"fps: {fps}");
        #if UNITY_EDITOR
        GUI.Label(new Rect(1000, 30, 100, 20), $"verts: {verts}k");
        #endif
    }
}
