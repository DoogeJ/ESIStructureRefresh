using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ESIStructureRefresh
{
    class Program
    {
        public static string AuthenticationToken = "--Your application Authentication token here--";

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing...");

            List<Character> characters = new List<Character>
            {
                new Character
                {
                    CharacterID = 0, //First character ID here
                    RefreshToken = "--" //First character refresh token here
                },
                new Character
                {
                    CharacterID = 1, //Second character ID here
                    RefreshToken = "--" //First character ID refresh token here
                }
            };

            foreach(Character curChar in characters)
            {
                RefreshTokenRequestResult refreshToken = OAuth2.GetAccessToken(AuthenticationToken, curChar.RefreshToken);

                //get character info
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.BaseAddress = "https://esi.evetech.net";
                string result = client.DownloadString($"/latest/characters/{curChar.CharacterID}/?datasource=tranquility");

                CharactersRequestResult charactersRequestResult = JsonConvert.DeserializeObject<CharactersRequestResult>(result);

                result = client.DownloadString($"/latest/corporations/{charactersRequestResult.corporation_id}/structures/?datasource=tranquility&language=en-us&page=1&token={refreshToken.access_token}");

                List<StructuresRequestResult> structuresRequestResultList = JsonConvert.DeserializeObject<List<StructuresRequestResult>>(result);

                foreach(StructuresRequestResult structuresRequestResult in structuresRequestResultList)
                {
                    result = client.DownloadString($"/latest/universe/structures/{structuresRequestResult.structure_id}/?datasource=tranquility&token={refreshToken.access_token}");
                    StructureRequestResult structureRequestResult = JsonConvert.DeserializeObject<StructureRequestResult>(result);
                    Console.WriteLine(structureRequestResult.name);
                    Console.WriteLine(structuresRequestResult.fuel_expires);
                    Console.WriteLine();
                }

            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }

    public class Character
    {
        public int CharacterID;
        public string RefreshToken;
    }
}
