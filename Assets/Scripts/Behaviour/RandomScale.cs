using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Momiji;

public class RandomScale : MonoBehaviour
{

    [SerializeField]
    private bool _initStart = true;
    [SerializeField]
    private float _minScale;
    [SerializeField]
    private float _maxScale;

    public float Time { get; set; } = 1.0f;

    // Use this for initialization
    void Start()
    {
        if (_initStart) Run();
    }

    public void Run()
    {
        transform.DOScale(Vector3.zero.RandomRange(_minScale, _maxScale), Time).Play();
    }

    IEnumerator RoopScale()
    {
        while (_initStart)
        {
            transform.DOScale(Vector3.zero.RandomRange(_minScale, _maxScale), Time).Play();
            yield return new WaitForSeconds(Time);
        }
    }
}
