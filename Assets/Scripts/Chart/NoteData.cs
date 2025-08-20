using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float time;     // 음악 기준 타이밍 (초)
    public int lane;       // 노트가 떨어질 레인
    public string type;    // "tap", "long" 등 (현재 tap만)
}

