using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1f; //timer for disappear popup
    private Vector3 moveVector;
    private static int sortingOrder; //variable for displaying last popup on top
    
    private TextMeshPro damageText;
    private float disappearTimer;
    private Color textColor;
    // Start is called before the first frame update
    void Awake()
    {
        damageText = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount, bool isCriticalHit)
    {
        damageText.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            //Normal hit
            damageText.fontSize = 32;
            textColor = new Color(255, 197, 0, 255);
        }
        else
        {
            //Critical hit
            damageText.fontSize = 38;
            textColor = new Color(255, 43, 0, 255);
        }
        damageText.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        damageText.sortingOrder = sortingOrder;
        moveVector = new Vector3(1.5f, 2) * 10f;
    }
    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 10f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            //First half of the popup lifetime
            float increaseScaleAmount = .1f;
            transform.localScale += increaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        else
        {
            //Second half of the popup lifetime
            float descreaseScaleAmount = .1f;
            transform.localScale -= descreaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            //Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            damageText.color = textColor;
            Destroy(gameObject);
        }
    }
    
    //Create a Damage Popup
    public static DamagePopup Create(Vector3 postion, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, postion, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }
}
