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

    public readonly int[,] CharacterStats = new int[9, 6];
    // Column are in this order PlayerSpeed,PlayerRange,BombCount,BombExplosionRange

    public GameObject[] PlayerPrefab;

    public GameObject Player1Spawned;
    public GameObject Player2Spawned;

    //public GameObject[] Items;

    public int Player1Select = 1;
    public int Player2Select = 2;
    private Vector2 StartPosition1 = new(-7f, -4.3f);
    private Vector2 StartPosition2 = new(2.8f, 4.1f);

    // Start is called before the first frame update
    void Start()
    {
        //For Bazzi
        CharacterStats[0, 5] = 6; // MaxBomb
        CharacterStats[0, 4] = 7; // MaxRange
        CharacterStats[0, 3] = 9; // MaxSpeed
        CharacterStats[0, 2] = 1; // BombCount
        CharacterStats[0, 1] = 1; // PlayerRange
        CharacterStats[0, 0] = 5; // PlayerSpeed

        //For Dao
        CharacterStats[1, 5] = 10; // MaxBomb
        CharacterStats[1, 4] = 7; // MaxRange
        CharacterStats[1, 3] = 7; // MaxSpeed
        CharacterStats[1, 2] = 1; // BombCount
        CharacterStats[1, 1] = 1; // PlayerRange
        CharacterStats[1, 0] = 5; // PlayerSpeed

        //For Dizni
        CharacterStats[2, 5] = 7; // MaxBomb
        CharacterStats[2, 4] = 9; // MaxRange
        CharacterStats[2, 3] = 8; // MaxSpeed
        CharacterStats[2, 2] = 2; // BombCount
        CharacterStats[2, 1] = 1; // PlayerRange
        CharacterStats[2, 0] = 4; // PlayerSpeed

        //For Mos
        CharacterStats[3, 5] = 8; // MaxBomb
        CharacterStats[3, 4] = 5; // MaxRange
        CharacterStats[3, 3] = 8; // MaxSpeed
        CharacterStats[3, 2] = 1; // BombCount
        CharacterStats[3, 1] = 1; // PlayerRange
        CharacterStats[3, 0] = 5; // PlayerSpeed

        //For Uni
        CharacterStats[4, 5] = 6; // MaxBomb
        CharacterStats[4, 4] = 7; // MaxRange
        CharacterStats[4, 3] = 8; // MaxSpeed
        CharacterStats[4, 2] = 1; // BombCount
        CharacterStats[4, 1] = 2; // PlayerRange
        CharacterStats[4, 0] = 5; // PlayerSpeed

        //For Ethi
        CharacterStats[5, 5] = 10; // MaxBomb
        CharacterStats[5, 4] = 8; // MaxRange
        CharacterStats[5, 3] = 8; // MaxSpeed
        CharacterStats[5, 2] = 2; // BombCount
        CharacterStats[5, 1] = 1; // PlayerRange
        CharacterStats[5, 0] = 4; // PlayerSpeed

        //For Marid
        CharacterStats[6, 5] = 9; // MaxBomb
        CharacterStats[6, 4] = 6; // MaxRange
        CharacterStats[6, 3] = 8; // MaxSpeed
        CharacterStats[6, 2] = 2; // BombCount
        CharacterStats[6, 1] = 1; // PlayerRange
        CharacterStats[6, 0] = 4; // PlayerSpeed


        //For Kephi
        CharacterStats[7, 5] = 9; // MaxBomb
        CharacterStats[7, 4] = 8; // MaxRange
        CharacterStats[7, 3] = 8; // MaxSpeed
        CharacterStats[7, 2] = 1; // BombCount
        CharacterStats[7, 1] = 2; // PlayerRange
        CharacterStats[7, 0] = 4; // PlayerSpeed


        //For Su
        CharacterStats[8, 5] = 9; // MaxBomb
        CharacterStats[8, 4] = 7; // MaxRange
        CharacterStats[8, 3] = 10; // MaxSpeed
        CharacterStats[8, 2] = 2; // BombCount
        CharacterStats[8, 1] = 1; // PlayerRange
        CharacterStats[8, 0] = 6; // PlayerSpeed

        SoundManager.Instance.PlayBGMSound(0.3f);

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
                    CharacterStats[Stats - 1, 2],
                    CharacterStats[Stats - 1, 3],
                    CharacterStats[Stats - 1, 4],
                    CharacterStats[Stats - 1, 5]
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
                    CharacterStats[Stats - 1, 2],
                    CharacterStats[Stats - 1, 3],
                    CharacterStats[Stats - 1, 4],
                    CharacterStats[Stats - 1, 5]
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
                SpawnPlayers(PlayerPrefab[0], StartPosition1, 1, Player1Select);
                break;
            case 2:
                SpawnPlayers(PlayerPrefab[0], StartPosition1, 1, Player1Select);
                break;
            // Add more cases for other values of Player1 if needed
            default:
                break;
        }

        switch (Player2)
        {
            case 1:
                SpawnPlayers(PlayerPrefab[1], StartPosition2, 2, Player2Select);
                break;
            case 2:
                SpawnPlayers(PlayerPrefab[1], StartPosition2, 2, Player2Select);
                break;
            // Add more cases for other values of Player2 if needed
            default:
                break;
        }
    }

}
