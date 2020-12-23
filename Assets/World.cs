using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public Material textureAtlas;
	public int width = 16;
    public int depth = 16;
	public static int chunkSize = 16;
	public static Dictionary<string, Chunk> chunks;

	public static string BuildChunkName(Vector3 v)
	{
		return (int)v.x + "_" + 
			         (int)v.y + "_" + 
			         (int)v.z;
	}

	IEnumerator BuildChunkColumn()
	{
		for(int i = 0; i < width; i++)
		{
            for(int j = 0; j < depth; j++)
            {
                Vector3 chunkPosition = new Vector3(i * chunkSize,
                                                this.transform.position.y,
                                                j * chunkSize);
                Chunk c = new Chunk(chunkPosition, textureAtlas);
                c.chunk.transform.parent = this.transform;
                chunks.Add(c.chunk.name, c);
            }
		}

		foreach(KeyValuePair<string, Chunk> c in chunks)
		{
			c.Value.DrawChunk();
			yield return null;
		}
		
	}

	// Use this for initialization
	void Start () {
		chunks = new Dictionary<string, Chunk>();
		this.transform.position = Vector3.zero;
		this.transform.rotation = Quaternion.identity;
		StartCoroutine(BuildChunkColumn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
