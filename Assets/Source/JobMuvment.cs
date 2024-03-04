using UnityEngine;
using UnityEngine.Jobs;

public struct JobMuvmemt : IJobParallelForTransform
{
    public float Speed;
    public float DeltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        Quaternion direction = transform.rotation;
        transform.position += direction * Vector3.forward * Speed * DeltaTime;
        Quaternion q = Quaternion.Euler(0, 15 * DeltaTime, 0);
        transform.rotation *= q;
    }
}
