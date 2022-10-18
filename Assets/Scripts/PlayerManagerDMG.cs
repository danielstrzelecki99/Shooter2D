using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManagerDMG : MonoBehaviour
{
    public static int PlayerHP = 100;
    public TextMeshProUGUI playerHPText;
    public static bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "+" + PlayerHP;
        if (isDead)
        {
            //spawn in a new place
        }
    }

    public static void TakeDamage(int damageAmount)
    {
        PlayerHP -= damageAmount;
        if (PlayerHP <= 0)
        {
            isDead = true;
        }
    }
}