using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private GameObject LineChild;

    public Vector2 PointA
    {
        get
        {
            return pointA;
        }
        set
        {
            pointA = value;
        }
    }
    public Vector2 PointB
    {
        get
        {
            return pointB;
        }
        set
        {
            pointB = value;
        }
    }
    private void Awake()
    {
        DrawLine();
    }
    public Line(Vector2 pointA, Vector2 pointB)
    {
        this.pointA = pointA;
        this.pointB = pointB;
    }
    public void DrawLine()
    {
        //get the direction and rotation of pointB relative to pointA
        Vector2 direction = pointB - pointA;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //move sprite to pointA and rotate it to face pointB
        LineChild.transform.position = pointA;
        LineChild.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

        //change the x local scale of the sprite
        //gets the percentage of the width of the sprite to the distance between the 2 points. multiply by 100 to make it match the full length
        LineChild.transform.localScale = new Vector3(100 * (direction.magnitude / LineChild.GetComponent<SpriteRenderer>().sprite.rect.width), 1, 1);
    }
}
