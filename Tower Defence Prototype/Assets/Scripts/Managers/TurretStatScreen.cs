using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurretStatScreen : MonoBehaviour
{
    [Header("Canvas Elements")]
    [SerializeField] private GameObject statScreen;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI atkSpeedText;
    [SerializeField] private TextMeshProUGUI sellPriceText;

    private Turret target;
    private Camera mainCam;

    public Turret Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }


    private void Start() {
        mainCam = Camera.main;
        // statScreen.SetActive(false);

    }
    private void Update() {
        if (target == null)
        {
            statScreen.SetActive(false);
            return;
        }
        statScreen.SetActive(true);
        Vector2 screenPos = mainCam.WorldToScreenPoint(target.transform.position);
        statScreen.transform.position = screenPos;

    }
    public void UpdateStats()
    {
        damageText.text = string.Format("({0}, {1})", target.MinDamage, target.MaxDamage);
        rangeText.text = target.AttackRange.ToString();
        atkSpeedText.text = target.AttacksPerSecond.ToString();
    }


}
