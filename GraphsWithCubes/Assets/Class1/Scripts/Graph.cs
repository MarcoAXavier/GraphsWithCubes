using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private Transform _pointPrefab;
    private Transform PointPrefab => _pointPrefab;

    [SerializeField, Range(10, 100)]
    private int _resolution = 10;
    private int Resolution => _resolution;


    [SerializeField]
    private FunctionLibrary.FunctionName _function = 0;
    private FunctionLibrary.FunctionName Function => _function;


    Transform[] Points { get; set; }

    private void Awake ()=> PositionAndScalePoints();

    private void Update() => UpdatePointsPosition();

    private void PositionAndScalePoints()
    {
        Points = new Transform[Resolution * Resolution];
        float step = (float)2 / Resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 pos = Vector3.zero;

        for (int i = 0, x = 0, z = 0; i < Points.Length; i++, x++)
        {
            if (x == Resolution)
            {
                x = 0;
                z++;
            }
            pos.x = (x + .5f) * step - 1f;
            pos.y = Mathf.Pow(pos.x,3);
            pos.z = (z + .5f) * step - 1f;

            Transform point = Instantiate(PointPrefab);
            point.SetParent(transform, false);

            point.localPosition = pos;
            point.localScale = scale;

            Points[i] = point;
        }
    }

    private void UpdatePointsPosition()
    {
        float time = Time.time;
        for (int i = 0; i < Points.Length; i++)
        {
            Transform point = Points[i];
            Vector3 pos = point.position;

            var function = FunctionLibrary.GetFunction((int)Function);
            pos.y = function(pos.x, pos.z, time);

            point.position = pos;
        }
    }
}
