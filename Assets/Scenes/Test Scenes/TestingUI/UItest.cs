using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItest : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float value = 1;
    [SerializeField] private List<UIBar> uibar;

    private void Update() {
        if (uibar.Count > 0) {
            foreach (var item in uibar) {
                item.UpdateBarPercentFill(value);
            }
        }
    }
}
