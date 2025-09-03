
///////////////////////////////// Filtrage Basique ///////////////////////////////////
using System.Text.RegularExpressions;

Console.WriteLine("--------------Words-------------");
Console.WriteLine("Filtrage Basique");
string[] words = { "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune" };

words.Where(w => !w.Contains('x') && w.Count() >= 4 && w.Count() == Math.Round(words.Average(w => w.Length), 0)).ToList().ForEach(Console.WriteLine);

/*
- dans l’ordre inverse de celui naturellement calculé : .Reverse
- triés a-z : .Order
- triés z-a : OrderDescending
  */


///////////////////////// Données parasites 1 /////////////////////////////////////
Console.WriteLine("\nDonnées parasites 1");
string[] words2 = { "whatThe!!!", "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune", "My kingdom for a horse !", "Ooops I did it again" };
words2.Skip(1).SkipLast(2).ToList().ForEach(Console.WriteLine);

///////////////////////// Données parasites 2 /////////////////////////////////////
Console.WriteLine("\nDonnées parasites 2");
string[] words3 = { "+++++", "<<<<<", ">>>>>", "bonjour", "hello", "@@@@", "vert", "rouge", "bleu", "jaune", "#####", "%%%%%%%" };
words3.Where(w => Regex.IsMatch(w, "^[a-zA-Z]+$")).ToList().ForEach(Console.WriteLine);

///////////////////////// Elitisme /////////////////////////////////////
Console.WriteLine("\nElitisme");
string[] words4 = { "i am the winner", "hello", "monde", "vert", "rouge", "bleu", "i am the looser" };
words4.Take(1).ToList().ForEach(w => Console.WriteLine("The winner is : "+w));
words4.TakeLast(1).ToList().ForEach(w => Console.WriteLine("The looser is : " + w));

///////////////////////// Epsilon /////////////////////////////////////
Console.WriteLine("\nEpsilon");
Dictionary<char, double> frequencies = new Dictionary<char, double>()
{
    { 'a', 7.636 },
    { 'b', 0.901 },
    { 'c', 3.260 },
    { 'd', 3.669 },
    { 'e', 14.715 },
    { 'f', 1.066 },
    { 'g', 0.866 },
    { 'h', 0.737 },
    { 'i', 7.529 },
    { 'j', 0.613 },
    { 'k', 0.049 },
    { 'l', 5.456 },
    { 'm', 2.968 },
    { 'n', 7.095 },
    { 'o', 5.796 },
    { 'p', 2.521 },
    { 'q', 1.362 },
    { 'r', 6.553 },
    { 's', 7.948 },
    { 't', 7.244 },
    { 'u', 6.311 },
    { 'v', 1.628 },
    { 'w', 0.114 },
    { 'x', 0.387 },
    { 'y', 0.308 },
    { 'z', 0.136 }
};
double Epsilon (string word, Dictionary<char, double> frequencies)
{
    return word
        .GroupBy(c => c)
        .ToDictionary(group => group.Key, group => group.Count())
        .Sum(c => frequencies[c.Key] / 100.0 / c.Value);
}


///////////////////////// Dictionnaire /////////////////////////////////////
Console.WriteLine("\nDictionnaire");
List<string> frenchWords = new List<string>() {
    "Merci",
    "Hotdog",
    "Oui",
    "Non",
    "Désolé",
    "Réunion",
    "Manger",
    "Boire",
    "Téléphone",
    "Ordinateur",
    "Internet",
    "Email",
    "Sandwich",
    "Hello",
    "Taxi",
    "Hotel",
    "Gare",
    "Train",
    "Bus",
    "Métro",
    "Tramway",
    "Vélo",
    "Voiture",
    "Piéton",
    "Feu rouge",
    "Cédez",
    "Ralentir",
    "gauche",
    "droite",
    "Continuer",
    "Sandwich",
    "Retourner",
    "Arrêter",
    "Stationnement",
    "Parking",
    "Interdit",
    "Péage",
    "Trafic",
    "Route",
    "Rond-point",
    "Football",
    "Carrefour",
    "Feu",
    "Panneau",
    "Vitesse",
    "Tramway",
    "Aéroport",
    "Héliport",
    "Port",
    "Ferry",
    "Bateau",
    "Canot",
    "Kayak",
    "Paddle",
    "Surf",
    "Plage",
    "Mer",
    "Océan",
    "Rivière",
    "Lac",
    "Étang",
    "Marais",
    "Forêt",
    "Hello",
    "Montagne",
    "Vallée",
    "Plaine",
    "Désert",
    "Jungle",
    "Savane",
    "Volleyball",
    "Tundra",
    "Glacier",
    "Neige",
    "Pluie",
    "Soleil",
    "Nuage",
    "Vent",
    "Tempête",
    "Ouragan",
    "Tornade",
    "Séisme",
    "Tsunami",
    "Volcan",
    "Éruption",
    "Ciel"
};


Console.ReadLine();
 