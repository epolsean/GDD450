using UnityEngine;
using System.Collections;

public class ParticleCirclesSphere : MonoBehaviour 
{
    private ParticleSystem.Particle[] points;

    private void CreatePoints()
    {
        points = new ParticleSystem.Particle[10];
        for (int i = 0; i < points.Length; i++)
        {
            float angle = Mathf.PI * (36f* i/180f);
            Vector3 p = new Vector3(Mathf.Sin(angle), 0,Mathf.Cos(angle));
            points[i].position = p;
            if (p.x < 0)
            {
                p.x *=-1;
            }
            if (p.y < 0)
            {
                p.y *= -1;
            }
            if (p.z < 0)
            {
                p.z *= -1;
            }
            points[i].color = new Color(p.x,p.y,p.z);
            Debug.Log("color of particle : " + points[i].color);
            points[i].velocity = new Vector3(0, 1, 0);
            Debug.Log("position : " + points[i].position);
            points[i].size = 1f;
        }
    }
    void Start()
    {
        CreatePoints();
        particleSystem.SetParticles(points, points.Length);
    }

    void Update()
    {
        //CreatePoints();
        for (int i = 0; i < points.Length; i++)
        {
            //int vel = Random.Range(-4, 4);
            float angle = Mathf.PI * (36f * i / 180f);
            Vector3 p = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
            if (p.x < 0)
            {
                p.x *= -1;
            }
            if (p.y < 0)
            {
                p.y *= -1;
            }
            if (p.z < 0)
            {
                p.z *= -1;
            }
            points[i].color = new Color(p.x, p.y, p.z);
            points[i].velocity = Vector3.Normalize(points[i].position);//Vector3.Cross(points[i].position,transform.position);
            //Debug.Log("velocity : " + points[i].velocity);
            points[i].startLifetime = 5;
            points[i].position += points[i].velocity;
        }
        particleSystem.SetParticles(points, points.Length);
    }
}
