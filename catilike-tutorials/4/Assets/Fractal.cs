using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

	public Mesh[] meshes;
	private Material[,] materials;
	public Material material;
	public int maxDepth;
	private int depth;
	public float childScale;
	public float spawnProbability;
	public float maxRotationSpeed;
	private float rotationSpeed;

	private Vector3[] directions = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	private Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
	};

	void InitializeMaterials() {
		materials = new Material[maxDepth + 1, 2];
		for (int i = 0; i < maxDepth; i++) {
			float t = i / (maxDepth - 1f);
			t *= t;
			materials[i, 0] = new Material(material);
			materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);

			materials[i, 1] = new Material(material);
			materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
		}

		materials[maxDepth, 0] = new Material(material);
		materials[maxDepth, 0].color = Color.magenta;

		materials[maxDepth, 1] = new Material(material);
		materials[maxDepth, 1].color = Color.red;
	}

	// Use this for initialization
	void Start () {
		if (materials == null) {
			InitializeMaterials();
		}
		rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
		gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = materials[depth, Random.Range(0, 2)];
		if (depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}

	private IEnumerator CreateChildren() {
		for (int i = 0; i < directions.Length; i++) {
			if (Random.value < spawnProbability) {
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
				new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, directions[i], rotations[i]);
			}
		}
		if (depth == 0 && Random.value < spawnProbability) {
			yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
			new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.down, Quaternion.Euler(0f, 0f, 180f));
		}
	}

	void Initialize (Fractal parent, Vector3 direction, Quaternion rotation) {
		spawnProbability = parent.spawnProbability;
		meshes = parent.meshes;
		materials = parent.materials;
		depth = parent.depth + 1;
		maxDepth = parent.maxDepth;
		maxRotationSpeed = parent.maxRotationSpeed;
		transform.parent = parent.transform;
		childScale = parent.childScale;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
		transform.localRotation = rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}
}
