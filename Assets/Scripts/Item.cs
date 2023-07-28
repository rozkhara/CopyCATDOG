using UnityEngine;

public class Item : MonoBehaviour
{
    public int ItemIndex;

    private void OnTriggerEnter2D(Collider2D ObtainPlayer)
    {
        Controller ObtainPlayer1Info = ObtainPlayer.GetComponent<Player1>();
        Controller ObtainPlayer2Info = ObtainPlayer.GetComponent<Player2>();

        if (ObtainPlayer.gameObject.CompareTag("Player1") && !ObtainPlayer1Info.Flowed)
        {
            ObtainItem(ObtainPlayer1Info);
            SoundManager.Instance.PlaySFXSound("Pickup", 1f);

        }
        else if(ObtainPlayer.gameObject.CompareTag("Player2") && !ObtainPlayer2Info.Flowed)
        {
            ObtainItem(ObtainPlayer2Info);
            SoundManager.Instance.PlaySFXSound("Pickup", 1f);
        }
    }

    private void ObtainItem(Controller ObtainPlayerInfo)
    {
        if (ItemIndex == 0)
        {
            ObtainPlayerInfo.CurrentSpeed += 1;
            ObtainPlayerInfo.PlayerSpeedInit += 1;
        }
        else if (ItemIndex == 1)
        {
            ObtainPlayerInfo.PlayerRange += 1;
        }
        else
        {
            ObtainPlayerInfo.MaxBomb += 1;
        }

        Destroy(gameObject);
    }
}
