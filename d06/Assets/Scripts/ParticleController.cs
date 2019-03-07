using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public MainController mainController;
    private ParticleSystem particleSystem;
    private ParticleCollisionEvent[] collisionEvents;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[8];
    }

    private void OnParticleTrigger()
    {
        print("Trigger");
    }

    public void OnParticleCollision(GameObject other)
    {
        int collCount = particleSystem.GetSafeCollisionEventSize();

        if (collCount > collisionEvents.Length)
            collisionEvents = new ParticleCollisionEvent[collCount];
        int eventCount = particleSystem.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < eventCount; i++)
        {
            print(other.name);
        }
    }
}
