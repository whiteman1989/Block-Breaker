using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
    public Sprite onMagnetSprite;
    public bool turnMagnet = false;

    private Sprite ofMagnetSprite;
    private ParticleSystem wave;
    private PointEffector2D magnetism;

    // Use this for initialization
    void Start () {
        ofMagnetSprite = this.GetComponent<SpriteRenderer>().sprite;
        wave = this.GetComponentInChildren<ParticleSystem>();
        magnetism = GetComponent<PointEffector2D>();
        MagnetismSwich();
	}

    void OnCollisionEnter2D(Collision2D col) {
        turnMagnet = (!turnMagnet);
        MagnetismSwich();
    }
		

    void MagnetismSwich() {
        if (turnMagnet) { this.GetComponent<SpriteRenderer>().sprite = onMagnetSprite;} else { this.GetComponent<SpriteRenderer>().sprite = ofMagnetSprite; }
        magnetism.enabled = turnMagnet;
        var em = wave.emission;
        em.enabled = turnMagnet;
        print("Switch magnetism/////////");
    }
}
