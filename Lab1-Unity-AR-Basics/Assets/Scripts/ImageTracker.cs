using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject spoonPrefab;
    public GameObject shovelPrefab;

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnObjectForTrackedImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateObjectForTrackedImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                Destroy(spawnedObjects[trackedImage.referenceImage.name]);
                spawnedObjects.Remove(trackedImage.referenceImage.name);
            }
        }
    }

    void SpawnObjectForTrackedImage(ARTrackedImage trackedImage)
    {
        GameObject prefabToSpawn = null;

        // Определяем какой объект создавать для каждого маркера
        if (trackedImage.referenceImage.name == "Marker1")
        {
            prefabToSpawn = spoonPrefab;
        }
        else if (trackedImage.referenceImage.name == "Marker2")
        {
            prefabToSpawn = shovelPrefab;
        }

        if (prefabToSpawn != null && !spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject newObject = Instantiate(prefabToSpawn, trackedImage.transform);
            spawnedObjects[trackedImage.referenceImage.name] = newObject;
        }
    }

    void UpdateObjectForTrackedImage(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject objectToUpdate = spawnedObjects[trackedImage.referenceImage.name];
            
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                objectToUpdate.transform.position = trackedImage.transform.position;
                objectToUpdate.transform.rotation = trackedImage.transform.rotation;
                objectToUpdate.SetActive(true);
            }
            else
            {
                objectToUpdate.SetActive(false);
            }
        }
    }
}