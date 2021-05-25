using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private int segments;

    [SerializeField] private Vector2 start;
    [SerializeField] private Vector2 end;
    private List<GameObject> lines;
    private List<Vector2> randomLocations;
    private void Start()
    {
        InvokeRepeating("Initialise", 0f, .1f);
        // Initialise();
    }

    public void Initialise()
    {
        foreach(Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        randomLocations = GetRandomPointsOnLine(start, end);

        //instantiates the lines parented to the lightning bolt, stores them in a list
        lines = new List<GameObject>();

        for(int i = 0; i < segments; i++)
        {
            GameObject line = Instantiate(linePrefab);
            line.transform.parent = transform;
            // line.SetActive(false);
            lines.Add(line);
        }
        Activate();
    }
    public void Activate()
    {
        Vector2 currentPoint;
        Vector2 nextPoint;
        
        for (int i = 0; i < randomLocations.Count - 1; i++)
        {
            currentPoint = randomLocations[i];
            nextPoint = randomLocations[i + 1];
            Line lineComponent = lines[i].GetComponent<Line>();
            lineComponent.PointA = currentPoint;
            lineComponent.PointB = nextPoint;
            lineComponent.DrawLine();
        }
    }
    public List<Vector2> GetRandomPointsOnLine(Vector2 start, Vector2 end)
    {
        List<Vector2> vectors = new List<Vector2>();
        List<float> points = new List<float>();

        Vector2 distance = end - start;
        Vector2 normal = distance.normalized;

        for (int i = 0; i < segments - 1; i++)
        {
            float randomPoint = Random.Range(0, distance.magnitude);
            Debug.Log("Unsorted Random point on line: " + randomPoint);
            points.Add(randomPoint);
        }

        points.Sort();

        foreach (float f in points)
        {
            Debug.Log("Sorted Random point on line: " + f);
        }

        vectors.Add(start);
        for (int i = 0; i < points.Count; i++)
        {
            Vector2 convertedPoint = points[i] * normal;
            Debug.Log("Point * normal: " + convertedPoint);

            convertedPoint += Random.insideUnitCircle * (distance.magnitude / 4) ;
            Debug.Log("Converted point + random unit circle: " + convertedPoint);

            vectors.Add(convertedPoint);
        }
        vectors.Add(end);

        return vectors;
    }
}
