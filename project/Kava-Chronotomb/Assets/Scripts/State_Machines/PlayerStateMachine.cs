using System.Collections;
using System.Collections.Generic;

public class PlayerStateMachine
{
    // Rules for changing STATE
    private Dictionary<Constants.States, List<Constants.States>> validStateTransitions = new Dictionary<Constants.States, List<Constants.States>>()
    {
        { Constants.States.MAIN, 
            new List<Constants.States>() {   
                Constants.States.BLUE   
            }
        },
        { Constants.States.BLUE, 
            new List<Constants.States>() {   
                Constants.States.MAIN,
                Constants.States.RED,
            }
        },
        { Constants.States.RED, 
            new List<Constants.States>() {   
                Constants.States.MAIN,
                Constants.States.BLUE,
            }
        }
    };

    // Rules for changing SUBSTATE. Every STATE needs its own set of substates and substates rules
    // Form: Dictionary[STATE][SUBSTATE] contains list of all possible next substates
    private Dictionary<Constants.States, Dictionary<Constants.SubStates, List<Constants.SubStates>>> validSubStateTransitions = new Dictionary<Constants.States, Dictionary<Constants.SubStates, List<Constants.SubStates>>>()
    {
        // Main state
        { Constants.States.MAIN,
            new Dictionary<Constants.SubStates, List<Constants.SubStates>>() {
                { Constants.SubStates.IDLE, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE_ATTACK,   
                        Constants.SubStates.IDLE_JUMP,   
                        Constants.SubStates.STAGGERED,   
                        Constants.SubStates.WALK,
                    }
                },
                { Constants.SubStates.IDLE_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                    }
                },
                { Constants.SubStates.IDLE_JUMP, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                        Constants.SubStates.JUMP_ATTACK,
                    }
                },
                { Constants.SubStates.JUMP_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE_JUMP,
                        Constants.SubStates.WALK_JUMP,
                    }
                },
                { Constants.SubStates.STAGGERED, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                    }
                },
                { Constants.SubStates.WALK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                        Constants.SubStates.WALK_ATTACK,
                        Constants.SubStates.WALK_JUMP,
                    }
                },
                { Constants.SubStates.WALK_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.WALK,
                    }
                },
                { Constants.SubStates.WALK_JUMP, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.JUMP_ATTACK,
                        Constants.SubStates.WALK,
                    }
                },
            }
        },
        // Blue state
        { Constants.States.BLUE,
            new Dictionary<Constants.SubStates, List<Constants.SubStates>>() {
                { Constants.SubStates.IDLE, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE_ATTACK,   
                        Constants.SubStates.IDLE_JUMP,   
                        Constants.SubStates.STAGGERED,   
                        Constants.SubStates.WALK,
                    }
                },
                { Constants.SubStates.IDLE_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                    }
                },
                { Constants.SubStates.IDLE_JUMP, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                        Constants.SubStates.JUMP_ATTACK,
                    }
                },
                { Constants.SubStates.JUMP_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE_JUMP,
                        Constants.SubStates.WALK_JUMP,
                    }
                },
                { Constants.SubStates.STAGGERED, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                    }
                },
                { Constants.SubStates.WALK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                        Constants.SubStates.WALK_ATTACK,
                        Constants.SubStates.WALK_JUMP,
                    }
                },
                { Constants.SubStates.WALK_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.WALK,
                    }
                },
                { Constants.SubStates.WALK_JUMP, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.JUMP_ATTACK,
                        Constants.SubStates.WALK,
                    }
                },
            }
        },
        // Red state
        { Constants.States.RED,
            new Dictionary<Constants.SubStates, List<Constants.SubStates>>() {
                { Constants.SubStates.IDLE, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE_ATTACK,   
                        Constants.SubStates.IDLE_JUMP,   
                        Constants.SubStates.STAGGERED,   
                        Constants.SubStates.WALK,
                    }
                },
                { Constants.SubStates.IDLE_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                    }
                },
                { Constants.SubStates.IDLE_JUMP, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                        Constants.SubStates.JUMP_ATTACK,
                    }
                },
                { Constants.SubStates.JUMP_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE_JUMP,
                        Constants.SubStates.WALK_JUMP,
                    }
                },
                { Constants.SubStates.STAGGERED, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                    }
                },
                { Constants.SubStates.WALK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.IDLE,
                        Constants.SubStates.WALK_ATTACK,
                        Constants.SubStates.WALK_JUMP,
                    }
                },
                { Constants.SubStates.WALK_ATTACK, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.WALK,
                    }
                },
                { Constants.SubStates.WALK_JUMP, 
                    new List<Constants.SubStates>() {   
                        Constants.SubStates.JUMP_ATTACK,
                        Constants.SubStates.WALK,
                    }
                },
            }
        }
    };

    public Constants.States ChangeState(Constants.States oldState, Constants.States newState){
        if (validStateTransitions[oldState].Contains(newState)) {
            return newState;
        }
        return oldState;
    }

    public Constants.SubStates ChangeSubState(Constants.States oldState, Constants.SubStates oldSubState, Constants.SubStates newSubState) {        
        if (validSubStateTransitions[oldState][oldSubState].Contains(newSubState)) {
            return newSubState;
        }
        return oldSubState;
    }
}
