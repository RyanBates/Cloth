using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpringDamperBehaviour : MonoBehaviour
{
    public GameObject game;
    public float rows = 10;
    public float colms = 10;
    public float drag = 1;
    public float density = 1;

    public float spring, damping, rest;

    public Vector3 wind = new Vector3(10,10,10);

    public Slider windx;
    public Slider windy;
    public Slider windz;

    HooksLaw.Particle p1, p2, p3;

    HooksLaw.SpringDamper Sd;

    public List<HooksLaw.Particle> particles;
    public List<HooksLaw.SpringDamper> springDampers;
    public List<GameObject> objects;

    public void CalculateWind(Vector3 w)
    {
        Vector3 v = ( p1.velocity + p2.velocity + p3.velocity) / 3 - w;

        Vector3 n = Vector3.Cross((p2.position - p1.position), (p3.position - p1.position)).normalized;

        float ao = n.magnitude;

        float a = ao * Vector3.Dot(v, n) / v.magnitude;

        Vector3 f = (density / -1) * (v.magnitude * v.magnitude) * drag * a * n;

        p1.AddForce(f / 5);
        p2.AddForce(f / 5);
        p3.AddForce(f / 5);
    }

    void Start()
    {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < colms; j++)
            {
                GameObject p = Instantiate(game, new Vector3(i * 5, -j * 5, 7), Quaternion.identity);
                p.AddComponent<ParticleBehaviour>();
                p.GetComponent<ParticleBehaviour>().particle = new HooksLaw.Particle(p.transform.position, Vector3.zero, 1);
                gameObject.GetComponent<SpringDamperBehaviour>().particles.Add(p.GetComponent<ParticleBehaviour>().particle);
                objects.Add(p);
            }

        for (int i = 0; i < (colms * rows); i++)
        {
            particles[i].gravity = true;

            if (i % colms < colms - 1)
            {
                p1 = particles[i];
                p2 = particles[i + 1];
                
                Sd = new HooksLaw.SpringDamper(p1, p2);
                springDampers.Add(Sd);
            }

            if (i < (colms * rows) - colms)
            {
                p1 = particles[i];
                p2 = particles[i + (int)rows];

                Sd = new HooksLaw.SpringDamper(p1, p2);
                springDampers.Add(Sd);
            }

            if (i < (colms * rows) - colms && i % colms < colms - 1)
            {
                int bottom = i + (int)rows;
                int right = i + 1;

                p1 = particles[right];
                p2 = particles[bottom];

                Sd = new HooksLaw.SpringDamper(p1, p2);
                springDampers.Add(Sd);
            }

            if (i < (colms * rows) - colms && i % colms < colms - 1)
            {
                int bottom = i + (int)colms;
                int bottomright = bottom + 1;

                p1 = particles[i];
                p2 = particles[bottomright];

                Sd = new HooksLaw.SpringDamper(p1, p2);
                springDampers.Add(Sd);
            }
            
        }
    }
    
    void Update()
    {
        foreach (var p in particles)
            if (p.gravity == true)
            {
                p.Update(Time.deltaTime);
                for (int i = 0; i < particles.Count - (int)colms; i++)
                {
                    particles[0].anchor = true;

                    p1 = particles[i];
                    p2 = particles[i + 1];
                    p3 = particles[i + (int)colms];

                    p1.position = objects[i].transform.position;
                    p2.position = objects[i + 1].transform.position;
                    p3.position = objects[i + (int)colms].transform.position;

                    p1.anchor = false;
                    p2.anchor = true;
                    p3.anchor = false;

                   CalculateWind(wind);
                }
            }

        foreach (var s in springDampers)
            s.CalculateForce(spring,rest,damping);
        
        windx.minValue = -10;
        windx.maxValue = 10;
        wind.x = windx.value;

        windy.minValue = -10;
        windy.maxValue = 10;
        wind.y = windy.value;

        windz.minValue = -10;
        windz.maxValue = 10;
        wind.z = windz.value;
    }
}