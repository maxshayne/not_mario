using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public List<Transform> Transforms;

    private List<Vector3> points;

    private Vector3 curPoint;

    private int index;

    public float speed;

    // Use this for initialization
    void Start()
    {
        //двигаем платформы по точкам и по кругу, т.е. А->B->C->A
        points = new List<Vector3>();
        foreach (var tr in Transforms)
        {
            points.Add(tr.position);
        }
        curPoint = points[0];
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //условие перехода к следующей точке - превышение значений x и y относительно целевой точки
        if (Mathf.Abs(transform.position.x - curPoint.x) > 0.01f || Mathf.Abs(transform.position.y - curPoint.y) > 0.01f)
        {            
            transform.position = Vector3.MoveTowards(transform.position, curPoint, speed * Time.deltaTime);
        }
        else
        { 
            //приехали к последней точке - едем на первую
            index++;
            if (index >= points.Count) index = 0;
            curPoint = points[index];
        }
    }
}