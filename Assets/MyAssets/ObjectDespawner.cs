using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Templates.MR;

public class ObjectDespawner : MonoBehaviour
{
    public SpawnedObjectsManager spawnedObjectsManager;
    InputAction tapCountAction;
    [SerializeField]
    InputAction tapAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //tapCountAction = InputSystem.actions.FindAction("Screen Touch Count");
        tapAction = InputSystem.actions.FindAction("Click");
        //tapAction.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        if(tapAction.IsPressed())
        {
            print("Touch detected ");
        }
        /*if(tapCountAction.ReadValue<int>() == 2)
        {
            spawnedObjectsManager.OnDestroyObjectsButtonClicked();
        }*/
    }
}
