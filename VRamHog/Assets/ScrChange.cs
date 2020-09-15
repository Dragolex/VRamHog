using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrChange : MonoBehaviour
{
    [SerializeField] GameObject vram_manager = null;

    [SerializeField] int value = 0;

    // Start is called before the first frame update
    void Start()
    {
        ScrVramManager scr_vram_manager = vram_manager.GetComponent<ScrVramManager>();

        Button button = GetComponent<Button>();

        // Switch on or off if pressed
        button.onClick.AddListener(() => {
            scr_vram_manager.vram_to_hog += value;
            scr_vram_manager.vram_to_hog = Math.Max(0, scr_vram_manager.vram_to_hog);
        });
    }
}
