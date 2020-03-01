using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class StreamlineNetwork : NetworkManager
{
    public Transform playerSpawn;
    GameObject ball;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        //// spawn ball if two players
        //if (numPlayers == 2)
        //{
        //    ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        //    NetworkServer.Spawn(ball);
        //}
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
