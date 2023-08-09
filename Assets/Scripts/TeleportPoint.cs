using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPoint : MonoBehaviour
{   
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas;
    
    [SerializeField] public Transform teleportLocation;
    
    // Start is called before the first frame update
    void Awake()
    {
        canvas.SetActive(false);
    }

    public Transform GetTeleportLocation()
    {
        return teleportLocation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<CharacterController>(out CharacterController Controller))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<CharacterController>(out CharacterController Controller))
        {
            canvas.SetActive(false);
        }
    }
}
