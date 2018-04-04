using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ControllerFromSound : MonoBehaviour
{

    [SerializeField]
    private AudioSource _source;

    public float Time { get; private set; }
    public float Power { get; private set; }

    // Use this for initialization
    void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                var spec = new float[1024];

                _source.GetSpectrumData(spec, 0, FFTWindow.Hamming);

                Time = _source.time;
                Power = spec.Sum();
            })
            .AddTo(this);
    }
}
