using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private int segments;
    [SerializeField] private Color colour;
    [SerializeField] private Vector2 startPoint;
    [SerializeField] private Vector2 endPoint;
    [SerializeField] private float minSpread;
    [SerializeField] private float maxSpread;
    [SerializeField] private float updateRate;
    private List<GameObject> lines;
    private List<Vector2> randomLocations;

    public Vector2 StartPoint
    {
        get
        {
            return startPoint;
        }
        set
        {
            startPoint = value;
        }
    }
    public Vector2 EndPoint
    {
        get
        {
            return endPoint;
        }
        set
        {
            endPoint = value;
        }
    }
    private void Awake()
    {
        Initialise();
    }
    private void OnEnable() {
        InvokeRepeating("Activate", 0f, updateRate);
    }

    public void Initialise()
    {
        lines = new List<GameObject>();

        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //instatiates the lines, parents them to this object, all lines get stored in a list
        for (int i = 0; i < segments; i++)
        {
            GameObject line = Instantiate(linePrefab);
            line.transform.parent = transform;
            lines.Add(line);
        }
    }
    public void Activate()
    {
        randomLocations = GetRandomPointsOnLine(startPoint, endPoint);

        Vector2 currentPoint;
        Vector2 nextPoint;

        for (int i = 0; i < randomLocations.Count - 1; i++)
        {
            //get the current point and next point
            currentPoint = randomLocations[i];
            nextPoint = randomLocations[i + 1];

            //get the line component
            Line lineComponent = lines[i].GetComponent<Line>();
            lineComponent.GetComponentInChildren<SpriteRenderer>().color = colour;

            //set its start and end points then draw it
            lineComponent.PointA = currentPoint;
            lineComponent.PointB = nextPoint;
            lineComponent.DrawLine();
        }
    }
    public List<Vector2> GetRandomPointsOnLine(Vector2 start, Vector2 end)
    {
        List<Vector2> vectors = new List<Vector2>();
        List<float> points = new List<float>();

        //get the direction of the slope and its normalized direction
        Vector2 direction = end - start;
        Vector2 normalDir = direction.normalized;

        //get random points on the line, store them in a list
        for (int i = 0; i < segments - 1; i++)
        {
            float randomPoint = Random.Range(0, direction.magnitude);
            points.Add(randomPoint);
        }

        //so that lines can be drawn from closest to the start to the furthest
        points.Sort();

        //getting a list of vector2s that have randomness added to create the jagged lines
        //add the start location so that it is first, end location gets added after the random points are added
        vectors.Add(start);

        for (int i = 0; i < points.Count; i++)
        {
            //converts the random point on the line to a Vector2, point * normalDirection(length is always 1)
            Vector2 convertedPoint = points[i] * normalDir;

            //get the normalalized perpendicular left vector of the line
            Vector2 perpendicular = Vector2.Perpendicular(direction).normalized;

            if (i % 2 == 0)
            {
                perpendicular = -perpendicular;
            }

            //scale the perpendicular vector by the spread and add it to the points
            convertedPoint += start + (perpendicular * Random.Range(minSpread, maxSpread));

            vectors.Add(convertedPoint);
        }

        vectors.Add(end);

        return vectors;
    }
}
