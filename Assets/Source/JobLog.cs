using Unity.Jobs;
using UnityEngine;

public struct JobLog : IJob
{
    public int number;
    public void Execute()
    {
        Debug.Log(Mathf.Log(number));
    }
}