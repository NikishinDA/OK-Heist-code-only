using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingWaypointController : WaypointController
{
    public override void AgentReached(AgentController agent)
    {
        agent.gameObject.SetActive(false);
    }
}
