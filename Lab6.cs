using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab6 : MonoBehaviour
{
    // Pyramid:
    [SerializeField] private int stonesRequired;
    private GameObject[] stones;
    private GameObject[] layers;
    private GameObject pyramidParent;

    // Forest:
    // The size of the forest that will be generated.
    public int forestSize;

    //Celestial Object:
    public bool daytime;
    private bool previousValue;

    private GameObject celestialObject;

    void Start()
    {
        InitializeVariables();
        CreateGround();
        CreatePyramid();
        CreateRandomForest();
        CreateCelestialObject();
    }

    private void Update()
    {
        // Rotate the celestial object around the Y-axis
        if (celestialObject != null)
        {
            celestialObject.transform.Rotate(Vector3.right, 20 * Time.deltaTime);
        }
        // Check if the value of daytime has changed and update the celestial object if it has
        if (previousValue != daytime)
        {
            previousValue = daytime;
            UpdateCelestialObject();
        }
    }

    void InitializeVariables()
    {
        //Check that value of stones is at least 3 and no more than 10
        if (stonesRequired < 3 || stonesRequired > 10)
        {
            Debug.LogError("Stones required must be between 3 and 10. Setting to 3");
            stonesRequired = 3;
        }
        // Check that value of forest size is at least 1
        if (forestSize < 1)
        {
            Debug.LogError("Forest size must be at least 1. Setting to 1");
            forestSize = 1;
        }
    }

    void CreateGround()
    {
        {
            // Creates the ground at the middle of the scene using a simple plane.
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0, 0, 0);
            ground.transform.localScale = Vector3.one * 2;

            // Gets the Renderer component from the ground.
            Renderer groundRenderer = ground.GetComponent<Renderer>();

            // Creates & sets the color for the ground.
            ColorUtility.TryParseHtmlString("#986eba", out Color groundColor);
            groundRenderer.material.SetColor("_Color", groundColor);
        }
    }

    // Create initial celestial object and add light component to it
    void CreateCelestialObject()
    {
        {
            celestialObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            celestialObject.name = "Celestial Object";
            celestialObject.transform.position = new Vector3(0f, 8f, 12f);
            // Add the light component
            Light lightComp = celestialObject.AddComponent<Light>();
            UpdateCelestialObject();

        }
    }

    // Update the celestial object's color and light intensity based on the time of day
    void UpdateCelestialObject()
    {
        if (celestialObject != null)
        {
            Light lightComp = celestialObject.GetComponent<Light>();
            lightComp.intensity = 10;  
            Renderer celestialRenderer = celestialObject.GetComponent<Renderer>();
            if (daytime)
            {
                ColorUtility.TryParseHtmlString("#ffe45e", out Color celestialYellow);
                celestialRenderer.material.SetColor("_Color", celestialYellow);
                lightComp.color = Color.yellow;
            }
            else
            {
                ColorUtility.TryParseHtmlString("#1e5262", out Color celestialBlue);
                celestialRenderer.material.SetColor("_Color", celestialBlue);
                lightComp.color = Color.blue;
            }
        }
    }
    void CreateRandomForest()
    {
        {
            // Creates an empty GameObject that will store the trees generated in the Hierarchy.
            GameObject forest = new GameObject("Forest");

            // Randomly generates trees based on the int forestSize provided.
            for (int i = 0; i < forestSize; i++)
            {
                // The trees are represented as cylinders with varied heights.
                GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tree.name = "Tree " + (i + 1);
                tree.transform.localScale = new Vector3(Random.Range(0.25f, 1f), Random.Range(0.25f, 1f), Random.Range(0.25f, 1f));

                // The more trees there are, the closer together they are.
                if (forestSize <= 5)
                {
                    tree.transform.position = new Vector3(Random.Range(-9f, -3f), (0 + tree.transform.localScale.y), Random.Range(-9f, -3f));
                }
                else
                {
                    tree.transform.position = new Vector3(Random.Range(-8f, -4f), (0 + tree.transform.localScale.y), Random.Range(-8f, -4f));
                }

                // Every even-numbered tree will be a different color.
                Renderer groundRenderer = tree.GetComponent<Renderer>();
                if (i % 2 == 0)
                {
                    groundRenderer.material.SetColor("_Color", Color.green);
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#068c32", out Color darkGreen);
                    groundRenderer.material.SetColor("_Color", darkGreen);
                }

                // Stores all trees under the "Forest" GameObject.
                tree.transform.SetParent(forest.transform);
            }
        }
    }
    void CreatePyramid()
    {
        {
            float height = 1;
            
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
                        stone.transform.parent = layers[(int)height - 1].transform;
                    }
                    // Set the color of the layer to a random color
                    Renderer[] renderer = layers[(int)height - 1].GetComponentsInChildren<Renderer>();
                    Renderer layerRenderer = layers[(int)height - 1].GetComponent<Renderer>();
                    layerRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                    foreach (Renderer r in renderer)
                    {
                        // Set the color of the stone to the same color as the layer
                        r.material.color = layerRenderer.material.color;
                    }
                }
                // Set the position of the layer to the correct height
                layers[(int)height - 1].transform.position = new Vector3(0f, height, 0f);
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
                layer.transform.parent = pyramidParent.transform;
            }
        }
    }
}
