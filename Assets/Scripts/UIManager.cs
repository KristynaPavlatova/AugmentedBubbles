using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject instractionsUI;

    private void Start()
    {
        BubbleSpawner.VeryFirstBubbleSpawned += HideInstructionsUI;
    }
    public void ExitApp()
    {
        Application.Quit();
    }

    public void HideInstructionsUI()
    {
        instractionsUI.SetActive(false);
    }
}
