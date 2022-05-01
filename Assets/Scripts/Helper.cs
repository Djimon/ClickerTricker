using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERarity
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    VeryRare = 3,
    Exotic = 4,
    Epic = 5,
    Legendary = 6,
    Unique = 7
}

public enum ECardEffect
{
    AddGain,
    AddGrowthRate,
    Tower,
    Shield
}

public enum ETowerType
{
    none = -1,
    SingleTarget,
    Slowing,
    AoE
}

public enum EColorNames
{
    White = 0,
    Gray = 1,
    Cyan = 2,
    Blue = 3,
    Yellow = 4,
    Orange = 5,
    Magenta = 6,
    Violet = 7
}

public static class C
{
    private static Hashtable ColorValues = new Hashtable
    {
        {EColorNames.White, new Color(1,1,1) },                   // 0 - common = white
        {EColorNames.Gray, new Color(0.8f,0.8f,0.8f) },          // 1 - uncommon = gray
        {EColorNames.Cyan, new Color(100f/255f,230f/255f,230f/255f) }, // 2 - rare = cyan
        {EColorNames.Blue, new Color(100f/255f,150f/255f,230f/255f) }, // 3 - VeryRare   = blue
        {EColorNames.Yellow, new Color(235f/255f,215f/255f,25f/255f) },  // 4 - Exotic = yellow
        {EColorNames.Orange, new Color(235f/255f,100f/255f,25f/255f) },  // 5 - Epic = orange
        {EColorNames.Magenta, new Color(165f/255f,0,85f/255f) },        // 6 - Legendary = magenta
        {EColorNames.Violet, new Color(65f/255f,0,115f/255f) }         // 7 - Unqiue = violet
    };

    public static Color ColValue (EColorNames cname)
    {
        return (Color) ColorValues[cname];
    }

    public static Color ColValue(int n)
    {
        return (Color)ColorValues[(EColorNames) n];
    }

}

public static class Helper 
{
    /*
     Common      = 0,
    Uncommon    = 1,
    Rare        = 2,
    VeryRare    = 3,
    Exotic      = 4,
    Epic        = 5,
    Legendary   = 6,
    Unique      = 7
     */
    public static Color[] RarityColor = new Color[]
    {
        
    };


    internal static int NSize(float n)
    {
        return (int)Mathf.Log10(Mathf.Abs(n)) / 3;
    }

    internal static string AutoFormatNumber(float n)
    {
        int size = NSize(n);
        if (size <= -8)
            return "0";
        //Debug.Log(n + " -> NSize=" + size);
        switch(size)
        {
            //SMALL NUMBERS
            case 0:
                if(n>=1)
                    return n.ToString("N");
                else
                    return (n / Mathf.Pow(10, 3 * (size -1))).ToString("#,###.##") + "m";
            case -1:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "μ";
            case -2:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "n";
            case -3:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "p";
            case -4:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "f";
            case -5:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "a";
            case -6:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "z";
            case -7:
                return (n / Mathf.Pow(10, 3 * (size - 1))).ToString("#,###.##") + "y";

            //LARGE NUMBERS
            case 1: 
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##")+ "k";
            case 2:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "M";
            case 3:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "B";
            case 4:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "T";
            case 5:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Qa";
            case 6:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Qi";
            case 7:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Sx";
            case 8:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Sp";
            case 9:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Oc";
            case 10:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Nn";
            case 11:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Dc";
            case 12:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "UDc";
            case 13:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "DDc";
            case 14:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "TDc";
            case 15:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "QaDc";
            case 16:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "QiDc";
            case 17:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "SxDc";
            case 18:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "SpDc";
            case 19:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "OcDc";
            case 20:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "NvDc";
            case 21:
                return (n / Mathf.Pow(10, 3 * size)).ToString("#,###.##") + "Vig";
            default: return n.ToString("N");
        }
    }

    
}
