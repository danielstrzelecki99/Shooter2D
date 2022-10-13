using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Animator animator;
    int CurrentWeaponNo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        if (CurrentWeaponNo == 0)
        {
            CurrentWeaponNo += 1;
            animator.SetLayerWeight(CurrentWeaponNo - 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", true);
        }
        else
        {
            CurrentWeaponNo -= 1;
            animator.SetLayerWeight(CurrentWeaponNo + 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", false);
        }

    }
}
