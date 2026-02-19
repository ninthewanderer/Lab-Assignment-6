using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    [SerializeField] private int stonesRequired;
    private GameObject[] stones;
    private GameObject[] layers;
    private GameObject pyramidParent;

    // Start is called before the first frame update
    void Start()
    {
        float height = 1;
        //Check that value is at least 3 and no more than 10
        if (stonesRequired < 3 || stonesRequired > 10)
        {
            Debug.LogError("Stones required must be between 3 and 10. Setting to 3");
            stonesRequired = 3;
        }
        stones = new GameObject[stonesRequired];
        layers = new GameObject[stonesRequired];
        // Create layer parent objects
        for (int i = 0; i < stonesRequired; i++)
        {
            GameObject layer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            layer.transform.position = new Vector3(0, height, 0);
            layer.transform.localScale = new Vector3(1, 1, 1);
            layer.name = "Layer " + (i + 1);
            // Disable the mesh renderer to make the layer invisible
            layer.GetComponent<MeshRenderer>().enabled = false;
            layers[i] = layer;
            height++;
            // Debug.Log("Height: " + height);
        }
        height = 1f;
        // Create layers of stones
        while (stonesRequired > 0)
        {
            // Debug.Log("Starting layer with " + stonesRequired + " stones");
            // Create layer in X
            for (int i = 0; i < stonesRequired; i++)
            {
                // create layer in Z
                for (int j = 0; j < stonesRequired; j++)
                {
                    // Create a stone and set its position and scale
                    GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    stone.transform.position = new Vector3(i * 1.1f, height, j * 1.1f);
                    stone.transform.localScale = new Vector3(1, 1, 1);
                    stone.name = "Stone " + (i + 1);
                    stones[i] = stone;
                    // Set the parent of the stone to the current layer
                    stone.transform.parent = layers[(int)height-1].transform;
                }
            }
            // Set the position of the layer to the correct height
            layers[(int)height-1].transform.position = new Vector3(0f, height, 0f);
            stonesRequired--;
            height += 1f;
         }
        // Calculate center of each layer and set the position of the layer to the center
        for (int i = 0; i < layers.Length; i++)
        {
            // Start as 0 and add the position of each stone in the layer to it
            Vector3 center = Vector3.zero;
            foreach (Transform stone in layers[i].transform)
            {
                center += stone.position;
            }
            // Divide the center by the number of stones in the layer to get the average position
            center /= layers[i].transform.childCount;
            // Set the position of the layer to the center
            layers[i].transform.position = new Vector3(-center.x, layers[i].transform.position.y, -center.z);
        }
        pyramidParent = new GameObject("Pyramid");
        // Set the parent of each layer to the pyramid parent

        foreach (GameObject layer in layers)
        {
            Renderer[] renderers = layer.GetComponentsInChildren<Renderer>();
            renderers[0].material.SetColor("_Color", Color.red);
            renderers[1].material.SetColor("_Color", Color.blue);
            renderers[2].material.SetColor("_Color", Color.green);
            layer.transform.parent = pyramidParent.transform;
        }
    }

    void CreatePyramid()
    {
        float height = .5f;
        //Check that value is at least 3 and no more than 10
        if (stonesRequired < 3 || stonesRequired > 10)
        {
            Debug.LogError("Stones required must be between 3 and 10. Setting to 3");
            stonesRequired = 3;
        }
        stones = new GameObject[stonesRequired];
        while (stonesRequired > 0)
        {
            // Crreate layer in X
            for (int i = 0; i < stonesRequired; i++)
            {
                GameObject layer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                layer.transform.position = new Vector3(0, 0, 0);
                layer.transform.localScale = new Vector3(1, 1, 1);
                layer.name = "Layer " + (i + 1);   
                layer.GetComponent<MeshRenderer>().enabled = false;
                layers[i] = layer;
                // create layer in Z
                for (int j = 0; j < stonesRequired; j++)
                {
                    GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    stone.transform.position = new Vector3(i * 1.1f, height, j * 1.1f);
                    stone.transform.localScale = new Vector3(1, 1, 1);
                    stone.name = "Stone " + (i + 1);
                    stones[i] = stone;
                    stone.transform.parent = layers[i].transform;
                }

            }
            stonesRequired--;
            height += 1f;
        }

    }

}
