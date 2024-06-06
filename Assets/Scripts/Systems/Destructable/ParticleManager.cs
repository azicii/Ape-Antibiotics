using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager instance;
    public static ParticleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ParticleManager>();
            }

            return instance;
        }
    }

    [SerializeField] GameObject particlePrefab;
    private List<GameObject> particles = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ActivateParticles(GameObject parent)
    {
        Vector3 parentPosition = parent.transform.position;
        Vector3 parentForward = parent.transform.forward;

        Mesh parentMesh = parent.GetComponent<MeshFilter>().mesh;
        Material parentMaterial = parent.GetComponent<MeshRenderer>().material;

        // These numbers are arbitrary and can be changed and set via a serialiezed variable [Tegomlee].
        int numOfParticles = Random.Range(3, 7);

        parent.SetActive(false);

        for (int i = 0; i < numOfParticles; i++)
        {
            float radius = Random.Range(0.1f, 1f);
            Vector3 randomPosition = Random.insideUnitSphere * radius;
            randomPosition += parentPosition;
            randomPosition.y = parentPosition.y;

            Vector3 direction = randomPosition - parentPosition;
            direction.Normalize();

            float dotProduct = Vector3.Dot(parentForward, direction);
            float dotProductAngle = Mathf.Acos(dotProduct / parentForward.magnitude * direction.magnitude);

            randomPosition.x = Mathf.Cos(dotProductAngle) * radius + parentPosition.x;
            randomPosition.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + parentPosition.z;

            GameObject currentParticle = GetParticle();
            currentParticle.transform.position = randomPosition;
            currentParticle.GetComponent<MeshRenderer>().material = parentMaterial;
            currentParticle.GetComponent<MeshFilter>().mesh = parentMesh;
        }
    }

    public void DisableParticle(GameObject particleToDisable)
    {
        particleToDisable.SetActive(false);
    }

    private GameObject GetParticle()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            if (!particles[i].activeInHierarchy)
            {
                particles[i].SetActive(true);
                return particles[i];
            }
        }

        GameObject newParticle = Instantiate(particlePrefab);
        particles.Add(newParticle);
        return newParticle;
    }
}
