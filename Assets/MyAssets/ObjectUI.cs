using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEditor.XR.ARSubsystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ObjectUI : MonoBehaviour
{
    [SerializeField]
    List<Sprite> objectIcons = new List<Sprite>();

    [SerializeField]
    GameObject[] objectPrefabs;

    public Image objectImage;
    public ObjectSpawner objectSpawner;

    public ARTrackedImageManager trackedImageManager;

    public int currentObjectIndex = 0;


    private void Start()
    {
        if(objectSpawner == null)
        {
            objectSpawner = GameObject.FindGameObjectWithTag("ObjectSpawner").GetComponent<ObjectSpawner>();
        }

        // Dodanie prefabów i obrazków obiektów do menu
        objectPrefabs = Resources.LoadAll<GameObject>("ObjectPrefabs");
        foreach(GameObject obj in objectPrefabs)
        {
            objectIcons.Add(Resources.Load<Sprite>($"ObjectIcons/{obj.name}"));
        }

        // Ustawienie defaultowego obrazka
        if(objectIcons.Count() != 0)
        {
            objectImage.sprite = objectIcons[currentObjectIndex];
        }
    }

    // Wywo³ane przy wciœniêciu przycisku > w ObjectMenu
    public void NextObjectImage()
    {
        currentObjectIndex++;
        if(objectPrefabs.Length < currentObjectIndex)
        {
            return;
        }
        if (objectIcons != null && currentObjectIndex < objectIcons.Count())
        {
            objectImage.sprite = objectIcons[currentObjectIndex];
            //trackedImageManager.trackedImagePrefab = objectPrefabs[currentObjectIndex];

            // Tutaj ustawia³o index spawnowanego obiektu w object spawnerze we wczeœniejszej wersji
            // (jak spawnowanie myszk¹ dzia³a³o), aktualnie nic nie robi
            objectSpawner.SetSpawnObjectIndex(currentObjectIndex);
            
        }
        else
        {
            currentObjectIndex--;
        }
    }

    // Wywo³ane przy wciœniêciu przycisku < w ObjectMenu
    public void PreviousObjectImage()
    {
        currentObjectIndex--;
        if (currentObjectIndex >= 0 && objectIcons != null && objectIcons.Count() > 0)
        {
            objectImage.sprite = objectIcons[currentObjectIndex];

            // Tutaj ustawia³o index spawnowanego obiektu w object spawnerze we wczeœniejszej wersji
            // (jak spawnowanie myszk¹ dzia³a³o), aktualnie nic nie robi
            objectSpawner.SetSpawnObjectIndex(currentObjectIndex);

        }
        else
        {
            currentObjectIndex++;
        }
    }
}
