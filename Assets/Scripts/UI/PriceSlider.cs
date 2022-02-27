using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceSlider : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Text priceText;
    private Slider priceSlider;

    private void Awake(){
        priceSlider = GetComponent<Slider>();
    }

    public void SetPrice(){
        inventory.SetPrice((int)priceSlider.value);
        priceText.text = "Price:\t    " + priceSlider.value.ToString();
    }
}
