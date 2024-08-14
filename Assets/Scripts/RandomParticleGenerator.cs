using UnityEngine;

public class RandomParticleGenerator : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float volumeRadius = 10f;

    void Start()
    {
        ConfigureParticleSystem();
    }

    void ConfigureParticleSystem()
    {
        var main = particleSystem.main;
        main.maxParticles = 2000000;

        var emission = particleSystem.emission;
        emission.rateOverTime = 200000f;

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = volumeRadius;
        shape.randomDirection = true;

        var velocityOverLifetime = particleSystem.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;
        velocityOverLifetime.orbitalX = new ParticleSystem.MinMaxCurve(-1f, 1f);
        velocityOverLifetime.orbitalY = new ParticleSystem.MinMaxCurve(-1f, 1f);
        velocityOverLifetime.orbitalZ = new ParticleSystem.MinMaxCurve(-1f, 1f);

        var limitVelocity = particleSystem.limitVelocityOverLifetime;
        limitVelocity.enabled = true;
        limitVelocity.limit = 2f; 
    }
}
