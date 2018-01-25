using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexChip : MonoBehaviour
{

    [SerializeField]
    private float DOWN_INTERBAL = 0.5f;

    private int _x, _y, _z;
    private float randomStart;
    private bool _hit;
    private Vector3 _initPos;
    private Vector3 _keepPos;
    private Vector3 _exitPos;
    private float _lerpTime;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }
    public int Z { get { return _z; } }
    public bool Hit { get { return _hit; } }

    void Start()
    {
        randomStart = Random.Range(-1.0f, 1.0f);
        _hit = false;
        _initPos = transform.position;
        _lerpTime = 0;

        var move = DOTween.Sequence()
            .Append(transform.DOMove(_exitPos, Random.Range(1.0f, 5.0f)))
            .Append(transform.DOMove(_initPos, Random.Range(1.0f, 5.0f)))
            .SetEase(Ease.InBounce)
            .SetLoops(-1);

        var rot = DOTween.Sequence()
            .Append(transform.DORotate(Vector3.up * 180, 1.0f))
            .SetEase(Ease.InBounce)
            .SetLoops(-1);

        var allExits = DOTween.Sequence()
            .Append(transform.DOMove(_exitPos, 1.0f))
            .SetEase(Ease.InBounce);

        move.Play();
        rot.Play();
    }

    public void SetNum(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
}
