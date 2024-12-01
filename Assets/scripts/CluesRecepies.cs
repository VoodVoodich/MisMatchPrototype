
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;


class CluesRecepies
{
    static Dictionary<(string, string), string> clueRecipes = new Dictionary<(string, string), string> { };
    public static Dictionary<string, List<string>> deleteOnCombining = new Dictionary<string, List<string>> { };

    public static string Combine(string a, string b)
    {
        (string, string) potenstial_key;

        if (string.Compare(a, b, StringComparison.Ordinal) < 0)
            potenstial_key = (a, b);
        else
            potenstial_key = (b, a);

        return clueRecipes.GetValueOrDefault(potenstial_key, "");
    }

    public static void AddRecipe((string, string) ab, string c, List<string> d = null)
    {
        string a = ab.Item1;
        string b = ab.Item2;
        (string, string) newKey;
        if (string.Compare(a, b, StringComparison.Ordinal) < 0)
            newKey = (a, b);
        else
            newKey = (b, a);

        clueRecipes[newKey] = c;

        
        if (d is not null)
            deleteOnCombining[c] = d;
    }

    public static void SetUpRecipes()
    {
        clueRecipes = new Dictionary<(string, string), string> { };

        AddRecipe(("Sex1", "Sex2"), "SuperSex");
        
        AddRecipe(("Massive burns", "Explosion at the spot"), "Death from explosion");



        AddRecipe(("Greedy Boss", "A Boss Rumour"), "Cost Effective Boss", new List<string>{"Greedy Boss"});


        
        AddRecipe(("Boss and the victim convo exploaded", "Missed the important part"), "Possibilities in the storage room", new List<string>{"Boss and the victim convo exploaded"});
        AddRecipe(("Victim and the Boss tension", "Filled gaps"), "Victim and the Boss connection", new List<string>{"Victim and the Boss tension"});


        AddRecipe(("Victim and the Boss tension", "Boss and the victim convo exploaded"), "Boss suspect");
        AddRecipe(("Autopsy in progress", "Lonely cop"), "Work mess");
        AddRecipe(("Boss disappeared", "Boss suspect"), "Boss is the killer");
    }



}