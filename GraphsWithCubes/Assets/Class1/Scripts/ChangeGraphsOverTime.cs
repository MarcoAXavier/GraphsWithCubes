using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class ChangeGraphsOverTime : MonoBehaviour
{
    private Graph Graph { get; set; }

    [SerializeField] private float _interval;
    private float Interval => _interval;

    private void Start()
    {
        Graph = GetComponent<Graph>();
        Graph.OnInit += Init;
    }

    public void Init()
    {
        StartCoroutine(ChangeFunctionOverTime());
    }

    private IEnumerator ChangeFunctionOverTime()
    {
        WaitForSeconds wfs = new WaitForSeconds(Interval);

        int currentFunction = 0;
        while (enabled)
        {
            yield return wfs;
            Graph.SetFunction(currentFunction++);
            currentFunction %= FunctionLibrary.GetFunctionsCount();
        }
    }

    public void OnDestroy()
    {
        Graph.OnInit -= Init;
    }
}
