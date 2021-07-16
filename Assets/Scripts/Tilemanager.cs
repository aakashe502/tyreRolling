using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemanager : MonoBehaviour
{
    public GameObject[] tilesPrefabs;
    public float zSpawn=0;
    public float tilelength=30;
    public int numberoftiles=5;
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<numberoftiles;i++){
            if(i==0)
            SpawnTile(0);
            else
            SpawnTile(Random.Range(0,tilesPrefabs.Length));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z-35>zSpawn-numberoftiles*tilelength){
            SpawnTile(Random.Range(0,tilesPrefabs.Length));
            DeleteTile();
        }
        
    }
    public void SpawnTile(int tileindex)
    {
        GameObject go = Instantiate(tilesPrefabs[tileindex],transform.forward*zSpawn,transform.rotation);
        activeTiles.Add(go);
        zSpawn+=tilelength;

    }
    private void DeleteTile(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
