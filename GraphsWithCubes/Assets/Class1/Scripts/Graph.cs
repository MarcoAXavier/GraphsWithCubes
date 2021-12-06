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

    [SerializeField, Range(1,10)]
    private float _pointsSizeDivider;
    private float PointsSizeDivider => _pointsSizeDivider;


    private void Awake ()=> PositionAndScalePoints();
    

    private void PositionAndScalePoints()
    {
        for (int i = 0; i < Resolution; i++)
        {
            Vector3 pos = Vector3.right * (i / PointsSizeDivider - 1f);

            Transform point = Instantiate(PointPrefab);
            //pos.y = pos.x*pos.x;

            point.localPosition = pos;
            point.localScale = Vector3.one / PointsSizeDivider;
        }
    }
}
