using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{

    public VisualEffect vfx;
    public int aliveParticles;

    public Transform SpinnerColliderTransform;

    private void Update()
    {
        vfx.SetVector3("SpinnerPosition", SpinnerColliderTransform.position);
        vfx.SetVector3("SpinnerAngles", SpinnerColliderTransform.eulerAngles);
        vfx.SetVector3("SpinnerSize", SpinnerColliderTransform.localScale);
    }

    void LateUpdate()
    {
        aliveParticles = vfx.aliveParticleCount;
    }
}
