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
        static Random generator = new Random();

        static int playerPoints = 0;
        static int dealerPoints = 0;

        static int playerCard = 0;
        static int dealerCard = 0;

        static int money = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(@"
                                               _             
                                              (_)            
                                  ___ __ _ ___ _ _ __   ___  
                                 / __/ _` / __| | '_ \ / _ \ 
                                | (_| (_| \__ \ | | | | (_) |
                                 \___\__,_|___/_|_| |_|\___/ 
                                                             
                                ");
            
            Console.WriteLine("Hej och välkommen till William Casino");
            Console.WriteLine("\nHär kommer du att finna stora chanser att vinna stort och 'små' risker att förlora stort");
            Console.WriteLine("\nVälj det spel ni vill spela:\n");

            ChooseGame();

            Console.WriteLine("Klicka på valfi knapp för att avsluta");
            Console.ReadKey();
        }
        static void ChooseGame()
        {
            string[] games =
            {
                "1: Blackjack",
                "2: Roulette (inte gjorts, finns här som anternativ för enkel utbyggning programmet"
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

            bool isHitting = false;
            bool drawingCards = true;
            bool wantsToPlay = true;

            int[] bettableAmmount =
            {
                0,
                1,
                100,
                1000,
                10000,
                100000,
                1000000
            };

            while (wantsToPlay == true)
            {
                Console.Clear();
                while (playerPoints < 21 && drawingCards == true && money > 0)
                {
                    Console.WriteLine("Hur mycket vill du betta?");

                    for (int i = 0; i < bettableAmmount.Length; i++)
                    {
                        Console.WriteLine(bettableAmmount[i]);
                    }

                    Console.WriteLine("Skriv det indexvärde till det belopp du önskar betta");
                    string userInput = Console.ReadLine().Trim().ToLower();

                    while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5" && userInput != "6")
                    {
                        Console.WriteLine("Du har valt ett belopp som inte existerar, vänligen försök igen");
                        userInput = Console.ReadLine().Trim().ToLower();
                    }

                    int.TryParse(userInput, out int userInputInt);

                    money = money - bettableAmmount[userInputInt];

                    Console.WriteLine("\nDu har: " + money + " pengar kvar");
                    Thread.Sleep(1500);
                
                    string[] options =
                    {
                        "1: Hit",
                        "2: Stand",
                        "3: Double up"
                    };

                    Console.Clear();
                    DrawCard();

                    Console.WriteLine("\n\nVad vill du göra?");

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.WriteLine(options[i]);
                    }

                    int.TryParse(Console.ReadLine().Trim(), out int playerInput);

                    while (playerInput != 1 && playerInput != 2 && playerInput != 3)
                    {
                        Console.WriteLine("Du har valt ett ogitigt alternativ, vänligen välj igen");
                        int.TryParse(Console.ReadLine().Trim(), out playerInput);
                    }

                    if (playerInput == 1)
                    {
                        isHitting = true;

                        while (isHitting == true)
                        {
                            if (playerPoints >= 21)
                            {
                                isHitting = false;
                            }
                        
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

                    else if (playerInput == 2)
                    {
                        Console.WriteLine("Du har valt att stand:a, dealern kommer strax att börja dra kort");
                        Thread.Sleep(2000);
                    }

                    else if (playerInput == 3)
                    {
                        Console.WriteLine("Du har valt att double up:a, det belopp du bettade kommer nu att dubblas");

                        StringToInt(userInput);

                        money = money - bettableAmmount[userInputInt];

                        Console.WriteLine("\nDu har : " + money + " kvar");

                        Thread.Sleep(2000);

                        Console.Clear();
                        isHitting = true;

                        while (isHitting == true)
                        {
                            if (playerPoints >= 21)
                            {
                                isHitting = false;
                            }

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

                        Thread.Sleep(2000);
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
                        Console.WriteLine("\n\nDealern bustade");
                        Console.WriteLine("\nDu vann omgången!");
                        money = PlayerWin(money);
                        Console.WriteLine(money);
                    }

                    else
                    {
                        Console.WriteLine("\n\nDealern fick mer mer poäng än dig!");
                        Console.WriteLine("\nDealern vann omgången!");
                    }

                    drawingCards = false;
                }

                playerPoints = 0;
                dealerPoints = 0;
                drawingCards = true;

                Console.WriteLine("Vill du köra igen?");
                Console.WriteLine("\nSkriv Y för att fortsätta, N för att sluta");

                string continuePlay = Console.ReadLine().Trim().ToLower();

                while (continuePlay != "n" && continuePlay != "y")
                {
                    Console.WriteLine("\nDu har valt ett ogiltigt alternativ, vänligen försök igen");
                    continuePlay = Console.ReadLine().Trim().ToLower();
                }

                if (continuePlay == "n")
                {
                    wantsToPlay = false;
                }

                else
                {
                    wantsToPlay = true;
                }
            }
        }

        static void HowToPlayBlackjack()
        {
            string[] info =
            {
                "Du kommer nu att lära dig hur man spelar!",
                "\nSpelet börjar med attt dealern drar 1 kort till dig och ett kort till sig själv, detta upprepas 1 gång.",
                "\nEfter detta har du möjligheten att kunna göra följande: 'Stand', 'Hit eller 'Double down'",
                "\nOm du väljer 'Stand' kommer du inte få fler kort, utan kommer under restan av spelets gång att bara ha de kort du har för tillfället.",
                "Dealern kommer därefter att börja dra kort.",
                "\nVäljer du 'Hit' kommer du att komma in i 'Hit' sektionen där du får dra kort för att öka dina poäng.",
                "När du slutar dra kort kommer dealern att dra sina kort",
                "\nOm du väljer 'Double down' kommer samma sak som hände med 'Hit' att hända, men du dubblar beloppet som du bettar.",
                "\n\nDenna versionen av BlackJack är däremot inte som de andra.",
                "Normalt sett brukar man vinna den mängd pengar man bettar gånger två, men i denna versionen så vinner man mer destå mindre man bettar, förutom om man inte bettar något.",
                "Hur detta fungerar tänker jag låta er lösa ut, lycka till!"
            };

            for (int i = 0; i < info.Length; i++)
            {
                Console.WriteLine(info[i]);
                Thread.Sleep(1000);
            }

            Console.WriteLine("\n\nKlicka på valfri knapp för att återgå till BlackJack menyn");
            Console.ReadKey();
            BlackJack();
        }

        static void BlackJack()
        {
            LoadGame();

            string[] options =
            {
                "1: Börja spela",
                "2: Lär dig spela"
            };

            Console.WriteLine(@"
                                 _     _            _    _            _    
                                | |   | |          | |  (_)          | |   
                                | |__ | | __ _  ___| | ___  __ _  ___| | __
                                | '_ \| |/ _` |/ __| |/ / |/ _` |/ __| |/ /
                                | |_) | | (_| | (__|   <| | (_| | (__|   < 
                                |_.__/|_|\__,_|\___|_|\_\ |\__,_|\___|_|\_\
                                                       _/ |                
                                                      |__/                 
                            ");

            Console.WriteLine("Hej och välkommen till spelet Blackjack!");
            Console.WriteLine("\nVad vill du göra?");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }

            int.TryParse(Console.ReadLine().Trim(), out int playerInput);

            while (playerInput != 1 && playerInput != 2)
            {
                Console.WriteLine("Du har valt ett ogiltigt alternativ, vänligen välj igen");
                int.TryParse(Console.ReadLine().Trim(), out playerInput);
            }

            if (playerInput == 1)
            {
                Console.WriteLine("Du har valt: " + options[0]);
                Console.WriteLine("\nSpelet kommer nu att starta");
                Console.Clear();

                PlayBlackJack();
            }

            else if (playerInput == 2)
            {
                Console.WriteLine("Du har valt: " + options[1]);
                Console.WriteLine("\nDu kommer nu att bli omdirigerad till hur man spelar strax");
                Console.Clear();

                HowToPlayBlackjack();
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
                Console.WriteLine(dots[i]);
                Thread.Sleep(500);
            }

            Console.Clear();
        }

        static void DrawCard()
        {
            for (int i = 0; i < 2; i++)
            {
                playerCard = generator.Next(1, 11);
                dealerCard = generator.Next(1, 11);

                playerPoints = playerPoints + playerCard;
                dealerPoints = dealerPoints + dealerCard;

                Console.WriteLine("\nDealern ger dig ett kort, det är värt " + playerCard);
                Thread.Sleep(1000);
                Console.WriteLine("\nDealern ger sig själv ett kort, det är värt " + dealerCard);
                Thread.Sleep(1000);
            }

            Console.WriteLine("\nDu har nu " + playerPoints + " värt i kort");
            Thread.Sleep(500);
            Console.WriteLine("\nDealern har nu " + dealerPoints + " värt i kort");
            Thread.Sleep(500);
        }

        static int StringToInt(string userInput)
        {
            int.TryParse(userInput, out int userInputInt);

            return userInputInt;
        }

        static int PlayerWin(int money)
        {
            money = money * 2;
            return money;
        }
    }   
}
