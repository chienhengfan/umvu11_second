using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_UIControl : MonoBehaviour
{
    public float cdTime = 2.0f;
    private bool isCold = false;
    private Image imageUI;
    public KeyCode keyCode;

    private void Start()
    {
        imageUI = this.GetComponent<Image>();
        imageUI.fillAmount = 1.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode) && !isCold)
        {
            imageUI.fillAmount = 0f;
            isCold = true;
        }

        if (isCold)
        {
            imageUI.fillAmount += (1f / cdTime) * Time.deltaTime;
            if (imageUI.fillAmount >= 0.98f)
            {
                imageUI.fillAmount = 1.0f;
                isCold = false;
            }
        }
    }
}
