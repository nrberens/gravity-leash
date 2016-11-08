using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using CoherentNoise.Generation.Fractal;

public class MapGenerator3D : MonoBehaviour {

	public int height;
	public int width;
	public int depth;

	public string seed;

	public bool useRandomSeed;

	[RangeAttribute(0,100)]
	public int randomFillPercent;
	public int smoothIterations;
	public int minSmoothWalls;
	public int maxSmoothWalls;
	int [,,] map;

	public int currentGizmoDepth;
	// Use this for initialization
	void Start () {

		GenerateMap();
		currentGizmoDepth = depth/2;
	}
	
	// Update is called once per frame
	void Update () {
/*		if(Input.GetMouseButtonDown(0)) {
			currentGizmoDepth++;
		}
		if(Input.GetMouseButtonDown(1)){
			currentGizmoDepth--;
		}*/
		if(Input.GetKeyDown(KeyCode.G)) {
			GenerateMap();
		}
		if(Input.GetKeyDown(KeyCode.F)) {
			//flip normals
			Mesh mesh = GetComponent<MeshFilter>().mesh;
			mesh.triangles = mesh.triangles.Reverse().ToArray();
		}
	}

	void GenerateMap() {
		map = new int[width, height, depth];

		//RandomFillMap();
		//PerlinFillMap();
		//RidgeFillMap();
		BillowFillMap();

		for (int i = 0; i < smoothIterations; i++) {
			SmoothMap();
		}

		//TODO not working
		AddWallsToMap();

		float[,,] floatMap = new float[width, height, depth];
		for(int x = 0; x < width-1; x++) {
			for(int y = 0; y < height-1; y++) {
				for(int z = 0; z < depth-1; z++) {
					floatMap[x,y,z] = (float) map[x,y,z];
				}
			}
		}

		MarchingCubes.SetModeToCubes();
		Mesh mesh = MarchingCubes.CreateMesh(floatMap);
		mesh.name = "world";
		GetComponent<MeshFilter>().mesh = mesh;
		mesh.RecalculateNormals();

		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	void AddWallsToMap() {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				for(int z = 0; z < depth; z++) {
					if(x==0 || x == width-1 || y ==0 || y == height-1 || z == 0 || z == depth-1) {
						map[x,y,z] = 1;
					}
				}
			}
		}
	}

	void RandomFillMap() {
		if(useRandomSeed) {
			seed = Time.time.ToString() + transform.position.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				for(int z = 0; z < depth; z++) {
					if(x==0 || x == width-1 || y ==0 || y == height-1 || z == 0 || z == depth-1) {
						map[x,y,z] = 1;
					}
					map[x,y,z] = (pseudoRandom.Next(0,100) < randomFillPercent) ? 1 : 0;
				}
			}
		}
	}

	void PerlinFillMap() {
		if(useRandomSeed) {
			seed = Time.time.ToString() + transform.position.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		PinkNoise pink = new PinkNoise(pseudoRandom.Next(0,100));

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				for(int z = 0; z < depth; z++) {
					if(x==0 || x == width-1 || y ==0 || y == height-1 || z == 0 || z == depth-1) {
						map[x,y,z] = 1;
					} else {
						if(pink.GetValue(x,y,z) > 0.15 ) map[x,y,z] = 1;
						else map[x,y,z] = 0;
					}
				}
			}
		}
	}

	void RidgeFillMap() {
		if(useRandomSeed) {
			seed = Time.time.ToString() + transform.position.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		RidgeNoise ridge = new RidgeNoise(pseudoRandom.Next(0,100));

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				for(int z = 0; z < depth; z++) {
					if(x==0 || x == width-1 || y ==0 || y == height-1 || z == 0 || z == depth-1) {
						map[x,y,z] = 1;
					} else {
						Debug.Log(ridge.GetValue(x,y,z));
						if(ridge.GetValue(x,y,z) > 1.55f ) map[x,y,z] = 1;
						else map[x,y,z] = 0;
					}
				}
			}
		}
	}

void BillowFillMap() {
		if(useRandomSeed) {
			seed = Time.time.ToString() + transform.position.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		BillowNoise billow = new BillowNoise(pseudoRandom.Next(0,100));

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				for(int z = 0; z < depth; z++) {
					if(x==0 || x == width-1 || y ==0 || y == height-1 || z == 0 || z == depth-1) {
						map[x,y,z] = 1;
					} else {
						Debug.Log(billow.GetValue(x,y,z));
						if(billow.GetValue(x,y,z) > -1.1f ) map[x,y,z] = 1;
						else map[x,y,z] = 0;
					}
				}
			}
		}
	}
	

	void SmoothMap() {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				for(int z = 0; z < depth; z++) {
					int neighboringWalls = GetSurroundingWallCount(x,y,z);
					if(neighboringWalls > maxSmoothWalls) {
						map[x,y,z] = 1;
					} else if (neighboringWalls < minSmoothWalls) {
						map[x,y,z] = 0;
					}
				}
			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY, int gridZ) {
		int wallCount = 0;

		for(int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++) {
			for(int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++) {
				for(int neighborZ = gridZ - 1; neighborZ <= gridZ + 1; neighborZ++) {
					if(neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height && neighborZ >= 0 && neighborZ < depth){
						if(neighborX != gridX || neighborY != gridY || neighborZ != gridZ) {
							wallCount += map[neighborX, neighborY, neighborZ];
						}
					} 
					else {
						wallCount++;
					} 
				}
			}
		}


		return wallCount;
	}

	void OnDrawGizmos() {
		/*if(map != null) {
			for (int x = 0; x < width; x++) {
				for(int y = 0; y < height; y++) {
					for(int z = 0; z < depth; z++) {
						if(z == currentGizmoDepth) {
							Gizmos.color = (map[x,y,z] == 1) ? Color.black : Color.white;
							Vector3 pos = new Vector3(-width/2 + x + .5f, -height/2 + y + .5f, -depth/2 + z + .5f);
							Gizmos.DrawCube(pos, Vector3.one);
						}
					}
				}
			}
		}*/
	}
}
