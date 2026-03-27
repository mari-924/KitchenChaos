using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter clearCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        ToggleVisuals(false);
    }   
    
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
        {
            ToggleVisuals(true);
        }
        else
        {
            ToggleVisuals(false);
        }
    }

   

    private void ToggleVisuals(bool isActive)
    {
        foreach(GameObject visualGameObject in visualGameObjectArray)
        {
            if(visualGameObject != null)
            {
                visualGameObject.SetActive(isActive);
            }
        }
    }
}