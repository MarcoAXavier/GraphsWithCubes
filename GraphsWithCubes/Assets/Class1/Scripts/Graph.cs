using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private Transform _pointPrefab;
    private Transform PointPrefab => _pointPrefab;

    [SerializeField, Range(10,100)]
    private float _resolution = 10;
    private float Resolution => _resolution;


    private void Awake()
    {
        for (int i = 0; i < Resolution; i++)
        {
            Vector3 pos = Vector3.right * i / (PointPrefab.transform.localScale.x*2);
            pos.y = pos.x*pos.x;
            Instantiate(PointPrefab, pos, Quaternion.identity);
        }
    }

}
