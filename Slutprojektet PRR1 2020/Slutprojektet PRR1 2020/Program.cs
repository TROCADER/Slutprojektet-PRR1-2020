using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Slutprojektet_PRR1_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej och välkommen till William Casino");
            Console.WriteLine("\nHär kommer du att finna stora chanser att vinna stort och 'små' risker att förlora stort");
            Console.WriteLine("\nVälj det spel ni vill spela:\n");

            ChooseGame();

            Console.ReadLine();
        }
        static void ChooseGame()
        {
            string[] games =
            {
                "1: Blackjack",
                "2: Roulette"
            };

            for (int i = 0; i < games.Length; i++)
            {
                Console.WriteLine(games[i]);
            }

            Console.WriteLine("\n\nKlicka på det nummer som är tillhörande till det valda spelet");

            int.TryParse(Console.ReadLine().Trim(), out int playerInput);

            //Fixa smrt kod genom DRY här:
            while (playerInput != 1 && playerInput != 2)
            {
                Console.WriteLine("Du har valt ett spel som inte existerar, vänligen välj igen");
                int.TryParse(Console.ReadLine().Trim(), out playerInput);
            }

            if (playerInput == 1)
            {
                Console.WriteLine("Du har valt Blackjack");
                Console.WriteLine("Du kommer nu bli skickad till det valda spelet");

                Thread.Sleep(1000);
            }
        }
    }   
}
