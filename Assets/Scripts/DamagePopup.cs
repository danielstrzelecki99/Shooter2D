using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = .7f; //timer for disappear popup
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
    public void Setup(int damageAmount)
    {
        damageText.SetText(damageAmount.ToString());
        
        textColor = damageText.color;
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
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //Second half of the popup lifetime
            float descreaseScaleAmount = .1f;
            transform.localScale -= Vector3.one * descreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            //Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            damageText.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
    //Create a Damage Popup
    public static DamagePopup Create(Vector3 postion, int damageAmount)
    {
        Debug.Log("Printing damage");
        Debug.Log($"GameAssets: {GameAssets.i.pfDamagePopup}");
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, postion, Quaternion.identity);
        Debug.Log($"damagePopupTransform: {damagePopupTransform}");
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        Debug.Log($"DamagePopup: {damagePopup}");
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }
    
}
