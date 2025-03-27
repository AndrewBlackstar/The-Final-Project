using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class WheaponWheelButton : MonoBehaviour
{
    public int id;
    Animator animator;
    public Image selectedItem;
    bool isSelected = false;
    public Sprite icon;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected) 
        {
            selectedItem.sprite = icon;

        }
    }

    public void Selected()
    {
        isSelected = true;
        WeaponWheelController.weaponID = id;
    }

    public void Deselected()
    {
        isSelected = false;
        WeaponWheelController.weaponID = 0;
    }

    public void HoverEnter()
    {
        animator.SetBool("hover", true);
    }
    
    public void HoverExit()
    {
        animator.SetBool("hover", false);
    }



}
