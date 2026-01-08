using UnityEngine;

public static class ValueManager
{

    public static bool IsPullingStrongly { get; set; } = false;

    public static bool HasExploded = false;

    //player
    public static Vector3 WorldMousePosition { get; set; }

    //destruction
    public static int DestructionCounter { get; set; } = 0;
    public static int MaxDestruction { get; set; } = 300;

    //size
    public static float SizeCounter { get; set; } = 0;

    public static bool GameHasEnded { get; set; } = false;

    public static float Timer { get; set; } = 0;


    public static float MovementSpeed { get; set; } = 10;
}
