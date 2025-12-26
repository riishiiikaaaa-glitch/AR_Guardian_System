using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceCube : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Material debugMaterial; // optional

    private ARRaycastManager raycastManager;
    private GameObject spawnedCube;
    private static readonly List<ARRaycastHit> hits = new();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Only allow one cube
        if (spawnedCube != null)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // Create anchor (locks to real world)
            GameObject anchorObj = new GameObject("GuardianAnchor");
            anchorObj.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            anchorObj.AddComponent<ARAnchor>();

            // Spawn cube as child of anchor
            spawnedCube = Instantiate(cubePrefab, anchorObj.transform);

            // 10cm cube, sitting on plane
            spawnedCube.transform.localScale = Vector3.one * 0.1f;
            spawnedCube.transform.localPosition = Vector3.up * 0.05f;

            // Add Guardian logic
            // Add Guardian logic (camera auto-resolves internally)
            spawnedCube.AddComponent<GuardianDistanceChecker>();


            // Optional debug material override
            if (debugMaterial != null)
            {
                Renderer r = spawnedCube.GetComponent<Renderer>();
                if (r != null)
                    r.material = debugMaterial;
            }

            Debug.Log("[TapToPlaceCube] Guardian cube anchored successfully");
        }
    }
}