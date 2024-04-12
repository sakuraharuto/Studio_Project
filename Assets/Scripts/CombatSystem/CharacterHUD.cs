using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : UIBase
{
    [Header("HUD Setting")]
    public Slider healthBarSlider;
    public Image shieldIcon;
    public TMP_Text shieldCount;

    public Sprite hasShield;
    public Sprite shieldBroken;

    private CombatUnit unit;

    // Start is called before the first frame update
    void Start()
    {   
        unit = gameObject.GetComponent<CombatUnit>();
        // Assign UI objects
        healthBarSlider = gameObject.transform.GetChild(0).GetComponent<Slider>();
        shieldIcon = gameObject.transform.GetChild(1).GetComponent<Image>();
        shieldCount = gameObject.transform.GetChild(2).GetComponent<TMP_Text>();

        shieldIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateShieldIcon();
    }

    public void UpdateHealthBar()
    {
        healthBarSlider.value = unit.currentHP;
    }

    public void UpdateShieldIcon()
    {
        //Sprite currentSprite = Resources.Load<Sprite>("Images/Combat/icon/hasShield");
        //shieldIcon.sprite = currentSprite;

        if (unit.currentShield > 0)
        {
            shieldIcon.gameObject.SetActive(true);

            shieldIcon.sprite = hasShield;

            shieldCount.text = unit.currentShield.ToString();
        }
        else
        {
            shieldCount.text = "";
        }

        
    }
}
