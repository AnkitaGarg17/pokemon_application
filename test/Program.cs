using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace test
{
    class Program
    {

        static async Task Main()
        {
            Console.WriteLine("Enter pokemon name?");
            var name = Console.ReadLine();

            try
            {
                 HttpClient client = new HttpClient();
                string apiUrl = "https://pokeapi.co/api/v2/pokemon/" + name.ToLower();
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    JsonDocument jsonDocument = JsonDocument.Parse(responseContent);

                    JsonElement desiredValue = jsonDocument.RootElement.GetProperty("types");

                    if (desiredValue.ValueKind == JsonValueKind.Array)
                    {

                        HashSet<string> strengths = new HashSet<string>();
                        HashSet<string> weaknesses = new HashSet<string>();

                        foreach (JsonElement arrayElement in desiredValue.EnumerateArray())
                        {
                            string? pokemonTypeName = null;

                            pokemonTypeName = arrayElement.GetProperty("type").GetProperty("name").ToString();

                            // another api call
                            string typeApiUrl = "https://pokeapi.co/api/v2/type/" + pokemonTypeName;
                            HttpResponseMessage responsetype = await client.GetAsync(typeApiUrl);
                            if (responsetype.IsSuccessStatusCode)
                            {
                                string responseContenttype = await responsetype.Content.ReadAsStringAsync();
                                JsonDocument jsonDocumentType = JsonDocument.Parse(responseContenttype);

                                JsonElement damage_relations = jsonDocumentType.RootElement.GetProperty("damage_relations");

                                addTypesInHashSet(damage_relations, "double_damage_to", strengths);
                                addTypesInHashSet(damage_relations, "no_damage_from", strengths);
                                addTypesInHashSet(damage_relations, "half_damage_from", strengths);
                                addTypesInHashSet(damage_relations, "double_damage_from", weaknesses);
                                addTypesInHashSet(damage_relations, "half_damage_to", weaknesses);
                                addTypesInHashSet(damage_relations, "no_damage_to", weaknesses);
                            }
                        }
                        Console.WriteLine("Pokemon " + name + "'s Strengths:");
                        DisplayTypes(strengths);

                        Console.WriteLine("Pokemon " + name + "'s Weaknesses:");
                        DisplayTypes(weaknesses);
                    }

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Pokémon "+name+" not exist.");
                }
                else
                {
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                }
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void addTypesInHashSet(JsonElement damageRelations, string typeName, HashSet<string> types)
        {
            JsonElement typeArray = damageRelations.GetProperty(typeName);
            foreach (JsonElement typeElement in typeArray.EnumerateArray())
            {
                string? typeNames = typeElement.GetProperty("name").GetString();
                types.Add(typeNames);
            }
        }

        static void DisplayTypes(HashSet<string> types)
        {
            foreach (string typeName in types)
            {
                Console.WriteLine(typeName);
            }
            Console.WriteLine();
        }
    }
}




