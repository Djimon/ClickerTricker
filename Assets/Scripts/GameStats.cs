using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{

    //Magic Constants
    //Board-Variables
    internal static int imaxBoardLevel = 5; //ToDO: Beim SpielStart ermitteln?

    internal static float flevelUpStartPrice = 10;
    internal static float fLevelUpPriceMultiplier =2;

    internal static float fIncreaseGlobalGainPrice = 10;
    internal static int iIncreasePointsglobally = 1;
    internal static int iInCreaseGainLevelUpMultiplier = 2;
    internal static float fIncreaseGainPriceMultiplier = 2;

    internal static float fIncreaseGrowthRatePrice = 20;
    internal static float fGrowthRateMultiplier = 1.1f;
    internal static float fIncreaseGrowthPriceMultiplier = 2;

    //Ground Variables
    internal static float fFirstGroundPrice = 0;
    internal static float fSecondGroundPrice = 5f;
    internal static float fGroundPriceMultiplier = 1.5f;

    internal static GameObject GO_BaseGround;
    internal static bool hasBaseGround = false;
    internal static int iUpgradeBaseGroundPoints = 5;
    internal static float fUpgradeBaseGroundPrice = 50;
    internal static float fUpgradeBaseGroundPriceMultiplier = 1.5f;

    //Automation
    internal static float fAutomateGroundPrice = 250;
    internal static bool IsAutomationPreview = false;

    //Growth Variables
    internal static float fGrowthStartSpeed = 0.5f;
    internal static float iBaseGroundGrowthSpeed = 5f;
}
