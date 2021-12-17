public static class Constants
{
    public static ushort REC_LEN = 1200;
    
    public enum States {
        MAIN,
        RED,
        BLUE,
    }

    public enum SubStates {
        IDLE,
        WALK,
        IDLE_JUMP,
        WALK_JUMP,
        IDLE_ATTACK,
        WALK_ATTACK,
        JUMP_ATTACK,
        STAGGERED,
    }

    public enum Conditions {
        DAMAGED,
        SLOWED,
    }
    
    
}