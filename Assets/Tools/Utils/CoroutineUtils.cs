using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForMouseClick : CustomYieldInstruction
{
    public override bool keepWaiting => !Input.GetMouseButtonDown(0);
}
