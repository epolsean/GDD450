using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour 
{
    private ParticleSystem.Particle[] points;
    float[] angleThetaIncreases;
    float[] anglePhiIncreases;
    float[] startAngleTheta;
    float[] startAnglePhi;

    public Material material;

    public enum PowerupType {Speed, Damage, Health, Mystery };
    public PowerupType power;

    public Mesh damageBoostMesh;
    public Mesh speedBoostMesh;
    public Mesh healthBoostMesh;
    public Mesh mysteryBoostMesh;

    private void CreatePoints()
    {
        points = new ParticleSystem.Particle[100];
        angleThetaIncreases = new float[100];
        anglePhiIncreases = new float[100];
        startAngleTheta = new float[100];
        startAnglePhi = new float[100];
        for (int i = 0; i < points.Length; i++)
        {
            angleThetaIncreases[i] = Random.Range(-1f,1f);
            anglePhiIncreases[i] = angleThetaIncreases[i];
            startAngleTheta[i] = Random.Range(0, 2*Mathf.PI);
            startAnglePhi[i] = Random.Range(0, 2*Mathf.PI);
            points[i].position = new Vector3(Mathf.Cos(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Sin(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Cos(startAngleTheta[i]));
            
            points[i].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0, 1f),Random.Range(.3f,.9f));
            points[i].size = Random.Range(0.4f,0.6f);
        }
    }
    void Start()
    {
        float choicType = Random.Range(0f, 1000f);
        if (choicType < 180)
        {
            power = PowerupType.Damage;
            GetComponent<MeshFilter>().mesh = damageBoostMesh;
        }
        else if (choicType < 400)
        {
            power = PowerupType.Health;
            GetComponent<MeshFilter>().mesh = healthBoostMesh;
        }
        else if (choicType < 620)
        {
            power = PowerupType.Speed;
            GetComponent<MeshFilter>().mesh = speedBoostMesh;
        }
        else
        {
            float secondChoice = Random.Range(0f, 1000f);
            if (secondChoice < 333)
            {
                power = PowerupType.Damage;
            }
            else if (secondChoice < 666)
            {
                power = PowerupType.Speed;
            }
            else
            {
                power = PowerupType.Health;
            }
            
            GetComponent<MeshFilter>().mesh = mysteryBoostMesh;
        }
        CreatePoints();
        particleSystem.SetParticles(points, points.Length);
        particleSystem.renderer.material = material;
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            startAnglePhi[i] += anglePhiIncreases[i] * Time.deltaTime * Random.Range(1.0f, 2.0f);
            startAngleTheta[i] += angleThetaIncreases[i] * Time.deltaTime * Random.Range(1.0f, 2.0f);
            points[i].position = new Vector3(Mathf.Cos(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Sin(startAnglePhi[i]) * Mathf.Sin(startAngleTheta[i]), Mathf.Cos(startAngleTheta[i]));
        }
        particleSystem.SetParticles(points, points.Length);
    }
}
