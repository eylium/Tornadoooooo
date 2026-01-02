using UnityEngine;

public static class ValueManager
{

    public static bool IsPullingStrongly { get; set; } = false;
    //public static bool IsLoosingHealth { get; set; } = false;
    //public static bool GainedEnergy { get; set; } = false;
    //public static bool IsDead { get; set; } = false;

    //public static int StarCounter { get; set; }
    //public static bool IsThrown { get; set; }

    public static bool HasExploded;

    //player
    public static Vector3 WorldMousePosition { get; set; }

    //destruction
    public static int DestructionCounter { get; set; }
    public static int MaxDestruction { get; set; } = 150;

    //size
    public static float SizeCounter { get; set; }
    public static float PlayerSize { get; set; }

    public static bool GameHasEnded {  get; set; }

    public static Collider embeddedCollider { get; set; }

    public static float Timer { get; set; }
}
