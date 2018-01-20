using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

	public Transform pointPrefab;
	public Transform[] points;

	[Range(10, 100)] 
	public int resolution = 10;

	void Awake () {
		float step = 2f / resolution;
		Vector3 scale = Vector3.one * step;
		Vector3 position;
		position.y = 0f;
		position.z = 0f;

		points = new Transform[resolution];
		for (int i = 0; i < resolution; i++) {
			Transform point = Instantiate(pointPrefab);
			position.x = (-1.0f + (i + 0.5f) * step);
			point.localPosition = position;
			point.localScale = scale;
			point.SetParent(transform, false);
			points[i] = point;
		}
	}

	void Update () {
		for(int i = 0; i < points.Length; i++) {
			Transform point = points[i];
			Vector3 position = point.localPosition;
			position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
			points[i].localPosition = position;
		}
	}
}
