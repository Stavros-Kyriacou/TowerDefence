using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private int segments;

    [SerializeField] private Vector2 start;
    [SerializeField] private Vector2 end;

    private void Start()
    {
        Draw(start, end);
    }

    public void Draw(Vector2 start, Vector2 end)
    {
        //get direction of vector
        //normalize to get a 0-1 scale
        //random number between 0 and vector magnitude
        //multiply by the normal

        Vector2 distance = end - start;
        Vector2 direction = distance.normalized;
        float rndNum = Random.Range(0, distance.magnitude);
        Vector2 pointOnLine = rndNum * direction;
        pointOnLine *= Random.insideUnitCircle;

        Line line1 = Instantiate(linePrefab, start, transform.rotation);
        line1.PointA = start;
        line1.PointB = pointOnLine;
        line1.DrawLine();

        Line line2 = Instantiate(linePrefab, pointOnLine, transform.rotation);
        line2.PointA = pointOnLine;
        line2.PointB = end;
        line2.DrawLine();
    }
}
