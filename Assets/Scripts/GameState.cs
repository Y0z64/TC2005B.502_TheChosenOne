using System;

[Serializable]
public class GameState
{
    public PlayerState playerState;
    public DynamicObjectState dynamicObjectState;
}

[Serializable]
public class PlayerState
{
    public float[] position;
    public float[] rotation;
    public int teleportCount;
}

[Serializable]
public class DynamicObjectState
{
    public bool objectExists;
    public float[] objectPosition;
    public float[] objectRotation;
}
