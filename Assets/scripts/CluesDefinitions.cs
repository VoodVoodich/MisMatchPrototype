
using System.Collections;
using System.Collections.Generic;


class CluesDefinitions
{
    public static Dictionary<string, string> clueDefs = new Dictionary<string, string>{
        {"Empty", "Development process clue, how did you get it?"},
        {"Game end", "Development clue for marking game over"},
        {"The case", "There is a murder in town, time to investigate!"},

        {"Massive burns", "The victim has massive burns on his body. The most probable cause of death."},
        {"Weak body", "The victim is less than average in physical strength."},
        {"Autopsy in progress", "The victim's corpse was delivered only recently and the department wasn't at all prepare to run a full autopsy, only basic checks were run."},

        {"Matches factory", "Place of murder is the town's matches factory."},
        {"Explosion at the spot", "The murder scene clearly had an explosion at the moment of murder"},
        {"Just a worker", "Victim is the ordinary factory worker."},

        {"Witness 2", "Guard was at post when explosion happened."},
        {"Witness 1", "An old time factory worker stayed late at the day of the murder"},
        {"Old grudges", "Witness 1 holds a grudge on every coworker because of a past incident"},
        {"Slacking worker", "Witness 2 is reported to often sleep at night shifts and other times as well."},

        {"Death from explosion", "Most probably victim was chosen to be killed with an explosion."},
        {"Lonely cop", "The police is the only service at the spot right now and missed other servises alltogether. Larry is sad and lonely because of that."},


        {"Greedy Boss", "The factory boss is cutting costs on safety measures and there is an iternal conflict because of that."},
        {"A Boss Rumour", "The factory boss cutting costs is a rumour."},

        {"Victim and the Boss tension", "Boss and the victim had a prior history of arguing about safety conditions."},
        {"Boss suspect", "The factory boss was also at the factory at the night of the murder and is a possible suspect. The motive is the conflict on cutting safety measures costs"},

        
        {"Boss and the victim convo exploaded", "Victim and the boss entered the storage room and after the heated argument explosion occured"},

        {"Missed the important part", "Greg overslept the time period of victim and the boss being in the storage room"},
        {"Filled gaps", "Braun took liberty to fill some gaps and add some reliable rumours in his theory"},

        {"Work mess", "Overall workflow of the police is extremely unorganised for this case"},
        {"Boss disappeared", "After the explosion boss was nowhere to be seen and his whereabouts are unknown"},
        
        {"Boss is the killer", "The boss killed the victim during a heated argument at the storage room with an explosion, then disappeared with no trace."}
,
        {"Living dead", "It appears it is not a murder, but it is still may be a crime. #Dead end of related story branch#"},
        {"Cost Effective Boss", "The factory boss is not cutting costs and the safety measures conflict isn't a big one. #Dead end of related story branch#"},
        {"Victim and the Boss connection", "Boss and the victim had a prior history of talking to each other, not necessarily due to a conflict. #Dead end of related story branch#"},
        {"Possibilities in the storage room", "While being likely, there are no proofs confirming the explosion was the result of a heated argument gone wrong. #Dead end of related story branch#"},
   };
}