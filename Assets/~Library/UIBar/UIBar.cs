using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{

    [SerializeField] private Slider slider = default;
    [SerializeField] private bool faceTowardsCamera = true;
    private void LateUpdate() {
        if (faceTowardsCamera) transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void UpdateBarPercentFill(float percentage) {
        slider.value = percentage;
    }
}
