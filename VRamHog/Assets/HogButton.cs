using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HogButton : MonoBehaviour
{
    [SerializeField] public GameObject quad_prefab;
    [SerializeField] public Shader shader;

    [SerializeField] GameObject vram_manager = null;
    [SerializeField] GameObject filled_toggle = null;

    ScrVramManager scr_vram_manager;


    public List<GameObject> quads = new List<GameObject>();

    bool already_applied = false;

    void Start()
    {
        // Lock frame rate to 30fps to keep GPU laod minimal
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        string[] args = System.Environment.GetCommandLineArgs();

        scr_vram_manager = vram_manager.GetComponent<ScrVramManager>();
        

        if ((args.Length >= 2) && (args[1] != "0"))
        {
            int loaded_val = 0;

            try
            {
                loaded_val = int.Parse(args[1]);
            }
            catch (Exception) {}

            scr_vram_manager.vram_to_hog = Math.Abs(loaded_val);

            try
            {
                if (int.Parse(args[2]) == 1)
                    filled_toggle.GetComponent<Toggle>().isOn = true;
            }
            catch (Exception) {}

            if (loaded_val > 0)
                Enable();
        }


        // Switch on or off if pressed
        GetComponent<Button>().onClick.AddListener(() => {

            if (already_applied)
            {
                if (!Application.isEditor)
                {
                    // Restart and pass the current requested vram
                    string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    System.Diagnostics.Process.Start(path,
                        scr_vram_manager.vram_to_hog.ToString() + " " + (filled_toggle.GetComponent<Toggle>().isOn ? "1" : "0"));

                    Application.Quit();
                }
                else
                {
                    foreach (GameObject quad in quads)
                        Destroy(quad);
                    Enable();
                }
            }
            else
            {
                Enable();
            }
        });
    }


    public void Enable()
    {
        already_applied = true;

        long bytes = ((long)scr_vram_manager.vram_to_hog) * 1000000;
        long bytes_per_pixel = 4;
        long total_pixels = bytes / bytes_per_pixel;

        List<Texture> textures = new List<Texture>();

        // Do fill the textures or not
        if (filled_toggle.GetComponent<Toggle>().isOn)
        {
            Debug.Log("FIlled!");

            // Adds as many textures of that size as fit in total_pixels
            total_pixels = AddFittingTexturesRandomFilled(textures, 4096, total_pixels);
            total_pixels = AddFittingTexturesRandomFilled(textures, 1024, total_pixels);
            total_pixels = AddFittingTexturesRandomFilled(textures, 256, total_pixels);
            total_pixels = AddFittingTexturesRandomFilled(textures, 64, total_pixels);
        }
        else
        {
            Debug.Log("Not FIlled!");

            // Adds as many textures of that size as fit in total_pixels
            total_pixels = AddFittingTexturesEmpty(textures, 4096, total_pixels);
            total_pixels = AddFittingTexturesEmpty(textures, 1024, total_pixels);
            total_pixels = AddFittingTexturesEmpty(textures, 256, total_pixels);
            total_pixels = AddFittingTexturesEmpty(textures, 64, total_pixels);
        }

        int i = 0;
        int count = textures.Count;
        foreach (Texture texture in textures)
        {
            GameObject quad = MonoBehaviour.Instantiate(quad_prefab);
            quad.transform.position = new Vector3((-count/2 + i) * 0.5f, -5f, 0);

            MeshRenderer renderer = quad.GetComponent<MeshRenderer>();

            // Ensure that this texture stays used by applying it to one of the quads
            Material material = new Material(shader);
            material.mainTexture = texture;
            renderer.material = material;

            quads.Add(quad);

            i++;
        }
    }


    long AddFittingTexturesRandomFilled(List<Texture> textures, int size, long total_pixels)
    {
        long pixels = size * size;

        Color32[] colors = new Color32[pixels];
        System.Random rnd = new System.Random();

        for (int i = 0; i < pixels; i++)
        {
            colors[i] = new Color32(
              (byte)rnd.Next(0, 255),
              (byte)rnd.Next(0, 255),
              (byte)rnd.Next(0, 255),
              (byte)rnd.Next(0, 255)
          );
        }

        while (total_pixels >= pixels)
        {
            // Create a texture of the needed size and send it to the GPU
            Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);

            // Set one random pixel to a different color to ensure that the GPU driver doesn't somehow recognize identical textures.
            colors[rnd.Next(0, size) * rnd.Next(0, size)] = Color.blue;
            texture.SetPixels32(colors);

            textures.Add(texture);

            total_pixels -= pixels;
        }

        return (total_pixels);
    }

    long AddFittingTexturesEmpty(List<Texture> textures, int size, long total_pixels)
    {
        long pixels = size * size;

        while (total_pixels >= pixels)
        {
            // Create a texture of the needed size and send it to the GPU
            Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
            textures.Add(texture);

            total_pixels -= pixels;
        }

        return (total_pixels);
    }

}
