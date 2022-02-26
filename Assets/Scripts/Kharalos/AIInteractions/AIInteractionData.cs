using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AIInteractionData", order = 1)]
public class AIInteractionData : ScriptableObject
{
        public string interactionAnimationTrigger;
        public float interactionTime;
}