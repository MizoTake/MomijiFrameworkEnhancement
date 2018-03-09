using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Magnet : MonoBehaviour, IInitializable
{
    [Inject]
    private List<Magnet> _magnets;
    [SerializeField]
    private float _range = 60;
    [SerializeField]
    private Type _mapType;
    public enum Type
    {
        S,
        N
    }

    public void Initialize()
    {
        Debug.Log(_magnets.Count);
    }
}