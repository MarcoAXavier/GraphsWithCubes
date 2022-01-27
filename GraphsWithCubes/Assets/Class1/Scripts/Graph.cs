using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting.Dependencies.Sqlite;
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

    private FunctionLibrary.Function CurrentFunction { get; set; } = null;
    Transform[] Points { get; set; }
    private float LerpTimer { get; set; } = 0;

    public Action OnInit;

    private void Start ()=> PositionAndScalePoints();

    private void Update() => UpdatePointsPosition();

    private void PositionAndScalePoints()
    {
        Points = new Transform[Resolution * Resolution];
        float step = (float)2 / Resolution;
        Vector3 scale = Vector3.one * step;
        SetFunction((int)Function);

        for (int i = 0, x = 0; i < Points.Length; i++, x++)
        {
            Transform point = Instantiate(PointPrefab);
            point.SetParent(transform, false);

            point.localScale = scale;

            Points[i] = point;
        }
        OnInit?.Invoke();
    }

    public void SetFunction(int newFunctionIndex)
    {
        CurrentFunction = FunctionLibrary.GetFunction(newFunctionIndex);
    }

    private void UpdatePointsPosition()
    {
        float time = Time.time;

        float step = 2f / Resolution;
        float v = .5f * step - 1f;
        for (int i = 0, x=0, z=0; i < Points.Length; i++, x++)
        {
            if (x == Resolution)
            {
                x = 0;
                z++;
                v = (z + .5f) *step - 1f;
            }

            float u = (x + .5f) * step - 1f;
            Points[i].localPosition = CurrentFunction(u, v, time);
        }
    }
}
