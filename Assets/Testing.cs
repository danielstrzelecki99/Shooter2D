using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform pfdamagePopup;

    //private void Start()
    //{
    //    Transform damagePopupTransform = Instantiate(pfdamagePopup, new Vector3(21, 3, 0), Quaternion.identity);
    //    DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
    //    damagePopup.Setup(300);
    //}
    void Start()
    {
        //DamagePopup.Create(new Vector3(21, 3, 0), 300);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //DamagePopup.Create(cursorPos, 100);
        }
    }
}
