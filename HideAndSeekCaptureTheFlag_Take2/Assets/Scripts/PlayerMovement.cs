using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player;
    public Transform playerBase;
    public LayerMask Ignore;

    private MeshRenderer[] playerChildMeshes;

    void Start()
    {
        playerChildMeshes = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        GameObject[] lightSources = GameObject.FindGameObjectsWithTag("MainLightSource");
        CheckInvisible(lightSources);
        lightSources = GameObject.FindGameObjectsWithTag("LightSource");
        CheckInvisible(lightSources);
    }

    private void CheckInvisible(GameObject[] lightSources)
    {
        RaycastHit hit;
        
        foreach (GameObject light in lightSources)
        {
            Vector3 directionToLight = light.transform.forward * -1;
            if (Physics.Raycast(playerBase.position, directionToLight, out hit, Mathf.Infinity, ~Ignore))//, 9))
            {
                MakeVisible(false);
            }
            else
            {
                MakeVisible(true);
            }
        }
    }

    private void MakeVisible(bool shouldBeVisble)
    {
        foreach (MeshRenderer mr in playerChildMeshes)
        {
            mr.enabled = shouldBeVisble;
        }
    }
}
