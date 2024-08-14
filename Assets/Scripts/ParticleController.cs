using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem redToGreenParticles;
    public ParticleSystem greenToRedParticles;

    void Update()
    {
        AdjustParticleDirection( redToGreenParticles.transform, greenToRedParticles.transform);
        AdjustParticleDirection( greenToRedParticles.transform, redToGreenParticles.transform);
    }

    void AdjustParticleDirection( Transform from, Transform to)
    {
        Vector3 direction = to.position - from.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        from.rotation = rotation;
    }

}
