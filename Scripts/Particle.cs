using System.Collections.Generic;
using UnityEngine;
        
namespace HooksLaw
{
    [System.Serializable]
    public class Particle
    {
        [SerializeField]
        public Vector3 force;
        [SerializeField]
        public Vector3 position;
        [SerializeField]
        public Vector3 acceleration;
        [SerializeField]
        public Vector3 velocity;

        [SerializeField]
        public bool gravity;
        [SerializeField]
        public bool anchor;

        [SerializeField]
        public float mass;

        public Particle()
        {
            position = Vector3.zero;
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
            force = Vector3.zero;
            mass = 5;
        }

        public Particle(Vector3 p, Vector3 v, float m)
        {
            position = p;
            acceleration = Vector3.zero;
            velocity = v;
            force = Vector3.zero;
            mass = m;
        }

        public void AddForce(Vector3 f)
        {
            force += f;
        }
        
        // Update is called once per frame
        public Vector3 Update(float deltaTime)
        {
            //force = Vector3.zero;

            if (gravity)
                AddForce(new Vector3(0, -9.81f, 0));

            if (anchor)
                return position;

            acceleration = force / mass;
            velocity += acceleration * deltaTime;
            position += velocity * deltaTime;
            force = Vector3.zero;

            return position;
        }
    }
    
    [System.Serializable]
    public class SpringDamper
    {
        public Particle _P1, _P2;
        [SerializeField]
        public float _Ks;
        [SerializeField]
        public float _Lo;
        [SerializeField]
        public float _Kd;

        public SpringDamper()
        {

        }

        public SpringDamper(Particle p1, Particle p2)
        {
            _P1 = p1;
            _P2 = p2;
        }

        public SpringDamper(Particle p1, Particle p2, float springConstant, float restLength, float dampingFactor)
        {
            _P1 = p1;
            _P2 = p2;
            _Ks = springConstant;
            _Kd = dampingFactor;
            _Lo = restLength;
        }
        
        public void CalculateForce(float s, float r, float d)
        {
            //Convert 3D to 1D
            Vector3 e = (_P2.position - _P1.position);
            float l = Vector3.Magnitude(e);            
            Vector3 E = e / l;

            //Calculating 1D Velocities
            Vector3 v1 = _P1.velocity;
            Vector3 v2 = _P2.velocity;

            float V1 = Vector3.Dot(E.normalized, v1);
            float V2 = Vector3.Dot(E.normalized, v2);

            //Convert 1D to 3D
            float Fsd = (-s * (r - l)) - (d * (V1 - V2));

            Vector3 F1 = Fsd * E;

            _P1.AddForce(F1);
            _P2.AddForce(-F1);

            Debug.DrawLine(_P1.position, _P2.position, Color.red);

        }
    }
}