using System;
using UnityEngine;

[Serializable] public struct Keys {
    public KeyCode moveForward;
    public KeyCode moveBackward;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode run;

    public Keys(KeyCode moveForward, KeyCode moveBackward, KeyCode moveLeft, KeyCode moveRight, KeyCode jump, KeyCode run) {
        this.moveForward = moveForward;
        this.moveBackward = moveBackward;
        this.moveLeft = moveLeft;
        this.moveRight = moveRight;
        this.jump = jump;
        this.run = run;
    }
}