using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{

    [SerializeField] UIBar uiBar;
    [SerializeField] float maxFright = 100;
    [SerializeField] float frightDecayRate = 3;
    
    [SerializeField] Image fillBarImage;

    float currentFright = 0;
    bool isPermanentlyScared = false;

    private void Start() {
        uiBar.UpdateBarPercentFill(currentFright/maxFright);
    }

    // Update is called once per frame
    void Update() {

        //fright decay & update UI.
        if (isPermanentlyScared == false) {
            currentFright -= frightDecayRate * Time.deltaTime;
            if (currentFright < 0) currentFright = 0;
        }
        uiBar.UpdateBarPercentFill(currentFright / maxFright);
        


    }

    public void ScareMe(float amount) {
        if (isPermanentlyScared) return;
        Debug.Log($"I got scared by {amount} amount");
        currentFright += amount;
        if (currentFright >= maxFright) {
            PermanentlyScared();
        }
    }

    public void PermanentlyScared() {
        Debug.Log("Screw this, I'm outta here!");
        isPermanentlyScared = true;
        fillBarImage.color = Color.red;
    }
}
