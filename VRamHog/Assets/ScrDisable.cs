using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrDisable : MonoBehaviour
{
    [SerializeField] GameObject vram_manager = null;
    [SerializeField] GameObject hog = null;
    [SerializeField] GameObject filled_toggle = null;

    // Start is called before the first frame update
    void Start()
    {
        ScrVramManager scr_vram_manager = vram_manager.GetComponent<ScrVramManager>();

        // Switch on or off if pressed
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!Application.isEditor)
            {
                // Restart and pass the current requested vram
                string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                // Negative value so it is just passed over but not applied
                System.Diagnostics.Process.Start(path, (-scr_vram_manager.vram_to_hog).ToString() + " " + (filled_toggle.GetComponent<Toggle>().isOn ? "1" : "0"));


                Application.Quit();
            }
            else
            {
                foreach (GameObject quad in hog.GetComponent<HogButton>().quads)
                    Destroy(quad);
            }
        });
    }
}
