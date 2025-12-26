using UnityEngine;
using TMPro;

public class GuardianDistanceChecker : MonoBehaviour
{
    private Transform arCamera;

    private enum GuardianState
    {
        Safe,
        Warning,
        Breach
    }

    private GuardianState currentState;

    private Renderer r;
    private MaterialPropertyBlock mpb;
    private TextMeshPro warningText;

    private float pulseTimer;
    private float flashTimer;

    void Awake()
    {
        r = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();

        // AUTO-FIND CAMERA (CRITICAL FIX)
        if (Camera.main != null)
            arCamera = Camera.main.transform;
        else
            Debug.LogError("Guardian: AR Camera not found (MainCamera tag missing)");

        // AUTO-FIND TMP (Prefab-safe)
        warningText = GetComponentInChildren<TextMeshPro>();

        if (warningText == null)
            Debug.LogError("Guardian: No TextMeshPro found in children");
    }

    void Update()
    {
        if (arCamera == null || r == null)
            return;

        float distance = Vector3.Distance(arCamera.position, transform.position);
        Debug.Log("Distance to Cube: " + distance);

        if (distance > 0.5f)
            SetState(GuardianState.Safe);
        else if (distance > 0.2f)
            SetState(GuardianState.Warning);
        else
            SetState(GuardianState.Breach);

        UpdateVisuals();
    }

    void SetState(GuardianState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        pulseTimer = 0f;
        flashTimer = 0f;

        UpdateText();
        Debug.Log("Guardian State Changed → " + currentState);
    }

    void UpdateVisuals()
    {
        r.GetPropertyBlock(mpb);

        switch (currentState)
        {
            case GuardianState.Safe:
                mpb.SetColor("_BaseColor", Color.cyan);
                break;

            case GuardianState.Warning:
                pulseTimer += Time.deltaTime * 4f;
                float pulse = (Mathf.Sin(pulseTimer) + 1f) * 0.5f;
                mpb.SetColor("_BaseColor",
                    Color.Lerp(Color.yellow * 0.5f, Color.yellow, pulse));
                break;

            case GuardianState.Breach:
                flashTimer += Time.deltaTime * 10f;
                float flash = Mathf.PingPong(flashTimer, 1f);
                mpb.SetColor("_BaseColor",
                    Color.Lerp(Color.red * 0.2f, Color.red, flash));
                break;
        }

        r.SetPropertyBlock(mpb);
    }

    void UpdateText()
    {
        if (warningText == null)
            return;

        switch (currentState)
        {
            case GuardianState.Safe:
                warningText.text = "SYSTEM ARMED";
                warningText.color = Color.cyan;
                break;

            case GuardianState.Warning:
                warningText.text = "WARNING: RESTRICTED AREA";
                warningText.color = Color.yellow;
                break;

            case GuardianState.Breach:
                warningText.text = "CRITICAL HALT // BACK AWAY";
                warningText.color = Color.red;
                break;
        }
    }
}
