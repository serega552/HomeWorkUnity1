using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayer : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currentPosition;

    void Start()
    {
        _points = new Transform[_path.childCount];

        for(int i = 0; i < _points.Length; i++)
        {
            _points[i] = _path.GetChild(i).transform;
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPosition];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPosition++;
                if(_currentPosition >= _points.Length)
            {
                _currentPosition = 0;
            }
        }
    }
}
