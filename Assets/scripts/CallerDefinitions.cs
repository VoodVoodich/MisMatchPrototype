
using System.Collections;
using System.Collections.Generic;

class CallersDefinitions
{
    public static Dictionary<string, Caller> callerDefs = new Dictionary<string, Caller>{
        {"Empty", new Caller(
            name:"Empty",
            greetingLine: "Empty",
            cluelessLine: "Empty.",
            monologues: new Dictionary<string, Monologue>{
                {"Empty", new Monologue( lines: new List<string>{
                                                    "Empty",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},
            }
        )},

        {"Supervisor", new Caller(
            name:"Supervisor",
            greetingLine: "Speak up, I don't have all day",
            cluelessLine: "I don't know what are you talking about.",
            monologues: new Dictionary<string, Monologue>{
                {"Initial Call", new Monologue( lines: new List<string>{
                                                    "Hello detective",
                                                    "I have a case for you",
                                                    "I've sent the basic stuff by email",
                                                    "Read everything by yourself",
                                                    "I can't brief you in as I'm dealing with pain in the ass",
                                                    "Figuratively of course",
                                                    "Anyway all I know about the case is that it is a murder",
                                                    "More details on that get from officer Larry who is on the spot right now",
                                                    "And from the Autopsy department who messes with the deceased's body",
                                                    "I gave you their contacts as well",
                                                    "Anyway that's all, I'm busy as hell, call me only if something serious happens",
                                                    "And you can't rug it under the carpet",
                                                    "Oh and also notify me when you solve the case",
                                                    "Okay good luck bye"
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"The case"},
                                                contactsGotOnMonologueEnd: new List<string>{"Larry", "Autopsy Dep.", "Supervisor"},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                                                , hungUpOnMonoligueEnd:true
                )},

                {"The case", new Monologue( lines: new List<string>{
                                                    "I told you all you require for work already",
                                                    "Don't waste my time",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Boss suspect", new Monologue( lines: new List<string>{
                                                    "Cool, a suspect, that's a good sign",
                                                    "Not as good as solving the case through",
                                                    "I can't make an accusation with just that",
                                                    "Like where is he now at least",
                                                    "Find out and this will be enough",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Boss is the killer", new Monologue( lines: new List<string>{
                                                    "So the main suspect disappeared after the alleged crime",
                                                    "Now that's a criminal if I ever saw one",
                                                    "Great hob, detective",
                                                    "I'll take the case from now on",
                                                    "Your paycheck will be sent in a minute",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Game end"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},
            }
        )},


        {"Autopsy Dep.", new Caller(
            name:"The Autopsy Department",
            greetingLine: "Autopsy Department, how may I be of help.",
            cluelessLine: "I'm afraid I'm of no use here.",
            monologues: new Dictionary<string, Monologue>{
                {"The case", new Monologue( lines: new List<string>{
                                                    "Oh, hello detective",
                                                    "About that...",
                                                    "I'll be honest, I got the guy just now",
                                                    "As well as found out we had such a case at all",
                                                    "Our old timer still complains this is the first time such mess has happened",
                                                    "Anyway the results will not come soon since we don't even have preparations in order",
                                                    "Place is a mess after the last case",
                                                    "I can only give observation data about the corpse",
                                                    "First that meets the eye are burned clothes and burns all over the body",
                                                    "No other wounds are to be seen, so the running theory they are the cause of death",
                                                    "The victims muscle tissues are lower than average, so he is less likely to be able to defend himself",
                                                    "And this is all I can surely say",
                                                    "I'll report as soon as we actually run the procedure",
                                                    "But don't count on it being fast",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Massive burns", "Weak body", "Autopsy in progress"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},
                
                {"Lonely cop", new Monologue( lines: new List<string>{
                                                    "Oh damn this sucks",
                                                    "What? I'm not about Larry!",
                                                    "I'm about the case at all",
                                                    "It's not only us who have organisation abnormalities",
                                                    "But such things happen sometimes and whining doesn't do the work",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},
                
                {"Drastic updates", new Monologue( lines: new List<string>{
                                                    "Hello detective, this is Alice from the Autopsy department",
                                                    "We have major updates on the corpse",
                                                    "What is bizzare we still haven't run the autopsy",
                                                    "We are to take a note to check on a pulse of corpses before the autopsy",
                                                    "The victim was pretty much alive",
                                                    "I was forced to send my intern home after she witnessed a courpse trying to get up",
                                                    "I took the liberty to contact your subordinate to find out how this mess happened",
                                                    "Usually medics at the spot check this kind of stuff",
                                                    "Okay sorry, we give him a very late first aid here",
                                                    "Good luck officer",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Living dead"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                                                , hungUpOnMonoligueEnd: true
                )},
                
                {"Explosion at the spot", new Monologue( lines: new List<string>{
                                                    "So the victim got blown up",
                                                    "This checks out",
                                                    "This also explains burned clothes",
                                                    "Body has no fractures, visible at least",
                                                    "We'll look at it as we will start the autopsy",
                                                    "We're still not ready",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},
            }
        )},


        {"Larry", new Caller(
            name:"Larry",
            greetingLine: "Officer Larry reporting for duty!",
            cluelessLine: "Sorry, boss, I dunno.",
            monologues: new Dictionary<string, Monologue>{
                {"Victims found", new Monologue( lines: new List<string>{
                                                    "Hi boss, officer Larry reporting, good news!",
                                                    "Good lady from the reception gave me the cable so I'm done.",
                                                    "With download, I didn't peek at the videos yet.",
                                                    "But the good lady hinted me to who could possibly witness the case!",
                                                    "And I confirmed two of them are the witnesses",
                                                    "First is another factory worker who stayed late",
                                                    "The second is the security guy who was on night shift that day",
                                                    "I've send you their contacts",
                                                    "I couldn't do the interrogation since they are not at work today",
                                                    "And because I have no idea what to ask them",
                                                    "That's all I have to report!",
                                                    "Good luck with the case, boss.",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Witness 1", "Witness 2"},
                                                contactsGotOnMonologueEnd: new List<string>{"Witness 1", "Witness 2"},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                                                , hungUpOnMonoligueEnd:true
                )},

                {"The case", new Monologue( lines: new List<string>{
                                                    "Hi boss, I'm on the murder scene as we speak!",
                                                    "I am ready to report right now!",
                                                    "The murder scene is the storage room in the local matches factory",
                                                    "At least what has left of it",
                                                    "Police was called for the massive explosion heard by the locals",
                                                    "As we arrived and started investigating we have found the victim",
                                                    "The victim is an ordinary employee of the factory",
                                                    "Why we investigate this as murder?",
                                                    "Medics who were here before us told that from their history they are able to save people with such burns",
                                                    "And the factory has a lasting conflict between workers and management",
                                                    "That's what your Supervisor told me at least, he was here first",
                                                    "I'm investigating all that now",
                                                    "More specific I'm fetching the recordings from the cameras for further analysis",
                                                    "But I forgot the charger so I transfer the files via Bluetooth",
                                                    "And it says it will finish in 10 hour",
                                                    "I'll report as soon as I analyse the recordings",
                                                    "And this report is over!",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Matches factory", "Explosion at the spot", "Just a worker"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{
                                                    new RingEvent(callsToWait: 0, secondsAfterCallsToWait: 5f, caller: "Larry", monologueIdentifier: "Victims found")
                                                }
                )},

                {"Autopsy in progress", new Monologue( lines: new List<string>{
                                                    "Well, that's a bummer",
                                                    "From my end I also can complain about the work flow",
                                                    "Usually police is not the only one at the spot when such things happen",
                                                    "I usually chat with medics or fire guys about stuff",
                                                    "But this time it is like we're late at the party and now all alone",
                                                    "But I'm not here to complain, I'm here for my cop duty!",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Lonely cop"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{ }
                )},

                {"Boss suspect", new Monologue( lines: new List<string>{
                                                    "Boss of the factory is the main suspect?",
                                                    "Sorry boss, I am of little help here",
                                                    "I mean in interrogating him",
                                                    "Nobody saw him since the explosion",
                                                    "And I did a few calls he is not at home",
                                                    "I won't make any conclusions, it's your work boss",
                                                    "And that's the end of my report on the boss of this factory",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Boss disappeared"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{
                                                    new RingEvent(callsToWait: 0, secondsAfterCallsToWait: 4.5f, caller: "Autopsy Dep.", monologueIdentifier: "Drastic updates")
                                                 }
                )},
            }
        )},

        
        {"Witness 1", new Caller(
            name:"Braun Dorfl",
            greetingLine: "Braun Dorfl speaking.",
            cluelessLine: "What? Whatcha talking about?",
            monologues: new Dictionary<string, Monologue>{
                {"The case", new Monologue( lines: new List<string>{
                                                    "Oh, you're the detective",
                                                    "What, it was a murder?",
                                                    "I could get this was the imminent effect of safety regulations negledgement",
                                                    "But someone decided to be faster to kick the bucket at this damn place",
                                                    "I'm Braun Dorfl and I have 4 years 4 months and 4 days left until retirement",
                                                    "Now back to the topic, it is true I stayed late that day",
                                                    "An old conveyor belt malfunctions and I am the only one who remembers how to fix it",
                                                    "Sadly I know nothing of who entered the storage room, but I have a good hunch",
                                                    "We have a big argument related to safety precausions at work",
                                                    "Our boss says that we have the minimum required but he is the only one with this opinion",
                                                    "This bastard finds any excuse to cut the costs",
                                                    "I bet he is the one who killed the poor guy, he had a good motive",
                                                    "The victim and the killer often talked about the pressing topic",
                                                    "I guess he chose to get rid of the irritating bug",
                                                    "I surely know they were both at the factory that night",
                                                    "He could've made it look like an accident and pay the right people to dodge consequences",
                                                    "But it is good the police actually took the case",
                                                    "I'll be of any help I could provide to you, detective",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Greedy Boss", "Victim and the Boss tension"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Matches factory", new Monologue( lines: new List<string>{
                                                    "Ye, I work there",
                                                    "For 25 years",
                                                    "And now I'm waiting for my retirement",
                                                    "4 years 4 months and 4 days and I'll be getting money for free",
                                                    "Ain't this something working all your life is worth for",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Witness 2", new Monologue( lines: new List<string>{
                                                    "Well, it makes sence one of those is also a witness",
                                                    "I wonder who had a shift that night",
                                                    "Wait I know who it was, it was Greg",
                                                    "The only thing he could have witnessed are sweet dreams",
                                                    "He is the massive slacker, everyone knows that",
                                                    "He sleeps  every night shift, hell, he sleeps at daytime too",
                                                    "But nobody cares since no one tried to steal matches from us",
                                                    "He basically gets money for free and he is far away from retirement than I am",
                                                    "This hurts my feelings and I did report this to the boss",
                                                    "But of course he did nothing since he gets a cut from guard's salary",
                                                    "Our boss is a true scumbag",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Slacking worker"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Old grudges", new Monologue( lines: new List<string>{
                                                    "Now that's just rude",
                                                    "My claim was legitemate because I did work for 2",
                                                    "I was already discussing it with the previous boss",
                                                    "But then this dougebag came and he just told me 'if you don't like it then work for 1'",
                                                    "It was a spit on my face, and my so called 'collegues' sided with this ignorant snob",
                                                    "So how can I be nice to them? It is just an objective fact they all are bad people",
                                                    "And I don't imagine stuff, all I said is true!",
                                                    "But... my hearing is only worse and worse with age",
                                                    "I'm not really sure what the victim and the boss were talking about",
                                                    "But I tell you the boss did it, he is this type of a person!",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Filled gaps"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},


            }
        )},

        
        {"Witness 2", new Caller(
            name:"Greg Cauldron",
            greetingLine: "Hello",
            cluelessLine: "Sorry, I got no clue.",
            monologues: new Dictionary<string, Monologue>{
                {"The case", new Monologue( lines: new List<string>{
                                                    "Oh this",
                                                    "I was on guard duty that day",
                                                    "I sit all the time at office",
                                                    "But I look at the cameras",
                                                    "And I saw the victim and the boss entering the storage room",
                                                    "That camera is rather high above the ground, I didn't see them arguing then",
                                                    "But in the storage the conversation heated up as often happens between them",
                                                    "Sadly some boxes obstructed vision so I don't fully know what happened",
                                                    "But shortly after an explosion occured",
                                                    "That's it",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Boss and the victim convo exploaded"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Witness 1", new Monologue( lines: new List<string>{
                                                    "Oh I know who are you talking about",
                                                    "I pity you, having a senile witness who has a grudge agains every coworker it seems",
                                                    "From the convos I had with other workers he is truly a pro",
                                                    "But his character...",
                                                    "With age wine becomes better, Braun with age is closer to his retirement",
                                                    "And good thing he himself counts the time, since everyone is waiting for it",
                                                    "The boss especially since he hates him the most",
                                                    "It all started when the current boss came into position",
                                                    "Rumour says that the reason is that the new boss didn't agree to employ him at another position",
                                                    "His plan was to get a bigger pension",
                                                    "And nobody supported his goal, so now he is mad at everyone",
                                                    "That's just a theory through",
                                                    "But he is so mad sometimes he imagines stuff to hate them for",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Old grudges"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Slacking worker", new Monologue( lines: new List<string>{
                                                    "Now that's just slander",
                                                    "I actually work, and work hard",
                                                    "And sometimes work is tough so I need a little rest",
                                                    "I do have an alarm that plays every 15 minutes",
                                                    "It is not my fault this place is so uneventful at night",
                                                    "But... I did sleep though the storage scene",
                                                    "I saw them both go into the storage room",
                                                    "But after I fell asleep and woke up when they were actively discussing something",
                                                    "Then I fell asleep again",
                                                    "And then the explosion occured",
                                                    "I don't know what it changes, but technically I didn't witness that the explosion is the result of arguing",
                                                    "So maybe someone else cause an explosion, I don't know",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"Missed the important part"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},

                {"Greedy Boss", new Monologue( lines: new List<string>{
                                                    "Yeah, it is kinda true",
                                                    "But I know his assistant and we drink beer every now and then",
                                                    "He isn't a money hungry maniac",
                                                    "He is worse, according to my friend",
                                                    "Makes sense since it makes his work harder",
                                                    "So the punch line is that the boss has an unhealthy obsession with effective expenses",
                                                    "He keeps track no more money than nessesary are spent",
                                                    "I dunno about business, but this strategy doesn't create a good image for him",
                                                    "There were conflicts on safety measures that were borderline required or not",
                                                    "But since the boss didn't comply to these complains stating they are excess he got the reputation",
                                                    "And the reputation created appropriate rumours",
                                                    },
                                                cluesGotOnMonologueEnd: new List<string>{"A Boss Rumour"},
                                                contactsGotOnMonologueEnd: new List<string>{},
                                                ringEventsOnMonologueEnd: new List<RingEvent>{}
                )},
            }
        )},
   };
}

