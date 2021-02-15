using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Languages : MonoBehaviour
{
    private static Dictionary<string,string>[] dictionary = new Dictionary<string, string>[]
    {
        // BEL
        new Dictionary<string, string>()
        {
            {"Start", "Start"},
            {"start", "start"},
            {"Enter Subject ID...", "Voer Subject ID in..."},
            {"Enter Subject ID", "Voer Subject ID in"},
            {"About us", "Over ons"},
            {"About the Tasks", "Over de taken"},
            {"About you", "Over jou"},
            {"Main Menu", "Hoofdmenu"},
            {"Pip's introduction", "Pips introductie"},
            {"Magic Boxes", "Magische dozen"},
            {"Pip's Car", "Pips auto"},
            {"Find the Puppy", "Vind de puppy"},
            {"The Movie Machine", "De filmmachine"},
            {"The Milkman", "De melkman"},
            {"Rating 1", "Rating 1"},
            {"Rating 2", "Rating 2"},
            {"Flying Spaceships", "Vliegende ruimteschepen"},
            {"Busy Bee", "Bezige bij"},
            {"What does Pip want?", "Wat wil Pip?"},
            {"Pip's Bus", "Pips bus"},
            {"What's a modi?", "Wat is een modi?"},
            {"Good days, bad days", "Goede dagen, slechte dagen"},
            {"Catch the Butterfly", "Vang de vlinder"},
            {"Cartoon", "Cartoon"},
            {"Real", "Echte vlinder"},
            {"Demo", "Demo"},
            {"Practice", "Oefenen"},
            {"Continue", "Ga verder"},
            {"Finish", "Einde"},
            {"Rating", "Rating"},
            {"Yes", "Ja"},
            {"No", "Nee"},
            {"Did your child watch the video?", "Heeft uw kind naar" + "\n" + "het filmpje gekeken?"},
            {"Did your child watch" + "\n" + "and understand the demo?", "Heeft uw kind de demo" + "\n" + "bekeken en begrepen?"},
            {"Parent\'s Go", "Beurt van de ouder" },
            {"Child\'s Go", "Beurt van het kind"},
            {"Choose \"Repeat\"if your child hasn't fully understood", "Kies \"Herhaal\" als uw kind het niet helemaal heeft begrepen"},
            {"Repeat", "Herhaal"},
        },
 
        // FRA
        new Dictionary<string, string>()
        {
            {"Start", "Commencer"},
            {"start", "commencer"},
            {"Enter Subject ID...", "Saisir l’identifiant du sujet..."},
            {"Enter Subject ID", "Saisir l’identifiant du sujet"},
            {"About us", "A propos"},
            {"About the Tasks", "A propos des tâches"},
            {"About you", "A propos de vous"},
            {"Main Menu", "Menu principal"},
            {"Pip's introduction", "Présentation de Pip"},
            {"Magic Boxes", "Les boites magiques"},
            {"Pip's Car", "La voiture de Pip"},
            {"Find the Puppy", "Trouve le chien"},
            {"The Movie Machine", "La machine à film"},
            {"The Milkman", "Le vendeur de lait"},
            {"Rating 1", "Note 1"},
            {"Rating 2", "Note 2"},
            {"Flying Spaceships", "Les vaisseaux spatiaux volants"},
            {"Busy Bee", "Attrape l’abeille"},
            {"What does Pip want?", "Que veut Pip ?"},
            {"Pip's Bus", "Le bus de Pip"},
            {"What's a modi?", "Qu’est-ce qu’un dimo ?"},
            {"Good days, bad days", "Bonnes journées, mauvaises journées"},
            {"Catch the Butterfly", "Attrape le papillon"},
            {"Cartoon", "Dessin animé"},
            {"Real", "Réel"},
            {"Demo", "Démo"},
            {"Practice", "Entrainement"},
            {"Continue", "Continuer"},
            {"Finish", "Terminer"},
            {"Rating", "Note"},
            {"Yes", "Oui"},
            {"No", "Non"},
            {"Did your child watch the video?", "Votre enfant a-t-il regardé la vidéo ?"},
            {"Did your child watch" + "\n" + "and understand the demo?", "Votre enfant a-t-il regardé" + "\n" +  "et compris la démonstration ?"},
            {"Parent\'s Go", "Tour du parent" },
            {"Child\'s Go", "Tour de l’enfant"},
            {"Choose \"Repeat\"if your child hasn't fully understood", "Choisissez \"Répéter\" si votre enfant n'a pas bien compris"},
            {"Repeat", "Répéter"},
        },

        // GBR
        new Dictionary<string, string>()
        {
            {"Start", "Start"},
            {"start", "start"},
            {"Enter Subject ID...", "Enter Subject ID..."},
            {"Enter Subject ID", "Enter Subject ID"},
            {"About us", "About us"},
            {"About the Tasks", "About the Tasks"},
            {"About you", "About you"},
            {"Main Menu", "Main Menu"},
            {"Pip's introduction", "Pip's introduction"},
            {"Magic Boxes", "Magic Boxes"},
            {"Pip's Car", "Pip's Car"},
            {"Find the Puppy", "Find the Puppy"},
            {"The Movie Machine", "The Movie Machine"},
            {"The Milkman", "The Milkman"},
            {"Rating 1", "Rating 1"},
            {"Rating 2", "Rating 2"},
            {"Flying Spaceships", "Flying Spaceships"},
            {"Busy Bee", "Busy Bee"},
            {"What does Pip want?", "What does Pip want?"},
            {"Pip's Bus", "Pip's Bus"},
            {"What's a modi?", "What's a modi?"},
            {"Good days, bad days", "Good days, bad days"},
            {"Catch the Butterfly", "Catch the Butterfly"},
            {"Cartoon", "Cartoon"},
            {"Real", "Real"},
            {"Demo", "Demo"},
            {"Practice", "Practice"},
            {"Continue", "Continue"},
            {"Finish", "Finish"},
            {"Rating", "Rating"},
            {"Yes", "Yes"},
            {"No", "No"},
            {"Did your child watch the video?", "Did your child watch the video"},
            {"Did your child watch" + "\n" + "and understand the demo?", "Did your child watch" + "\n" + "and understand the demo?"},
            {"Parent\'s Go", "Parent\'s Go"},
            {"Child\'s Go", "Child\'s Go"},
            {"Choose \"Repeat\"if your child hasn't fully understood", "Choose \"Repeat\"if your child hasn't fully understood"},
            {"Repeat", "Repeat"},
        },

        // NDL
        new Dictionary<string, string>()
        {
            {"Start", "Start"},
            {"start", "start"},
            {"Enter Subject ID...", "Voer Subject ID in..."},
            {"Enter Subject ID", "Voer Subject ID in"},
            {"About us", "Over ons"},
            {"About the Tasks", "Over de taken"},
            {"About you", "Over jou"},
            {"Main Menu", "Hoofdmenu"},
            {"Pip's introduction", "Pips introductie"},
            {"Magic Boxes", "Magische dozen"},
            {"Pip's Car", "Pips auto"},
            {"Find the Puppy", "Vind de puppy"},
            {"The Movie Machine", "De filmmachine"},
            {"The Milkman", "De melkman"},
            {"Rating 1", "Rating 1"},
            {"Rating 2", "Rating 2"},
            {"Flying Spaceships", "Vliegende ruimteschepen"},
            {"Busy Bee", "Bezige bij"},
            {"What does Pip want?", "Wat wil Pip?"},
            {"Pip's Bus", "Pips bus"},
            {"What's a modi?", "Wat is een modi?"},
            {"Good days, bad days", "Goede dagen, slechte dagen"},
            {"Catch the Butterfly", "Vang de vlinder"},
            {"Cartoon", "Cartoon"},
            {"Real", "Echte vlinder"},
            {"Demo", "Demo"},
            {"Practice", "Oefenen"},
            {"Continue", "Ga verder"},
            {"Finish", "Einde"},
            {"Rating", "Rating"},
            {"Yes", "Ja"},
            {"No", "Nee"},
            {"Did your child watch the video?", "Heeft uw kind naar" + "\n" + "het filmpje gekeken?"},
            {"Did your child watch" + "\n" + "and understand the demo?", "Heeft uw kind de demo" + "\n" + "bekeken en begrepen?"},
            {"Parent\'s Go", "Beurt van de ouder" },
            {"Child\'s Go", "Beurt van het kind"},
            {"Choose \"Repeat\"if your child hasn't fully understood", "Kies \"Herhaal\" als uw kind het niet helemaal heeft begrepen"},
            {"Repeat", "Herhaal"},
        },

        // SWE
        new Dictionary<string, string>()
        {
            {"Start", "Start"},
            {"start", "start"},
            {"Enter Subject ID...", "Skriv in deltagarkod"},
            {"Enter Subject ID", "Skriv in deltagarkod"},
            {"About us", "Om oss"},
            {"About the Tasks", "Om uppgifterna"},
            {"About you", "Om dig"},
            {"Main Menu", "Huvudmeny"},
            {"Pip's introduction", "Pip's introduction"},
            {"Magic Boxes", "Magiska lådor"},
            {"Pip's Car", "Pips bil"},
            {"Find the Puppy", "Hitta valpen"},
            {"The Movie Machine", "FilmMaskinen"},
            {"The Milkman", "Mjölkbudet"},
            {"Rating 1", "Utvärdering 1"},
            {"Rating 2", "Utvärdering 2"},
            {"Flying Spaceships", "Flygande rymdskepp"},
            {"Busy Bee", "Flitiga biet"},
            {"What does Pip want?", "Vad vill Pip ha?"},
            {"Pip's Bus", "Pips buss"},
            {"What's a modi?", "Vad är en modi?"},
            {"Good days, bad days", "Bra dagar,    dåliga dagar"},
            {"Catch the Butterfly", "Fånga fjärilen"},
            {"Cartoon", "Tecknat"},
            {"Real", "Verkligt"},
            {"Demo", "Demo"},
            {"Practice", "Övning"},
            {"Continue", "Fortsätt"},
            {"Finish", "Avsluta"},
            {"Rating", "Utvärdering"},
            {"Yes", "Ja"},
            {"No", "Nej"},
            {"Did your child watch the video?", "Såg ditt barn på videon?"},
            {"Did your child\nwatch and understand the demo?", "Såg och förstod ditt barn demon?"},
            {"Parent\'s Go", "Förälderns tur"},
            {"Child\'s Go", "Barnets tur"},
            {"Choose \"Repeat\"if your child hasn't fully understood", "Välj \"Upprepa\" om ditt barn inte helt har förstått"},
            {"Repeat", "Upprepa"},
        },

        // ZAF
        new Dictionary<string, string>()
        {
            {"Start", "Begin"},
            {"start", "begin"},
            {"Enter Subject ID...", "Druk in deelnemer ID"},
            {"Enter Subject ID", "Druk in deelnemer ID"},
            {"About us", "Oor ons"},
            {"About the Tasks", "Oor die take"},
            {"About you", "Oor jou"},
            {"Main Menu", "Hoof inhoud"},
            {"Pip's introduction", "Pip se inleading"},
            {"Magic Boxes", "Towerboksies"},
            {"Pip's Car", "Pip se kar"},
            {"Find the Puppy", "Vind die hondjie"},
            {"The Movie Machine", "Die fliek masjien"},
            {"The Milkman", "Die melk man"},
            {"Rating 1", "Gradering 1"},
            {"Rating 2", "Gradering 2"},
            {"Flying Spaceships", "Vlieënde ruimtetuie"},
            {"Busy Bee", "Besige bytjie"},
            {"What does Pip want?", "Wat wil Pip hê?"},
            {"Pip's Bus", "Pip se bus"},
            {"What's a modi?", "Wat is ŉ modi?"},
            {"Good days, bad days", "Goeie dae,      Slegte dae"},
            {"Catch the Butterfly", "Vang die skoenlapper"},
            {"Cartoon", "Spotprent"},
            {"Real", "Werklike"},
            {"Demo", "Demo"},
            {"Practice", "Oefen"},
            {"Continue", "Gaan voort"},
            {"Finish", "Eindig"},
            {"Rating", "Gradering"}
            // {"Yes", }
            // {"No", }
            // {"Did your child watch the video?", }
            // {"Did your child watch \and understand the demo?"}
            // {"Parent\'s Go", }
            // {"Child\'s Go", }
            // {"Choose \"Repeat\"if your child hasn't fully understood", }
            // {"Repeat", }
        }
    };

    public static string translate(string input)
    {
        string key = PrefsKeys.Keys.Language.ToString();
        int language = PlayerPrefs.GetInt(key, (int)PrefsKeys.Language.GBR);

        string output;
        if (!dictionary[language].TryGetValue(input, out output))
        {
            return input;
        }
        return output;
    }
}
