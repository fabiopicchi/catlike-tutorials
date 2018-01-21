using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

	public Transform pointPrefab;
	public Transform[] points;

	[Range(10, 100)] 
	public int resolution = 10;

	public GraphFuncitonName function;

	const float pi = Mathf.PI;

	static GraphFunction[] functions = {
		SineFunction, MultiSineFunction,
		Sine2DFunction, MultiSine2DFunction,
		RippleFunction, CylinderFunction,
		SphereFunction, TorusFunction
	};

	void Awake () {
		float step = 2f / resolution;
		Vector3 scale = Vector3.one * step;
		points = new Transform[resolution * resolution];
		for (int i = 0; i < points.Length; i++) {
			Transform point = Instantiate(pointPrefab);
			point.localScale = scale;
			point.SetParent(transform, false);
			points[i] = point;
		}
	}

	void Update () {
		float t = Time.time;
		float step = 2f / resolution;
		GraphFunction f = functions[(int)function];

		for (int i = 0, x = 0; x < resolution; x++) { 
			float u = (-1.0f + (x + 0.5f) * step);
			for (int z = 0; z < resolution; z++, i++) {
				float v = (-1.0f + (z + 0.5f) * step);
				points[i].localPosition = f(u, v, t);
			}
		}
	}

	static Vector3 SineFunction(float u, float v, float t) {
		return new Vector3(u, Mathf.Sin(pi * (u + t)), v);
	}

	static Vector3 Sine2DFunction(float u, float v, float t) {
		float y = Mathf.Sin(pi * (u + t));
		y += Mathf.Sin(pi * (v + t));
		y *= 0.5f;
		return new Vector3(u, y, v);
	}

	static Vector3 MultiSineFunction(float u, float v, float t) {
		float y = Mathf.Sin(pi * (u + t));
		y += Mathf.Sin(2f * pi * (u + 2f * t)) * 0.5f;
		y *= 2f / 3f;
		return new Vector3(u, y, v);
	}

	static Vector3 MultiSine2DFunction(float u, float v, float t) {
		float y = 4f * Mathf.Sin(pi * (u + v + 0.5f * t));
		y += Mathf.Sin(pi * (u + t));
		y += Mathf.Sin(2f * pi * (v + 2f * t)) * 0.5f;
		y *= 1f / 5.5f;
		return new Vector3(u, y, v);
	}

	static Vector3 RippleFunction(float u, float v, float t) {
		float d = Mathf.Sqrt(u * u + v * v);
		return new Vector3(u, Mathf.Sin(pi * (4f * d - t)) / (1f + 10f * d), v);
	}

	static Vector3 CylinderFunction(float u, float v, float t) {
		float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
		return new Vector3(
			r * Mathf.Sin(pi * u), 
			v, 
			r * Mathf.Cos(pi * u)
		);
	}
	static Vector3 SphereFunction(float u, float v, float t) {
		float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
		r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
		float s = r * Mathf.Cos(pi * v * 0.5f);
		return new Vector3(
			s * Mathf.Sin(pi * u), 
			r * Mathf.Sin(pi * v * 0.5f), 
			s * Mathf.Cos(pi * u)
		);
	}

	static Vector3 TorusFunction(float u, float v, float t) {
		float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
		float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
		float s = r2 * Mathf.Cos(pi * v) + r1;
		return new Vector3(
			s * Mathf.Sin(pi * u), 
			r2 * Mathf.Sin(pi * v), 
			s * Mathf.Cos(pi * u)
		);
	}
}
