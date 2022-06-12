using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Enumeration
{
    public static GameState PLAY = new GameState(0, nameof(PLAY));
    public static GameState PAUSE = new GameState(1, nameof(PAUSE));
    public static GameState GAME_OVER = new GameState(2, nameof(GAME_OVER));
    public static GameState END_LEVEL = new GameState(3, nameof(END_LEVEL));
    public static GameState TRANSITION = new GameState(4, nameof(TRANSITION));
    public static GameState INSTRUCTION = new GameState(5, nameof(INSTRUCTION));

    public GameState(int id, string name) : base(id, name) { }

}
