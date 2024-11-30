using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Caller
{

    public string name;
    public string greetingLine;
    public string cluelessLine;
    public Dictionary<string, Monologue> monologues;

    public Caller(string name, string greetingLine, string cluelessLine, Dictionary<string, Monologue> monologues)
    {
        this.name = name;
        this.greetingLine = greetingLine;
        this.monologues = monologues;
        this.cluelessLine = cluelessLine;
    }

    public void CheckCluesIntegrity()
    {
        bool integrityFail = false;
        foreach ((string k, Monologue m)  in monologues)
        {
            // If monologue isnt a ring event
            if (!m.hungUpOnMonoligueEnd)
            {
                if (!CluesDefinitions.clueDefs.ContainsKey(k))
                {
                    integrityFail = true;

                    Debug.Log(string.Format("Integrity fail for '{0}' with monologue '{1}'", name, k));
                }
            }
        }

        if (integrityFail)
            throw new System.Exception("Caller Integrity Fail");
    }
}

public class Monologue
{

    public List<string> lines;
    public List<string> cluesGotOnMonologueEnd;
    public List<string> contactsGotOnMonologueEnd;

    public List<RingEvent> ringEventsOnMonologueEnd;
    public bool hungUpOnMonoligueEnd;

    public Monologue(List<string> lines, List<string> cluesGotOnMonologueEnd, List<RingEvent> ringEventsOnMonologueEnd
    , List<string> contactsGotOnMonologueEnd, bool hungUpOnMonoligueEnd = false)
    {
        this.lines = lines;
        this.cluesGotOnMonologueEnd = cluesGotOnMonologueEnd;
        this.ringEventsOnMonologueEnd = ringEventsOnMonologueEnd;
        this.contactsGotOnMonologueEnd = contactsGotOnMonologueEnd;
        this.hungUpOnMonoligueEnd = hungUpOnMonoligueEnd;
    }
}


public class RingEvent
{
    public int callsToWait;
    public float secondsAfterCallsToWait;
    public string caller;
    public string monologueIdentifier;
    public bool isRealised = false;

    public RingEvent(int callsToWait, float secondsAfterCallsToWait, string caller, string monologueIdentifier)
    {
        this.callsToWait = callsToWait;
        this.secondsAfterCallsToWait = secondsAfterCallsToWait;
        this.caller = caller;
        this.monologueIdentifier = monologueIdentifier;
    }
}