public class Constants
{
    public const string PLAYER_DATA = "PlayerData";
}

public class PooledObjectTags
{
    public const string CUBE = "Cube";
    public const string CONFETTİ_VFX = "Confetti_VFX";
    public const string OBSTACLE = "Obstacle";
}
public class ObjectTags
{
    public const string CUBE = "Cube";
    public const string PLAYER_COLLECTOR = "PlayerCollector";
    public const string OPPONENT_COLLECTOR = "OpponentCollector";
    public const string COLLECT_MAGNET_AREA = "CollectMagnetArea";
    public const string COLLECTED_CUBE_ENTER_TRIGGER = "CollectedEnterTrigger";
    public const string COLLECTED_CUBE_EXIT_TRIGGER = "CollectedExitTrigger";
    public const string OBSTACLE = "Obstacle";
}
public enum PlayerStates
{
    IdleState = 0,
    RunState = 1,
    WinState = 2,
    FailState = 3
}

public enum PlayerCollectorStates
{
    IdleCollectorState = 0,
    RunCollectorState = 1,
    WinCollectorState = 2,
    FailCollectorState = 3
}
public enum OpponentCollectorStates
{
    IdleCollectorState = 0,
    CollectOpponentCollector = 1,
    PourOpponentCollector = 2,
    WinCollectorState = 3,
    FailCollectorState = 4,
}
public enum ObjectsLayer
{
    Default = 0,
    Cube = 6,
    CollectorBaseBody = 7,
    Border = 8,
    CollectMagnetArea = 9,
    CollectedCubeEnterTrigger = 10,
    PlayerCollector = 11,
    PlayerCollectedCube = 12,
    OpponentCollectedCube = 13,
    OpponentCollector = 14,
    PlayerMagnetArea = 15,
    OpponentMagnetArea = 16,
    CollectedCubeExitTrigger = 17,
    Obstacle = 18,

}

public enum CollectorType
{
    PlayerCollector,
    OpponentCollector,
}

public enum ListOperation
{
    Adding,
    Subtraction
}

public enum UIPanelType
{
    MainMenuPanel = 0,
    HudPanel = 1,
    FinishPanel = 2,
    CommonPanel = 3,
}
