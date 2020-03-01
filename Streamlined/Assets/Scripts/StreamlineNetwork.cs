using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class StreamlineNetwork : NetworkManager
{
    public GameObject jukebox;
    public Transform playerSpawn;
    GameObject ball;

    bool hasCannons = false;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        //// spawn ball if two players
        if (numPlayers == 1 && !hasCannons)
        {
            hasCannons = false;

            var cannon1 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Cannon"), new Vector3(-3, 5), Quaternion.identity);
            var cannon2 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Cannon"), new Vector3(0, 5), Quaternion.identity);
            var cannon3 = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Cannon"), new Vector3(3, 5), Quaternion.identity);

            NetworkServer.Spawn(cannon1);
            NetworkServer.Spawn(cannon2);
            NetworkServer.Spawn(cannon3);

            cannon1.GetComponent<CannonControl>().InvokeRepeating("Shoot", 0.0f, 2.5f);
            cannon2.GetComponent<CannonControl>().InvokeRepeating("Shoot", 2.5f/3f, 2.5f);
            cannon3.GetComponent<CannonControl>().InvokeRepeating("Shoot", 2.5f*2/3f, 2.5f);

            //jukebox.GetComponent<AudioSource>().mute = false;
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        //if (ball != null)
        //    NetworkServer.Destroy(ball);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
