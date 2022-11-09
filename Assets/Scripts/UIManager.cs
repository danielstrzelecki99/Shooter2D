using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI iteractInfo;
    [SerializeField] TextMeshProUGUI armorText;
    [SerializeField] TextMeshProUGUI firstAidText;
    [SerializeField] TextMeshProUGUI itemInfo;
    PhotonView view;

    void Update(){
            iteractInfo.text = PlayerMovement.interactInfoText;
            armorText.text = "[G] " + PlayerEq.armorAmount.ToString();
            firstAidText.text = "[F] " +  PlayerEq.aidKitAmount.ToString();
            if(ItemsManager.interval > 0){
                itemInfo.text = ItemsManager.itemInfoText;
                ItemsManager.interval -= Time.deltaTime;
            } else {
                itemInfo.text = "";
            }
    }
}
