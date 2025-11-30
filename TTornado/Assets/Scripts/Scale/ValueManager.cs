using UnityEngine;

public static class ValueManager
{

    public static bool IsPullingStrongly { get; set; } = false;
    public static bool IsLoosingHealth { get; set; } = false;
    public static bool GainedEnergy { get; set; } = false;
    public static bool IsDead { get; set; } = false;

    public static int StarCounter { get; set; }
    public static bool IsThrown { get; set; }

    public static bool IsCounted;

    public static Vector3 WorldMousePosition { get; set; }

}
