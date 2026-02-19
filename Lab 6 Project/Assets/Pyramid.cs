using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    [SerializeField] private int stonesRequired;
    private GameObject[] stones;
    private GameObject[] layers;

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
        // height
        for (int i = 0; i < stonesRequired; i++)
        {
            GameObject layer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            layer.transform.position = new Vector3(0, height, 0);
            layer.transform.localScale = new Vector3(1, 1, 1);
            layer.name = "Layer " + (i + 1);
            layer.GetComponent<MeshRenderer>().enabled = false;
            layers[i] = layer;
            height++;
            Debug.Log("Height: " + height);
        }
        height = 1f;
        while (stonesRequired > 0)
        {
            Debug.Log("Starting layer with " + stonesRequired + " stones");
            // Create layer in X
            for (int i = 0; i < stonesRequired; i++)
            {
                // create layer in Z
                for (int j = 0; j < stonesRequired; j++)
                {
                    GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    stone.transform.position = new Vector3(i * 1.1f, height, j * 1.1f);
                    stone.transform.localScale = new Vector3(1, 1, 1);
                    stone.name = "Stone " + (i + 1);
                    stones[i] = stone;
                    stone.transform.parent = layers[(int)height-1].transform;
                }
            }
            layers[(int)height-1].transform.position = new Vector3(0, height, 0);
            stonesRequired--;
            height += 1f;
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
