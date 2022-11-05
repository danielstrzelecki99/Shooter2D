using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class BarsManager : MonoBehaviour
{
    public Image fillHpImage;
    public Image fillArmorImage;
    public TextMeshProUGUI hpInfo;
    public TextMeshProUGUI armorInfo;
    // PhotonView view;

    // private void Start() {
    //     view = GetComponent<PhotonView>();
    // }

    private void Update() {
            fillHpImage.fillAmount = PlayerHealth.slocalHealth;
            fillArmorImage.fillAmount = PlayerHealth.slocalArmor;
            hpInfo.text = Mathf.Round((PlayerHealth.slocalHealth * 100)).ToString() + "/100";
            armorInfo.text = Mathf.Round((PlayerHealth.slocalArmor * 100)).ToString() + "/100";
    }
}
