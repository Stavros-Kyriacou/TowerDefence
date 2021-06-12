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
    private Vector2 padding = new Vector2(5, 5);
    private Vector2 screenPos = new Vector2();
    private Turret target;
    private Camera mainCam;
    private Vector2 screenBounds;
    public RectTransform backGround;

    public Turret Target { get { return target; } set { target = value; } }
    public GameObject StatScreen { get { return statScreen; } }


    private void Start()
    {
        mainCam = Camera.main;
        statScreen.SetActive(false);
    }
    private void Update()
    {
        if (target == null)
        {
            return;
        }

        statScreen.SetActive(true);

        //calculate screen bounds based on size of stat screen
        screenBounds = new Vector3(Screen.width - backGround.rect.width, Screen.height - backGround.rect.height, mainCam.transform.position.z);

        //convert turret world pos to clamped screen position
        screenPos = mainCam.WorldToScreenPoint(target.transform.position);
        screenPos.x = Mathf.Clamp(screenPos.x, padding.x, screenBounds.x - padding.x);
        screenPos.y = Mathf.Clamp(screenPos.y, padding.y, screenBounds.y - padding.y);

        //set stat screen position
        statScreen.transform.position = screenPos;
    }
    public void UpdateStats()
    {
        damageText.text = string.Format($"{target.MinDamage} to {target.MaxDamage}");
        rangeText.text = target.AttackRange.ToString();
        atkSpeedText.text = target.AttacksPerSecond.ToString();
    }
}
