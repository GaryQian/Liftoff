﻿using UnityEngine;
using System.Collections;
public enum ObstacleType { meteor, asteroid, satellite, other }

public class Obstacle : MonoBehaviour {
    public ObstacleType type;

    public GameObject alertPrefab;
    GameObject alert;
	// Use this for initialization
	void Start () {
        if (Util.even) type = ObstacleType.meteor;
        switch (type) {
            case ObstacleType.meteor: GetComponent<SpriteRenderer>().sprite = Util.obstacleHolder.getMeteor(0); break;
            case ObstacleType.satellite: GetComponent<SpriteRenderer>().sprite = Util.obstacleHolder.getSatellite(0); break;
        }
        float size = Random.Range(0.7f * 0.8f, 0.7f * 1.2f);
        transform.position = new Vector3(Random.Range(-Util.width, Util.width), Camera.main.transform.position.y + 30f, 0);
        transform.localScale = new Vector3(size, size, size);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));

        alert = Instantiate(alertPrefab);
        alert.GetComponent<Alert>().parent = gameObject;

        GetComponent<Motion>().endPos = new Vector3(transform.position.x, Camera.main.transform.position.y, 0);
        GetComponent<Motion>().begin();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll) {
        if (Util.wm.gameActive && coll.gameObject.name.Equals("Rocket") && !Util.wm.godmode) {
            Util.gm.die();
            GetComponent<Motion>().end();
        }
    }
}
