using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelManager : MonoBehaviour
{
    [Header ("Software")]
    [SerializeField] private Slider swSlider;
    [SerializeField] private TextMeshProUGUI swMinPPU;
    [SerializeField] private TextMeshProUGUI swMaxPPU;
    [SerializeField] private TextMeshProUGUI swUnits;
    [SerializeField] private TextMeshProUGUI swCost;
    [Header ("Hardware")]
    [SerializeField] private Slider hwSlider;
    [SerializeField] private TextMeshProUGUI hwMinPPU;
    [SerializeField] private TextMeshProUGUI hwMaxPPU;
    [SerializeField] private TextMeshProUGUI hwUnits;
    [SerializeField] private TextMeshProUGUI hwCost;
    [Header ("Servers")]
    [SerializeField] private Slider svrSlider;
    [SerializeField] private TextMeshProUGUI svrMinPPU;
    [SerializeField] private TextMeshProUGUI svrMaxPPU;
    [SerializeField] private TextMeshProUGUI svrUnits;
    [SerializeField] private TextMeshProUGUI svrCost;
    [Header ("Ads")]
    [SerializeField] private Slider adSlider;
    [SerializeField] private TextMeshProUGUI adMinPPU;
    [SerializeField] private TextMeshProUGUI adMaxPPU;
    [SerializeField] private TextMeshProUGUI adUnits;
    [SerializeField] private TextMeshProUGUI adCost;
    [Header ("Total Cost")]
    [SerializeField] private TextMeshProUGUI totalCost;
    private Inventory iv;

    [Header ("Audio Clips")]
    [SerializeField] private AudioClip itemClip;

    private void Awake(){
        iv = GetComponent<Inventory>();
    }
    private void Start(){
        swSlider.wholeNumbers = true;
        hwSlider.wholeNumbers = true;
        svrSlider.wholeNumbers = true;
        adSlider.wholeNumbers = true;

        swSlider.value = iv.software;
        swSlider.minValue = 0;
        swMinPPU.text = swSlider.minValue.ToString();
        swSlider.maxValue = iv.softwareLimit;
        swMaxPPU.text = iv.softwareLimit.ToString();

        hwSlider.value = iv.hardware;
        hwSlider.minValue = 0;
        hwMinPPU.text = hwSlider.minValue.ToString();
        hwSlider.maxValue = iv.hardwareLimit;
        hwMaxPPU.text = iv.hardwareLimit.ToString();

        svrSlider.value = iv.servers;
        svrSlider.minValue = 1;
        svrMinPPU.text = svrSlider.minValue.ToString();
        svrSlider.maxValue = iv.serversLimit;
        svrMaxPPU.text = iv.serversLimit.ToString();

        adSlider.value = iv.ads;
        adSlider.minValue = 0;
        adMinPPU.text = adSlider.minValue.ToString();
        adSlider.maxValue = iv.adsLimit;
        adMaxPPU.text = iv.adsLimit.ToString();

        UpdateIngredients();
        svrUnits.text = svrSlider.value.ToString();
        iv.serverDiff = 1;
        iv.UpdateServers();
        svrCost.text = "   " + (iv.servers * iv.serversPPU).ToString();
        totalCost.text = "     " + iv.totalCost();
        SoundManager.instance.PlaySound(itemClip);
    }
    public void UpdateIngredients(){
        UpdateHardware();
        UpdateSoftware(); // think about priority (what needs atleast 1 to run)
        UpdateServers();
        UpdateAds();
        totalCost.text = "     " + iv.totalCost();
    }
    public void UpdateSoftware(){
        if(swSlider.value > iv.MaxSWInc()) swSlider.value = iv.MaxSWInc();
        swUnits.text = swSlider.value.ToString();
        iv.UpdateSoftware(Int32.Parse(swUnits.text));
        swCost.text = "   " + (iv.software * iv.softwarePPU).ToString();
        totalCost.text = "     " + iv.totalCost();
        SoundManager.instance.PlaySound(itemClip);
    }
    public void UpdateHardware(){
        if(hwSlider.value > iv.MaxHWInc()) hwSlider.value = iv.MaxHWInc();
        hwUnits.text = hwSlider.value.ToString();
        iv.UpdateHardware(Int32.Parse(hwUnits.text));
        hwCost.text = "   " + (iv.hardware * iv.hardwarePPU).ToString();
        totalCost.text = "     " + iv.totalCost();
        SoundManager.instance.PlaySound(itemClip);
    }
    public void UpdateServers(){
        if((int)svrSlider.value == iv.servers) return;
        int diff = (int)svrSlider.value - iv.serverDiff;
        if(diff > iv.SvrToBuy()) return;
        iv.ServersWillBuy += diff;
        svrUnits.text = svrSlider.value.ToString();
        iv.UpdateServers();
        Debug.Log("TC: " + (int)iv.totalCost());
        svrCost.text = "   " + (iv.ServersWillBuy * iv.serversPPU).ToString();
        totalCost.text = "     " + iv.totalCost();
        SoundManager.instance.PlaySound(itemClip);
    }
    public void UpdateAds(){
        if(adSlider.value > iv.MaxAdInc()) adSlider.value = iv.MaxAdInc();
        adUnits.text = adSlider.value.ToString();
        iv.UpdateAds(Int32.Parse(adUnits.text));
        adCost.text = "   " + (iv.ads * iv.adsPPU).ToString();
        totalCost.text = "     " + iv.totalCost();
        SoundManager.instance.PlaySound(itemClip);
    }
}