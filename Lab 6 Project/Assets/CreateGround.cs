using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Creates the ground at the middle of the scene using a simple plane.
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = new Vector3(0, 0, 0);

        // Gets the Renderer component from the ground.
        Renderer groundRenderer = ground.GetComponent<Renderer>();

        // Creates & sets the color for the ground.
        ColorUtility.TryParseHtmlString("#986eba", out Color groundColor);
        groundRenderer.material.SetColor("_Color", groundColor);
    }
}
