using UnityEngine;


public class ParticleBehaviour : MonoBehaviour
{
    public HooksLaw.Particle particle;


    public void FixedUpdate()
    {
        transform.position = particle.Update(Time.fixedDeltaTime);
    }
}