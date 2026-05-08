using System;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.XR.ARFoundation;

using UnityEngine.XR.ARSubsystems;



public class MultipleImagesTrackingManager : MonoBehaviour

{

    // Prefab to instantiate when an image is detected

    [SerializeField] List<GameObject> prefabsToInstantiate = new List<GameObject>();


    // Reference to the ARTrackedImageManager component

    private ARTrackedImageManager trackedImageManager;


    // Dictionary to keep track of instantiated prefabs for each tracked image

    private Dictionary<string, GameObject> arObjects;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()

    {

        trackedImageManager = GetComponent<ARTrackedImageManager>();

        if (trackedImageManager == null)

        {

            Debug.LogError("ARTrackedImageManager component not found on the GameObject.");

            return;

        }

        trackedImageManager.trackablesChanged.AddListener(OnImagesTrackedChanged);

        arObjects = new Dictionary<string, GameObject>();

        SetupSceneElements();

    }



    private void OnDestroy()

    {

        trackedImageManager.trackablesChanged.RemoveListener(OnImagesTrackedChanged);

    }



    private void SetupSceneElements()

    {

        foreach (var prefab in prefabsToInstantiate)

        {

            var arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);

            arObject.name = prefab.name;

            arObject.gameObject.SetActive(false);

            arObjects.Add(arObject.name, arObject);

        }

    }


    private void OnImagesTrackedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)

    {

        foreach (var trackedImage in eventArgs.added)

        {

            UpdateARObject(trackedImage);

        }



        foreach (var trackedImage in eventArgs.updated)

        {

            UpdateARObject(trackedImage);

        }



        foreach (var trackedImage in eventArgs.removed)

        {

            //UpdateARObject(trackedImage.Value);

        }

    }


    private void UpdateARObject(ARTrackedImage trackedImage)

    {

        if (trackedImage == null) return;



        if (trackedImage.referenceImage == null)

        {

            Debug.LogWarning($"Tracked image {trackedImage.trackableId} has no reference image assigned.");

            return;

        }



        string imageName = trackedImage.referenceImage.name;



        if (!arObjects.ContainsKey(imageName))

        {

            Debug.LogWarning($"Image name '{imageName}' not found in arObjects dictionary.");

            return;

        }



        GameObject arObject = arObjects[imageName];



        if (trackedImage.trackingState is TrackingState.Tracking)

        {

            arObject.SetActive(true);

            arObject.transform.position = trackedImage.transform.position;

            arObject.transform.rotation = trackedImage.transform.rotation;

        }

        else

        {

            arObject.SetActive(false);

        }

    }



}