using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Momiji;

public class Rotation : MonoBehaviour
{

    [SerializeField]
    private Ease _easeType;
    [SerializeField]
    private RotateMode _mode;
    [SerializeField]
    private Vector3 _vector;
    [SerializeField]
    private float _rot;
    [SerializeField]
    private float _time = 1.0f;

    public int Count = -1;

    public float Time { set { _time = value; } }

    // Use this for initialization
    void Start()
    {
        Run();
    }

    public void Run()
    {
        // TODO: _vectorを使用して扱えるようにする
        DOTween.Sequence()
            .AppendCallback(() => transform.DORotate(Enum<Vector>.Random.EnumToNormalize(true) * _rot, _time, _mode).Play())
            .AppendInterval(_time)
            .SetEase(_easeType)
            .SetLoops(Count)
            .Play();
    }
}
