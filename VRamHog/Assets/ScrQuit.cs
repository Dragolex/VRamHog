using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrQuit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();

        // Switch on or off if pressed
        button.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}