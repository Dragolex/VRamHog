# VRamHog

Less scientifically written post about this in the German forum I developed this for:
https://www.hardwareluxx.de/community/threads/vramhog-software-zum-finden-des-vram-limits.1277261/

# Purpose

In many computer hardware forums, a recurring theme is the question, **how much video memory (VRam) does software and especially games truly need?**
The answer to the question can be significant for buying decisions and recomendations.
Once more this topic boiled up with the annoucnement of the Nvidia RTX 3080 of which some people claim it's 10Gb are not future proof, while NV itself claims current games don't require more than 6Gb.

A common hypothesis you hear is: "GPU-Z / MSI Afterburner (two softwares for GPU monitoring) show that my VRam is already fully occupied when I play. That means I need more VRam in the near future!"
A counter hypothesis to that is: "The VRam acts merely as a cache. Data is written and overwritten but rarely cleared from it. Therefore this cache appears full despite not being needed entirely."

If the later hypothesis is true, that means the amount of occupied VRam during a game is nearly useless for concliding how much VRam that game truly would need to be perceived as running smooth.

# Idea of the VRamHog
Now I thought about this and wondered: What if there were a way to simulate how a game would run if it hat less VRam?
Then you could test step by step until you find stutters, FPS drops increase or other general performance degeneration.
**That way you can determine the actual VRam requirement.**

This is not really something intended by the GPU manufacturers and is not directly suported by their drivers. So instead my concept goes a more direct way:
**Textures optionally filled with random data, are allocated and rendered on tiny squares on the screen.**

Having them actually render, yet greatly scaled down has the effect that the GPU cannot ommit the textures in any way and yet there is hardly any GPU computation load (which would benchmarks) because only a handful of pixels need to be rendered.

# Where to downlaod a pre-built version
Ready for Windows: https://www.dropbox.com/s/6tz6qyhicpue6cz/VRamHog_v2.zip?dl=0
Note: Unpack before running!

# How to build the VRamHog

The sourcecode here here is an Unity repository.
1. Download Unity Hub: https://unity.com/
2. Add an installation of Unity 2020.1.2f1 (new will probably work, but if you want to go sure use the original)
3. Open the cloned project with it

Note that to build the standalone version yourself it's important to build a 64 bit version or there are crashes when requesting more than 4Gb!

# How to use
1. Prepare your benching system as usual.
2. Start the software and chose an amount of VRam to begin with, using the "+" and "-" buttons)
3. Click "APPLY" and verify that approximately the desired amount of VRam is occupied (the number might be lower by a few percent because the GPU seems to kick out old textures from other programs)
4. Start the bench and pay most attention on scene loading times and stutters that appear to be caused by texture loading.
5. Increase the amount of occupied VRam (clicking apply again) and continue with step 4.

Note: Don't be surprised that the Unity loadings creen reappears when clicking "APPLY". To ensure that the VRam is actually entirely given back, the software restarts itself (apparently otherwise the GPU drivers keep track of the allocating process and keep some ram occupied even if the texture is actively deleted).

The "DISABLE" button frees the VRram.
The toggle "FILLED" causes that textures are actually filled with random data (instead of left empty). This is meant to counter possible compression attempts by the GPU. Occupying VRam that way is slower however.
