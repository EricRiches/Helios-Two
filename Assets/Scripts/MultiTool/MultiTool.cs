using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiTool : MonoBehaviour
{

    [SerializeField] Light flashlight_light;

    public static MultiTool instance;
    public static Flashlight flashlight = new();
    public static Sonic_Burst sonic_Burst = new();
    public static Freeze_Spray freeze_Spray = new();
    public static Hack_Panel hack_Panel = new();

    public EventReference flashlightSound;
    public static Dictionary<Tool, Tool_MultiTool> tools = new Dictionary<Tool, Tool_MultiTool>();
    [SerializeField]LayerMask flashlightRaycastLayermask;
    public float arcDegrees = 10f;
    public Tool_MultiTool currentTool;

    public bool canUseTool = true;

   



    public float maxIntensity = 5f; // Default maximum intensity
    public float minIntensity = 0.5f; // Minimum intensity when very close
    public int raycastResolution = 5; // Number of rays in a grid (5x5)
    public float maxDistance = 10f;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        tools[Tool.Flashlight] = flashlight;
        flashlight.SetLight(flashlight_light);
        tools[Tool.Sonic_Burst] = sonic_Burst;
        tools[Tool.Freeze_Spray] = freeze_Spray;
        tools[Tool.Hack_Panel] = hack_Panel;

    }

    public void SwitchTool(Tool toolType)
    {
        if (tools.TryGetValue(toolType, out Tool_MultiTool tool) && tool.IsUnlocked)
        {
            currentTool?.OnSwapOff();
            currentTool = tool;

        }
    }



    public void SetCanUseTool(bool value) { canUseTool = value; }



    public void UnlockFlashlight() { flashlight.SetUnlocked(true); }
    public void UnlockSonicBurst() { sonic_Burst.SetUnlocked(true); }
    public void UnlockFreezeSpray() { freeze_Spray.SetUnlocked(true); }
    public void UnlockHackPanel() { hack_Panel.SetUnlocked(true); }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!canUseTool || currentTool == null) return;
            currentTool.UseTool();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchTool(Tool.Flashlight);
        }

    }

    private void FixedUpdate()
    {
        if (flashlight.isOn)
        {
            float intensityMultiplier = CalculateIntensityMultiplier();

            // Adjust the light intensity using the multiplier
            flashlight.SetIntensity(maxIntensity * intensityMultiplier);
        }
    }
    private float CalculateIntensityMultiplier()
    {
        float closestDistance = float.MaxValue;
        Camera camera = Camera.main;

        // Set the total arc degrees (this can be modified as needed)
         // Modify this value to change the total arc (e.g., 60 for 60 degrees total)
        float halfFov = arcDegrees / 2f;  // Calculate the half field of view

        int halfResolution = raycastResolution / 2;
        int quartResolution = halfResolution / 2;

        // Raycast a grid around the camera viewport
        for (int x = -halfResolution; x < halfResolution; x++)
        {
            for (int y = -quartResolution; y < quartResolution; y++)
            {
                // Calculate the angular offset for the x-axis (spread rays across the horizontal arc)
                float angleOffsetX = (x / (float)raycastResolution) * arcDegrees;// - halfFov; // Spread rays from -halfFov to +halfFov

                // Calculate the angular offset for the y-axis (spread rays across the vertical arc)
                float angleOffsetY = (y / (float)raycastResolution) * arcDegrees;// - halfFov; // Similar vertical offset

                // Calculate the direction for this ray in world space
                Vector3 direction = camera.transform.forward
                                    + camera.transform.right * Mathf.Tan(Mathf.Deg2Rad * angleOffsetX)
                                    + camera.transform.up * Mathf.Tan(Mathf.Deg2Rad * angleOffsetY);

                // Cast the ray in this direction
                Ray ray = new Ray(camera.transform.position, direction.normalized);

                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, flashlightRaycastLayermask))
                {
                    // Track the closest distance
                    closestDistance = Mathf.Min(closestDistance, hit.distance);
                }
            }
        }

        // If no objects are detected, return maximum multiplier (1.0)
        if (closestDistance == float.MaxValue)
        {
            return 1f;
        }

        // Calculate the multiplier based on the closest distance
        return Mathf.Lerp(minIntensity / maxIntensity, 1f, closestDistance / maxDistance);
    }

}

public enum Tool
{
    Flashlight,
    Sonic_Burst,
    Freeze_Spray,
    Hack_Panel,
}