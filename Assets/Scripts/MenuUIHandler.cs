using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // Assign color to persistent main manager instance
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();

        // This will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        // Assign the saved color to the units
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    // Gets called when the start button is pressed
    public void StartNew()
    {
        SceneManager.LoadScene(1); // Scene index can be found under File > Build Settings
    }

    // Gets called when the exit button is pressed
    public void Exit()
    {
        // Save the selected color
        MainManager.Instance.SaveColor();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // Does only work for the built application
#endif
    }

    // Testing functions for the saving and loading of the color
    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
