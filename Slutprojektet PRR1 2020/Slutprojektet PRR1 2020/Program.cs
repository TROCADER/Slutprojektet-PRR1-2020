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

            //ChooseGame();

            PlayBlackJack();

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

            //Fixa smart kod genom DRY här:
            while (playerInput != 1 /*&& playerInput != 2*/)
            {
                Console.WriteLine("Du har valt ett spel som inte existerar eller är under konstruktion, vänligen välj igen");
                int.TryParse(Console.ReadLine().Trim(), out playerInput);
            }

            if (playerInput == 1)
            {
                Console.Clear();
                Console.WriteLine("Du har valt Blackjack");
                Console.WriteLine("Du kommer nu bli skickad till det valda spelet");

                Thread.Sleep(2000);
                Console.Clear();

                BlackJack();
            }
        }

        static void PlayBlackJack()
        {
            Random generator = new Random();

            int playerPoints = 0;
            int dealerPoints = 0;

            int playerCard = 0;
            int dealerCard = 0;

            bool isHitting = false;
            bool drawingCards = true;

            while (playerPoints < 21 && playerPoints < 21 && drawingCards == true)
            {
                string[] options =
                {
                    "1: Hit",
                    "2: Stand"
                };
                
                playerCard = generator.Next(1, 11);
                dealerCard = generator.Next(1, 11);

                playerPoints = playerPoints + playerCard;
                dealerPoints = dealerPoints + dealerCard;

                Console.WriteLine("Dealern ger dig ett kort, det är värt " + playerCard);
                Console.WriteLine("\nDealern ger sig själv ett kort, det är värt " + dealerCard);

                Console.WriteLine("\nDu har nu " + playerPoints + " värt i kort");
                Console.WriteLine("Dealern har nu " + dealerPoints + " värt i kort");

                Console.WriteLine("\n\nVad vill du göra?");

                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine(options[i]);
                }

                int.TryParse(Console.ReadLine().Trim(), out int playerInput);

                while (playerInput != 1 && playerInput != 2)
                {
                    Console.WriteLine("Du har valt ett ogitigt alternativ, vänligen välj igen");
                    int.TryParse(Console.ReadLine().Trim(), out playerInput);
                }

                if (playerInput == 1)
                {
                    isHitting = true;

                    while (isHitting == true)
                    {
                        playerCard = generator.Next(1, 11);
                        playerPoints = playerPoints + playerCard;

                        Console.WriteLine("Dealern ger dig ett kort, det är värt " + playerCard);
                        Console.WriteLine("\nDu har nu " + playerPoints + " värt i kort");

                        Console.WriteLine("Vill du fortsätta hit:a?");
                        Console.WriteLine("\nSkriv Y för att fortsätta, N för att sluta");

                        string playerChoose = Console.ReadLine().Trim().ToLower();

                        while (playerChoose != "n" && playerChoose != "y")
                        {
                            Console.WriteLine("\nDu har valt ett ogiltigt alternativ, vänligen försök igen");
                            playerChoose = Console.ReadLine().Trim().ToLower();
                        }

                        if (playerChoose == "n")
                        {
                            isHitting = false;
                        }
                    }
                }

                Console.WriteLine("\nDealern kommer nu att börja dra kort\n");

                while (dealerPoints < 21 && dealerPoints < playerPoints)
                {
                    dealerCard = generator.Next(1, 11);
                    dealerPoints = dealerPoints + dealerCard;

                    Console.WriteLine("\nDealern ger sig själv ett kort, det är värt " + dealerCard);
                    Console.WriteLine("\nDealern har nu " + dealerPoints + " värt i kort");

                    Thread.Sleep(1000);
                }

                if (dealerPoints > 21)
                {
                    Console.WriteLine("Dealern bustade");
                }

                drawingCards = false;
            }
        }

        static void BlackJack()
        {
            LoadGame();

            string[] options =
            {
                "1: Börja spela",
                "2: Lär dig spela"
            };

            Console.WriteLine("Hej och välkommen till spelet Blackjack!");
            Console.WriteLine("\nVad vill du göra?");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }

            int.TryParse(Console.ReadLine().Trim(), out int playerInput);

            //Fixa smart kod genom DRY här:
            while (playerInput != 1 /*&& playerInput != 2*/)
            {
                Console.WriteLine("Du har valt ett ogiltigt alternativ, vänligen välj igen");
                int.TryParse(Console.ReadLine().Trim(), out playerInput);
            }

            if (playerInput == 1)
            {
                Console.WriteLine("Du har valt: " + options[0]);
                Console.WriteLine("\nSpelet kommer nu att starta");
                Console.Clear();
            }

            Console.WriteLine();
        }

        static void LoadGame()
        {
            Console.Clear();

            Thread.Sleep(500);
            Console.WriteLine("Loading game, please wait...");

            string[] dots = new string[5];

            for (int i = 0; i < dots.Length; i++)
            {
                dots[i] = ".";
            }

            for (int i = 0; i < dots.Length; i++)
            {
                Console.WriteLine(dots[i]);
                Thread.Sleep(500);
            }

            Console.Clear();
        }
    }   
}
