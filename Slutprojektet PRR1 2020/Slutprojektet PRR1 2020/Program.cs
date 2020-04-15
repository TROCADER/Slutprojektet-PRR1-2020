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
        //Universiella variabler och en generator som används i flera metoder (anledningen till varför dessa ligger utanför metoden)
        static Random generator = new Random();

        static int playerPoints = 0;
        static int dealerPoints = 0;

        static int playerCard = 0;
        static int dealerCard = 0;

        static int money = 1000;

        static void Main(string[] args)
        {
            //Välkomnar användaren och frågar vilket spel som hen vill spela
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

            //Hoppar till ChooseGame metoden där spelet väljs
            ChooseGame();

            Console.WriteLine("Klicka på valfi knapp för att avsluta");
            Console.ReadKey();
        }
        static void ChooseGame()
        {
            //Här använder jag en array eftersom att det finns en fördefinerat mängd alternativ där användaren inte får utöka/kommer inte utökas senare i programmet
            string[] games =
            {
                "1: Blackjack",
                "2: Roulette, går ej att välja (inte gjorts, finns här som anternativ för enkel utbyggning programmet samt för att visa att for for-loopen funkar)"
            };

            //Skriver ut alla alternativ, valde en for loop då man kan utöka arrayen utan att behöva lägga till mycket kod (det är bara att lägga till i arrayen och koden funkar)
            for (int i = 0; i < games.Length; i++)
            {
                Console.WriteLine(games[i]);
            }

            Console.WriteLine("\n\nKlicka på det nummer som är tillhörande till det valda spelet");

            int.TryParse(Console.ReadLine().Trim(), out int playerInput);

            //Användaren måste välja ett giltigt spel (ett som existerar)
            while (playerInput != 1)
            {
                Console.WriteLine("Du har valt ett spel som inte existerar eller är under konstruktion, vänligen välj igen");
                int.TryParse(Console.ReadLine().Trim(), out playerInput);
            }

            //Vid valt spel blir spelaren omdirigerad till det valda spelet
            if (playerInput == 1)
            {
                Console.Clear();
                Console.WriteLine("Du har valt Blackjack");
                Console.WriteLine("Du kommer nu bli skickad till det valda spelet");

                Thread.Sleep(2000);
                Console.Clear();

                Blackjack();
            }
        }

        static void PlayBlackjack()
        {
            //Startargument som används för att sätta igång while-loopar som existerar i metoden
            bool isHitting = false;
            bool drawingCards = true;
            bool wantsToPlay = true;

            //Här använder jag en array eftersom att det finns en fördefinerat mängd alternativ där användaren inte får utöka/kommer inte utökas senare i programmet
            //En array med de bettbara beloppen
            int[] bettableAmmount =
            {
                0, //Ifall man bara vill spela, utan risken förlora cash, man kan inte heller vinna något då
                1,
                100,
                1000,
                10000,
                100000,
                1000000
            };

            //Sålänge som användaren vill spela spelet kommer spelsektionen att vara aktiv, används för att stänga ner spelet om användaren vill avsluta spelet (så att man kan köra spelet flera gånger)
            while (wantsToPlay == true)
            {
                //Gör så att man kan betta den valda mängden
                Console.Clear();
                while (playerPoints < 21 && drawingCards == true && money > 0)
                {
                    Console.WriteLine("Hur mycket vill du betta?");

                    for (int i = 0; i < bettableAmmount.Length; i++)
                    {
                        Console.WriteLine(bettableAmmount[i]);
                    }

                    //Här väljer spelaren mängden att betta
                    Console.WriteLine("Skriv det indexvärde till det belopp du önskar betta");
                    string userInput = Console.ReadLine().Trim().ToLower();

                    //Användaren måste välja ett giltigt belopp
                    while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5" && userInput != "6")
                    {
                        Console.WriteLine("Du har valt ett belopp som inte existerar, vänligen försök igen");
                        userInput = Console.ReadLine().Trim().ToLower();
                    }

                    //Substraherar det bettade beloppet från spelarens pengar
                    int.TryParse(userInput, out int userInputInt);

                    money = money - bettableAmmount[userInputInt];

                    Console.WriteLine("\nDu har: " + money + " pengar kvar");
                    Thread.Sleep(1500);

                    //Här använder jag en array eftersom att det finns en fördefinerad mängd alternativ där användaren inte får utöka/kommer inte utökas senare i programmet
                    //Alternativ av utföringbara handlingar
                    string[] options =
                    {
                        "1: Hit",
                        "2: Stand",
                        "3: Double up"
                    };

                    //Tillkallar DrawCard metoden som drar kort, för att hålla koden "renare"
                    Console.Clear();
                    DrawCard();

                    //Frågar vilken handling spelaren vill utföra, skriver ut alternativen som står i den ovanstående arrayen
                    Console.WriteLine("\n\nVad vill du göra?");

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.WriteLine(options[i]);
                    }

                    int.TryParse(Console.ReadLine().Trim(), out int playerInput);

                    //Spelaren måste välja ett giltigt alternativ
                    while (playerInput != 1 && playerInput != 2 && playerInput != 3)
                    {
                        Console.WriteLine("Du har valt ett ogitigt alternativ, vänligen välj igen");
                        int.TryParse(Console.ReadLine().Trim(), out playerInput);
                    }

                    //Om användaren väljer alternativ 1 ("Hit") så kommer den handligen att utföras
                    if (playerInput == 1)
                    {
                        isHitting = true;

                        while (isHitting == true)
                        {
                            if (playerPoints >= 21)
                            {
                                isHitting = false;
                            }

                            //Drar ett kort till spelaren, görs inte av DrawCard metoden då detta görs bara en gång, istället för flera gånger som DrawCard metoden gör
                            playerCard = generator.Next(1, 11);
                            playerPoints = playerPoints + playerCard;

                            //Säger informationen om kortet
                            Console.WriteLine("Dealern ger dig ett kort, det är värt " + playerCard);
                            Console.WriteLine("\nDu har nu " + playerPoints + " värt i kort");

                            //Frågar om spelaren vill fortsätta hit:a
                            Console.WriteLine("Vill du fortsätta hit:a?");
                            Console.WriteLine("\nSkriv Y för att fortsätta, N för att sluta");

                            string playerChoose = Console.ReadLine().Trim().ToLower();

                            //Spelaren måste välja ett giltigt alternativ
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

                    //Om användaren väljer alternativ 1 ("Stand") så kommer den handligen att utföras
                    //Om man väljer att stand:a kommer spelaren inte få dra fler kort och det är dealerns tur att dra kort
                    else if (playerInput == 2)
                    {
                        Console.WriteLine("Du har valt att stand:a, dealern kommer strax att börja dra kort");
                        Thread.Sleep(2000);
                    }

                    //Om användaren väljer alternativ 1 ("Double Down") så kommer den handligen att utföras
                    else if (playerInput == 3)
                    {
                        Console.WriteLine("Du har valt att double up:a, det belopp du bettade kommer nu att dubblas");

                        //Tillkallar StringToInt metoden som har userInput som parameter (defineras tidigare)
                        StringToInt(userInput);

                        //Substraherar det bettade beloppet igen (eftersom det är Double Down så bettas det dubbelt)
                        money = money - bettableAmmount[userInputInt];

                        Console.WriteLine("\nDu har : " + money + " kvar");

                        Thread.Sleep(2000);

                        Console.Clear();
                        isHitting = true;

                        //Resten fungerar som om man skulle hit:a
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

                    //Om dealern har mindre kort-poäng än spelaren/dealern ha mindre poäng än 21 (Blackjack) så kommer den att dra kort för att få mer poäng (för att vinna över spelaren)
                    while (dealerPoints < 21 && dealerPoints < playerPoints)
                    {
                        dealerCard = generator.Next(1, 11);
                        dealerPoints = dealerPoints + dealerCard;

                        Console.WriteLine("\nDealern ger sig själv ett kort, det är värt " + dealerCard);
                        Console.WriteLine("\nDealern har nu " + dealerPoints + " värt i kort");

                        Thread.Sleep(1000);
                    }

                    //Om dealern får mer än 21 i poäng så förlorar den/hen och spelaren vinner
                    if (dealerPoints > 21)
                    {
                        Console.WriteLine("\n\nDealern bustade");
                        Console.WriteLine("\nDu vann omgången!");
                        money = PlayerWin(money, userInputInt, bettableAmmount);
                        Console.WriteLine(money);
                    }

                    //Men spelaren förlorar om dealern får mer poäng än spelaren
                    else
                    {
                        Console.WriteLine("\n\nDealern fick mer mer poäng än dig!");
                        Console.WriteLine("\nDealern vann omgången!");
                    }

                    //Stänger ner loopen
                    drawingCards = false;
                }

                //Återställer värdena så att man börjar från början, skulle skapa problem man värdena inte återställdes
                playerPoints = 0;
                dealerPoints = 0;
                drawingCards = true;

                //Om spelaren vill spela igen är det bara att svara ja, annars nej och spelet stängs ner
                Console.WriteLine("Vill du köra igen?");
                Console.WriteLine("\nSkriv Y för att fortsätta, N för att sluta");

                string continuePlay = Console.ReadLine().Trim().ToLower();

                while (continuePlay != "n" && continuePlay != "y")
                {
                    Console.WriteLine("\nDu har valt ett ogiltigt alternativ, vänligen försök igen");
                    continuePlay = Console.ReadLine().Trim().ToLower();
                }

                //Stänger ner spelet
                if (continuePlay == "n")
                {
                    wantsToPlay = false;
                }

                //Spelet startas upp igen
                else
                {
                    wantsToPlay = true;
                }
            }
        }

        static void HowToPlayBlackjack()
        {
            //Här använder jag en array eftersom att det finns en fördefinerat mängd alternativ där användaren inte får utöka/kommer inte utökas senare i programmet
            //Information om hur man spelar Blackjack, skrivs ut genom en for-loop undertill
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
                "\nNär man bettar pengar så kommer den mängden att dras från ditt konto (som börjar på 1000)",
                "Om man vinner omgången kommer den mängd man bettade att dubblas och läggas till på ditt konto",
                "Om du förlorar omgången kommer den bettade mängden att inte returneras till dig som spelare, dealern behåller pengarna",
                "\n\nLycka till! Och spendera inte vårdslöst!"
            };

            //Skriver ut informationen som står skriven i arrayen (för enkel utbyggning)
            for (int i = 0; i < info.Length; i++)
            {
                Console.WriteLine(info[i]);
                Thread.Sleep(1000);
            }

            Console.WriteLine("\n\nKlicka på valfri knapp för att återgå till Blackjack menyn");
            Console.ReadKey();
            Blackjack();
        }

        static void Blackjack()
        {
            //La till en loadingscreen för "varför inte?"
            LoadGame();

            //Här använder jag en array eftersom att det finns en fördefinerat mängd alternativ där användaren inte får utöka/kommer inte utökas senare i programmet
            string[] options =
            {
                "1: Börja spela",
                "2: Lär dig spela"
            };

            //Välkomnar användaren till Blackjack
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

            //Skriver ut alternativen som användaren har att välja mellan (de står skrivna i en array lite högre upp i koden för enkel utbyggning)
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }

            //Lite onödig typkonvertering då det funkar lika bra med string argument, men gjorde detta för att få med typkonvertering
            int.TryParse(Console.ReadLine().Trim(), out int playerInput);

            //Måste välja ett giltigt alternativ
            while (playerInput != 1 && playerInput != 2)
            {
                Console.WriteLine("Du har valt ett ogiltigt alternativ, vänligen välj igen");
                int.TryParse(Console.ReadLine().Trim(), out playerInput);
            }

            //Spelaren omdirigeras till det faktiska spelet om hen väljer alternativ 1
            if (playerInput == 1)
            {
                Console.WriteLine("Du har valt: " + options[0]);
                Console.WriteLine("\nSpelet kommer nu att starta");
                Console.Clear();

                PlayBlackjack();
            }

            //Spelaren omdirigeras till "hur man spelar" sektionen om hen väljer alternativ 2
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

            //Behöver ha en CW som inte ligger i loopen då den ska bara skriva ut en gång
            Console.WriteLine("Loading game, please wait...");

            //Här använder jag en array eftersom att det finns en fördefinerat mängd alternativ där användaren inte får utöka/kommer inte utökas senare i programmet
            string[] dots = new string[5];

            //Skrivar ut 5 punkter som ska symbolisera att programmet laddas in
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i] = ".";
                Console.WriteLine(dots[i]);
                Thread.Sleep(500);
            }

            Console.Clear();
        }

        //En metod som drar korten för både spelaren och dealern
        static void DrawCard()
        {
            //Valde användningen av en for-loop då mängden gånger den ska köras är fördefinerat (2 i detta fall)
            for (int i = 0; i < 2; i++)
            {
                //Genererar ett värde för korten
                playerCard = generator.Next(1, 11);
                dealerCard = generator.Next(1, 11);

                //Adderar det beloppet till poängsumman
                playerPoints = playerPoints + playerCard;
                dealerPoints = dealerPoints + dealerCard;

                //Säger vad kortet var värt
                Console.WriteLine("\nDealern ger dig ett kort, det är värt " + playerCard);
                Thread.Sleep(1000);
                Console.WriteLine("\nDealern ger sig själv ett kort, det är värt " + dealerCard);
                Thread.Sleep(1000);
            }

            //Säger hur mycket poäng du och dealern har för tillfället
            Console.WriteLine("\nDu har nu " + playerPoints + " värt i kort");
            Thread.Sleep(500);
            Console.WriteLine("\nDealern har nu " + dealerPoints + " värt i kort");
            Thread.Sleep(500);
        }

        //Kom på lite sent att göra en typkonverteringsmetod som ska kunna användas flera gånger, så gjorde en sent istället för aldrig
        static int StringToInt(string userInput)
        {
            int.TryParse(userInput, out int userInputInt);

            return userInputInt;
        }

        //En metod som tar hand om bettingen om spelaren vinner (så att spelaren kan vinna pengar genom att spela)
        static int PlayerWin(int money, int userInputInt, int[] bettableAmmount)
        {
            money = money + (bettableAmmount[userInputInt] * 2);
            return money;
        }
    }   
}
