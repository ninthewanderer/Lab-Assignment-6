using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateForest : MonoBehaviour
{
    // The size of the forest that will be generated.
    public int forestSize;

    // Start is called before the first frame update
    void Start()
    {
        // Creates an empty GameObject that will store the trees generated in the Hierarchy.
        GameObject forest = new GameObject("Forest");

        // Randomly generates trees based on the int forestSize provided.
        for (int i = 0;  i < forestSize; i++)
        {
            // The trees are represented as cylinders with varied heights.
            GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tree.transform.localScale = new Vector3(0.25f, Random.Range(0.25f, 1f), 0.25f);

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
