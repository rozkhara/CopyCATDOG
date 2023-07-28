using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public readonly int[,] CharacterStats = new int[3, 4];
    // Column are in this order PlayerSpeed,PlayerRange,MaxBomb,BombExplosionRange

    public GameObject[] PlayerPrefab;

    public GameObject Player1Spawned;
    public GameObject Player2Spawned;

    //public GameObject[] Items;

    public int Player1Select = 1;
    public int Player2Select = 3;
    private Vector2 StartPosition1 = new(-7f, -4.3f);
    private Vector2 StartPosition2 = new(2.8f, 4.1f);

    // Start is called before the first frame update
    void Start()
    {
        //For Alpha
        CharacterStats[0, 0] = 5; // PlayerSpeed
        CharacterStats[0, 1] = 5; // PlayerRange
        CharacterStats[0, 2] = 5; // MaxBomb


        //For Beta
        CharacterStats[1, 0] = 4; // PlayerSpeed
        CharacterStats[1, 1] = 4; // PlayerRange
        CharacterStats[1, 2] = 4; // MaxBomb


        //For Gamma
        CharacterStats[2, 0] = 3; // PlayerSpeed
        CharacterStats[2, 1] = 3; // PlayerRange
        CharacterStats[2, 2] = 3; // MaxBomb


        CheckPlayer(PickCharacter.CurrentCharacter1p, PickCharacter.CurrentCharacter2p);

    }

    private void SpawnPlayers(GameObject Prefab, Vector2 Position, int playerIndex, int Stats)
    {
        GameObject player = Instantiate(Prefab, Position, Quaternion.identity);

        if (playerIndex == 1)
        {
            // Disable the Player2 script on the player object
            player.GetComponent<Player2>().enabled = false;

            // Get the Controller script and set the stats
            Player1 playerscript = player.GetComponent<Player1>();
            if (playerscript != null)
            {
                playerscript.SetCharacterStats(
                    CharacterStats[Stats - 1, 0],
                    CharacterStats[Stats - 1, 1],
                    CharacterStats[Stats - 1, 2]
                );
            }
            player.tag = "Player1";
            Player1Spawned = player;
        }
        else if (playerIndex == 2)
        {
            // Disable the Player1 script on the player object
            player.GetComponent<Player1>().enabled = false;

            // Get the Controller script and set the stats
            Player2 playerscript = player.GetComponent<Player2>();
            if (playerscript != null)
            {
                playerscript.SetCharacterStats(
                    CharacterStats[Stats - 1, 0],
                    CharacterStats[Stats - 1, 1],
                    CharacterStats[Stats - 1, 2]
                );
            }
            player.tag = "Player2";
            Player2Spawned = player;
        }
    }

    private void CheckPlayer(int Player1, int Player2)
    {
        switch (Player1)
        {
            case 1:
                SpawnPlayers(PlayerPrefab[0], StartPosition1, 1, 1);
                break;
            case 2:
                SpawnPlayers(PlayerPrefab[1], StartPosition1, 1, 2);
                break;
            case 3:
                SpawnPlayers(PlayerPrefab[2], StartPosition1, 1, 3);
                break;
            // Add more cases for other values of Player1 if needed
            default:
                break;
        }

        switch (Player2)
        {
            case 1:
                SpawnPlayers(PlayerPrefab[0], StartPosition2, 2, 1);
                break;
            case 2:
                SpawnPlayers(PlayerPrefab[1], StartPosition2, 2, 2);
                break;
            case 3:
                SpawnPlayers(PlayerPrefab[2], StartPosition2, 2, 3);
                break;
            // Add more cases for other values of Player2 if needed
            default:
                break;
        }
    }
}
