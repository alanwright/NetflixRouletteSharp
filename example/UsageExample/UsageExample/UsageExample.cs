// Alan Wright
// 7/17/14

using System;
using NetflixRouletteSharp;
using System.Collections.Generic;

namespace NetflixRouletteSharpUsageExample
{
    internal class UsageExample
    {
        private static void Main(string[] args)
        {
            //Title only exmaple
            Console.WriteLine("[Title Only] Response: {0}", NetflixRoulette.TitleRequest("Pulp Fiction"));

            //Title and year example
            Console.WriteLine("[Title and Year] Response: {0}", NetflixRoulette.TitleAndYearRequest("Hannibal", 2001));

            Console.WriteLine("[Director] Response:");
            List<RouletteResponse> list = NetflixRoulette.DirectorRequest("Joss Whedon");
            foreach(RouletteResponse r in list)
                Console.WriteLine(r);

            Console.ReadLine();
        }
    }
}
