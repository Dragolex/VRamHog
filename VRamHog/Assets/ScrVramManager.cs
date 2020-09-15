using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrVramManager : MonoBehaviour
{
    public int vram_to_hog = 0;

    Text text_obj;

    void Start()
    {
        text_obj = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text_obj.text = vram_to_hog + "Mb";
    }
}
