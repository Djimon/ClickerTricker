using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{

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
