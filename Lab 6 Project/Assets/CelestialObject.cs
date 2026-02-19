using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject celestialObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        celestialObject.name = "Celestial Object";
        celestialObject.transform.position = new Vector3(0f, 8f, 12f);

        Renderer celestialRenderer = celestialObject.GetComponent<Renderer>();
        ColorUtility.TryParseHtmlString("#ffe45e", out Color celestialYellow);
        celestialRenderer.material.SetColor("_Color", celestialYellow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
