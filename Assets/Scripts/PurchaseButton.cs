using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class PurchaseButton : MonoBehaviour
{
    public GameObject TowerPrefab;
    [HideInInspector]
    public int PurchaseCost=1;
    AudioSource source;
    private Button button;
    private TextMeshProUGUI cost;
    private Image icon;
    private Image bg;
    void Start()
    {
        PurchaseCost = TowerPrefab.GetComponent<TowerBehavior>().PurchaseCost;
        source = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        bg= GetComponent<Image>();
        cost = GetComponentInChildren<TextMeshProUGUI>();
        icon = transform.GetChild(0).GetComponent<Image>();

    }
    public void UpdateIcon()
    {
        button.interactable = MoneyManager.instance.Balance >= PurchaseCost&&TowerPrefab!=null;
        if(!button.interactable&& (TowerPlacer.SelectedTower == this))
        {
            TowerPlacer.SelectedTower = null;
            EventSystem.current.SetSelectedGameObject(null);
        }
        cost.text = PurchaseCost + "$";
        icon.sprite = TowerPrefab.GetComponent<TowerBehavior>().PurchaseIcon;
        icon.color = button.targetGraphic.canvasRenderer.GetColor();
        if (TowerPlacer.SelectedTower == this)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void ToggleSelected()
    {
        if(source!=null&& source.enabled)
        {
            source.Play();
        }
        if (TowerPlacer.SelectedTower == this)
        {
            TowerPlacer.SelectedTower = null;
            EventSystem.current.SetSelectedGameObject(null);

        }
        else
        {
            TowerPlacer.SelectedTower = this;
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    void Update()
    {
        UpdateIcon();
    }
}
