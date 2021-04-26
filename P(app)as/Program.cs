using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace pAPPas
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.reddit.com")
            };

            Console.OutputEncoding = Encoding.UTF8;
            int limit = 1;
            bool shouldRun;
            do
            {
                bool parsed = false;
                while (!parsed)
                {
                    Console.WriteLine("Kol'ko ces komentara?");
                    parsed = int.TryParse(EasterEgg(), out limit);
                    if (!parsed)
                        Console.WriteLine("Lepo unesi bre, 1,2,3... Ajde opet.");
                }

                Console.WriteLine("'Oces da ti grupisem po threadu? y/n");

                var grupisanje = EasterEgg().Equals("y", StringComparison.OrdinalIgnoreCase);

                var json = await httpClient.GetStringAsync($"r/serbia/comments.json?limit={limit}");
                var comments = JsonSerializer.Deserialize<Example>(json);

                System.Collections.Generic.IList<Child> children = comments.data.children;

                if (grupisanje)
                    children = children.OrderBy(c => c.data.link_title).ToList();

                foreach (var item in children)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine(item.data.author);
                    Console.WriteLine(item.data.link_title);
                    Console.WriteLine(item.data.body + Environment.NewLine);
                }

                Console.WriteLine("'Oces jos? y/n");
                shouldRun = EasterEgg().Equals("y", StringComparison.OrdinalIgnoreCase);
            } while (shouldRun);

            Console.WriteLine("Hvala sto ste koristili! Komentare, pohvale, pretnje, slike stopala i sl. saljite na u/pekulini.");
            Console.ReadLine();
        }

        private static string EasterEgg()
        {
            string s = Console.ReadLine();
            if (s.Equals("papas babun", StringComparison.OrdinalIgnoreCase))
                Console.WriteLine("It is known.");
            return s;
        }
    }
}
