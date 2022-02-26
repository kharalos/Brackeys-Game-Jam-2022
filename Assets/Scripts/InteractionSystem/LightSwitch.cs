using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{

    public List<Light> m_Light; // im using m_Light name since 'light' is already a variable used by unity
    public bool isOn;

    private void Start()
    {
        UpdateLight();
    }

    void UpdateLight()
    {
        foreach (var light in m_Light)
            light.enabled = isOn;
    }

    public override string GetDescription()
    {
        if (isOn) return "Press [E] to turn <color=red>off</color> the light.";
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact()
    {
        isOn = !isOn;
        OnInteract.Invoke();
        UpdateLight();
        if(!isOn) FindObjectOfType<MapPointsManager>().TaskAssigner(this);
    }
}