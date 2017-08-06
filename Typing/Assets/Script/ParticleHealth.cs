using UnityEngine;
using System.Collections;

public class ParticleHealth : MonoBehaviour {

    [SerializeField]
    GameObject parent;
    Transform _attractorTransform;
    float timer;

    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];
    // Use this for initialization
    void Start () {
        _particleSystem = GetComponent<ParticleSystem>();
        _attractorTransform = GameObject.FindGameObjectWithTag("Player").transform;
        parent = GameObject.FindGameObjectWithTag("particleparent");
        gameObject.transform.SetParent(parent.transform.parent.gameObject.transform, false);
        //gameObject.transform.localPosition = parent.transform.localPosition;
	}

    // Update is called once per frame
    void Update () {
        if (!_particleSystem.IsAlive())
            Destroy(gameObject);
	}
}
