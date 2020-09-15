using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrFps : MonoBehaviour
{
    public int frames_to_count = 10;
    Text text_obj;
    double duration;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        text_obj = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;
        counter++;

        if (counter == frames_to_count)
        {
            duration /= frames_to_count;

            text_obj.text = System.Math.Round((1.0f / duration), 2) +" FPS";

            duration = 0;
            counter = 0;
        }
    }
}
