using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform light;
    public GameObject playerGraphics;

    public float speed = 5f;

    //private Rigidbody rb;
    private MeshRenderer playerMesh;
    private MeshRenderer[] playerChildMeshes;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        playerMesh = playerGraphics.GetComponent<MeshRenderer>();
        playerChildMeshes = playerGraphics.GetComponentsInChildren<MeshRenderer>();
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //
        //rb.AddForce(movement * speed);

        CheckInvisible();
    }

    private void CheckInvisible()
    {

        Vector3 lightRot = light.forward;
        Vector3 directionToLight = light.forward * -1;

        RaycastHit hit;

        Debug.DrawRay(transform.position, directionToLight, Color.red);

        if (Physics.Raycast(transform.position, directionToLight, out hit, Mathf.Infinity))
        {
            MakeVisible(false);
        }
        else
        {
            MakeVisible(true);
        }
    }

    private void MakeVisible(bool shouldBeVisble)
    {
        playerGraphics.GetComponent<MeshRenderer>().enabled = shouldBeVisble;
        foreach (MeshRenderer mr in playerChildMeshes)
        {
            mr.enabled = shouldBeVisble;
        }
    }
}
