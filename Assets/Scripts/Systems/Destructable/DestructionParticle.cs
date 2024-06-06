using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionParticle : MonoBehaviour
{
    [SerializeField] float timeToDestroyParticle = 4f;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private WaitForSeconds particleDestroyTime;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        particleDestroyTime = new WaitForSeconds(timeToDestroyParticle);
    }

    private void OnEnable()
    {
        StartCoroutine(DisableParticle());
    }

    public void SetMesh(Mesh neededMesh, Material neededMaterial)
    {
        meshRenderer.material = neededMaterial;
        meshFilter.mesh = neededMesh;
    }

    private IEnumerator DisableParticle()
    {
        // Stuff can be done here [Tegomlee].

        yield return particleDestroyTime;

        ParticleManager.Instance.DisableParticle(gameObject);
    }
}
