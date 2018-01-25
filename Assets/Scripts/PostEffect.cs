using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PostEffect : MonoBehaviour
{

    public Material monoTone;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, monoTone);
    }
}
