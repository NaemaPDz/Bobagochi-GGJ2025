public enum PetGrowState 
{ 
    Baby, 
    Teen, 
    Adult 
}

public enum PetType 
{ 
    Base, 
    Honey, 
    BrownSugar, 
    GreenTea 
}

public enum StatsType
{
    Hunger,
    Happiness,
    Cleanliness,
    Energy
}

public enum ActionType
{
    Feed,
    Play,
    Bath,
    Sleep
}

public enum AffectedActionType
{
    ForceAction,
    StopAction
}

public enum Scene
{
    Home,
    Gameplay,
    EndScene,
    ExitScene
}

public struct FinalPetData
{
    public PetType petType;

    public float sweetness;
    public float chewy;

    public float happiness;
    public float energy;
}

public static class AnimationStaticValue
{
    public static readonly string BOBA = "Boba";

    public static readonly string IDLE = "-idle";
    public static readonly string EAT = "-Eat";
    public static readonly string PLAY = "-Play";
    public static readonly string BATH = "-Shower";
    public static readonly string SLEEP = "-Sleep";

    public static readonly string HUNGRY = "-Hungry";
    public static readonly string STINKY = "-Stinky";
    public static readonly string SLEEPY = "-Sleepy";
}