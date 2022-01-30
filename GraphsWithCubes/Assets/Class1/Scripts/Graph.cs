using System;
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

    [SerializeField, Min(0f)] 
    private float _functionDuration = 1f, _transitionDuration = 1f;
    private float FunctionDuration => _functionDuration;
    private float TransitionDuration =>_transitionDuration;

    private bool IsTransitioning { get; set; } = false;
    private FunctionLibrary.Function TransitionDestFunction { get; set; } = null;
    private FunctionLibrary.Function CurrentFunction { get; set; } = null;
    Transform[] Points { get; set; }
    private float CurrentDuration { get; set; } = 0;

    private void Start ()=> PositionAndScalePoints();

    private void Update()
    {
        CurrentDuration += Time.deltaTime;
        if (IsTransitioning)
        {
            if (CurrentDuration > TransitionDuration)
            {
                CurrentDuration -= TransitionDuration;
                SetFunction(TransitionDestFunction);
                TransitionDestFunction = null;
                IsTransitioning = false;
            }
        }

        if (CurrentDuration > FunctionDuration)
        {
            CurrentDuration -= FunctionDuration;
            IsTransitioning = true;
            TransitionDestFunction = FunctionLibrary.GetRandomFunctionOtherThan(CurrentFunction);
        }

        if(IsTransitioning) UpdateFunctionTransition(); 
        else UpdateFunction();
    }

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
    }

    public void SetFunction(int newFunctionIndex)
    {
        CurrentFunction = FunctionLibrary.GetFunction(newFunctionIndex);
    }

    public void SetFunction(FunctionLibrary.Function newFunction)
    {
        CurrentFunction = newFunction;
    }

    private void UpdateFunction()
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

    void UpdateFunctionTransition()
    {
        FunctionLibrary.Function
            from = CurrentFunction,
            to = TransitionDestFunction;
        Debug.Log($"from => {FunctionLibrary.GetFunctionName(from)} , to => {FunctionLibrary.GetFunctionName(to)}");
        float progress = CurrentDuration / TransitionDuration;
        float time = Time.time;

        float step = 2f / Resolution;
        float v = .5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < Points.Length; i++, x++)
        {
            if (x == Resolution)
            {
                x = 0;
                z++;
                v = (z + .5f) * step - 1f;
            }

            float u = (x + .5f) * step - 1f;
            Points[i].localPosition = FunctionLibrary.Morph(u, v, time, from, to, progress);
        }
    }
}
