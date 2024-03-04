using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class ObjectMuvment : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _speed;
    [SerializeField] private int _objectCount;
    [SerializeField] private float _logFrequancy;
    private float _timeToLog;


    private Transform[] _objectsOnScene;
    private TransformAccessArray _transformsOnScene;
    private JobMuvmemt _jobMuvmemt;
    private JobLog _jobLog;
    private JobHandle _jobMovementHandle;
    private JobHandle _jobLogHandle;

    private void Start()
    {
        Instatsiate();
    }

    
    
    private void Instatsiate()
    {
        _objectsOnScene = new Transform[_objectCount];

        for (int i = 0; i < _objectCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            GameObject instance = Instantiate(_prefab, position, Quaternion.identity);
            _objectsOnScene[i] = instance.transform;
        }

        _transformsOnScene = new TransformAccessArray(_objectsOnScene);

        _timeToLog = 0;
    }

    
    
    private void Update()
    {
        _jobMuvmemt = new JobMuvmemt()
        {
            Speed = _speed,
            DeltaTime = Time.deltaTime
        };
        _jobLog = new JobLog() { number = Random.Range(1, 1000) };

        _jobMovementHandle = _jobMuvmemt.Schedule(_transformsOnScene);

        _timeToLog += Time.deltaTime;
        if (_timeToLog >= _logFrequancy)
        {
            _timeToLog = 0;
            foreach (Transform obj in _objectsOnScene)
                _jobLog.Schedule();
        }
    }

    private void LateUpdate()
    {
        _jobMovementHandle.Complete();
    }

    private void OnDestroy()
    {
        _transformsOnScene.Dispose();
    }
}
