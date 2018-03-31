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

    // Use this for initialization
    void Start()
    {
        // TODO: _vectorを使用して扱えるようにする
        DOTween.Sequence()
            .AppendCallback(() => transform.DORotate(Enum<Vector>.Random.EnumToNormalize(true) * _rot, _time, _mode).Play())
            .AppendInterval(_time)
            .SetEase(_easeType)
            .SetLoops(-1)
            .Play();
    }
}
