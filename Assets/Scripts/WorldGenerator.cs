using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour {

	public GameController gc;
	public GameObject mapGen3D;
	public GameObject gem;
	public GameObject player;
	public int numMaps;
	public float mapSize;
	public int gemsPerMap;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		GenerateMaps();
		gc.totalGems = numMaps * gemsPerMap;
	}

	void GenerateMaps() {
		for(int i = 0; i < numMaps; i++) {
			Vector3 newPos = GetNewPosition(i);
			GameObject newMap = GameObject.Instantiate(mapGen3D, newPos, Quaternion.identity) as GameObject;
			ChangeMapMaterial(newMap);
			PopulateMap(newMap);
			if(i == 0) {
				Debug.Log(newPos);
				player.transform.position = newPos + new Vector3(mapSize/2,mapSize/2,mapSize/2);
			}
		}
	}
	
	Vector3 GetNewPosition(int i) {
		float x = Random.Range(i * mapSize, i * mapSize * 2);
		float y = Random.Range(i * mapSize, i * mapSize * 2);
		float z = Random.Range(i * mapSize, i * mapSize * 2);

		Vector3 pos = new Vector3(x,y,z);
		return pos;
	}

	void ChangeMapMaterial(GameObject map) {
		MeshRenderer mr = map.GetComponent<MeshRenderer>();
		Material mat = mr.materials[0];
		Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
		mat.color = color;
		mr.materials[0] = mat;
	}

	void PopulateMap(GameObject map) {
		float gemsPlaced = 0;
		//MeshFilter mf = map.GetComponent<MeshFilter>();
		//Mesh mesh = mf.sharedMesh;
		//TODO why no mesh info?
		MapGenerator3D mapgen = map.GetComponent<MapGenerator3D>();
		Mesh mesh = mapgen.myMesh;
		while(gemsPlaced < gemsPerMap) {
			float x = Random.Range(mesh.bounds.min.x + 2f, mesh.bounds.max.x-2f) + map.transform.position.x;			
			float y = Random.Range(mesh.bounds.min.y + 2f, mesh.bounds.max.y-2f) + map.transform.position.y;			
			float z = Random.Range(mesh.bounds.min.z + 2f, mesh.bounds.max.z-2f) + map.transform.position.z;			

			Vector3 pos = new Vector3(x,y,z);
			if(!Physics.CheckSphere(pos, .5f)) {
				GameObject thisGem = GameObject.Instantiate(gem, pos, Quaternion.identity) as GameObject;
				gemsPlaced++;
			}
		}
	}
}
