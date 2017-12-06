﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyAccumulator : MonoBehaviour, IEnergyMessenger
{
    public int charge;
    public int maxCharge;
    public Slider laserBar;
    private float currentChargePercent;
    private float targetChargePercent;

    void Start()
    {
        charge = 0;
        currentChargePercent = targetChargePercent = 0f;
    }

    public void GainEnergy()
    {
        if (charge + 1 <= maxCharge)
        {
            currentChargePercent = (float)charge / maxCharge;
            targetChargePercent = (float)(++charge) / maxCharge;
        }
    }
    void Update()
    {
        if (currentChargePercent < targetChargePercent)
        {
            //smoothly move laser bar up to current charge
            currentChargePercent += ((float)charge / maxCharge) * Time.deltaTime;
            laserBar.value = currentChargePercent;
        }
    }
}