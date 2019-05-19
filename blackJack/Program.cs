

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackJack
{
    class Program
    {
        enum Suits { Dimonds, Hearts, Spades, Clubs }

        enum Ranks { Ace, King, Queen, Jack, Ten, Nine, Eight, Seven, Six }

     

        struct Card
        {
            public Suits Suit;
            public Ranks Rank;
            public int Points;
        }
        static void Main(string[] args)
        {
            string OneMoreGame = "";
            int wins = 0, loses = 0, draws = 0; 
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the game!");

                Console.Write("Please, choose who will start(y-you/c-computer): ");
                string playerStart = Console.ReadLine();

                Card[] deck = new Card[36];

                for (int i = 0; i < 36; i++)
                {
                    deck[i].Suit = (Suits)(i % 4);
                    deck[i].Rank = (Ranks)(i / 4);
                    switch (deck[i].Rank)
                    {
                        case Ranks.Jack:
                            deck[i].Points = 2;
                            break;
                        case Ranks.Queen:
                            deck[i].Points = 3;
                            break;
                        case Ranks.King:
                            deck[i].Points = 4;
                            break;
                        case Ranks.Ace:
                            deck[i].Points = 11;
                            break;
                        case Ranks.Ten:
                            deck[i].Points = 10;
                            break;
                        case Ranks.Nine:
                            deck[i].Points = 9;
                            break;
                        case Ranks.Eight:
                            deck[i].Points = 8;
                            break;
                        case Ranks.Seven:
                            deck[i].Points = 7;
                            break;
                        case Ranks.Six:
                            deck[i].Points = 6;
                            break;
                    }
                }

                Card temp = new Card();
                Random rnd = new Random();
                for (int i = 0; i < rnd.Next(100, 500); i++)
                   {
                       int rand1 = rnd.Next(0, 36), rand2 = rnd.Next(0, 36);
                       temp = deck[rand1];
                       deck[rand1] = deck[rand2];
                       deck[rand2] = temp;
                   }

                Console.WriteLine("Sort deck:");
                foreach (var e in deck)
                    Console.Write(e.Rank + "" + e.Suit + " " + e.Points + "\t");
                Console.WriteLine();

                Card[] playerCards = new Card[10];
                Card[] computerCards = new Card[10];

                int countPlayerCard = 2, countComputerCards = 2;
                int playerPoints = 0, computerPoins = 0;
                bool playerFirst = true;

                if (playerStart.ToUpper() == "Y")
                {
                    playerCards[0] = deck[0];
                    playerCards[1] = deck[1];
                    computerCards[0] = deck[2];
                    computerCards[1] = deck[3];
                }
                else
                {
                    computerCards[0] = deck[0];
                    computerCards[1] = deck[1];
                    playerCards[0] = deck[2];
                    playerCards[1] = deck[3];
                    playerFirst = false;
                }

                for (int j = 0; j < 2; j++)
                {
                    if (playerFirst)
                    {
                        string continuePlay = "";

                        do
                        {

                            if (continuePlay.ToUpper() == "Y")
                            {
                                playerPoints = 0;
                                playerCards[countPlayerCard] = deck[countPlayerCard + countComputerCards];
                                countPlayerCard++;
                            }

                            Console.Write("You have this cards: ");

                            for (int i = 0; i < countPlayerCard; i++)
                            {
                                Console.Write(playerCards[i].Rank + " " + playerCards[i].Suit + "; ");
                                playerPoints += playerCards[i].Points;
                            }


                            Console.WriteLine();
                            Console.WriteLine($"Your points: {playerPoints}");

                            if (playerPoints >= 21)
                                break;


                            Console.WriteLine("Do you want to take one more card?(y-yes/n-no)");
                            continuePlay = Console.ReadLine();
                        } while (continuePlay.ToUpper() == "Y");
                    }
                    else
                    {
                        bool computerContinue = true;
                        do
                        {
                            for (int i = 0; i < countComputerCards; i++)
                            {
                                computerPoins += computerCards[i].Points;
                            }
                            if (computerPoins < 15)
                            {
                                computerPoins = 0;
                                computerCards[countComputerCards] = deck[countPlayerCard + countComputerCards];
                                countComputerCards++;
                            }
                            else if (15 < computerPoins && computerPoins < 19)
                                computerContinue = (rnd.Next(0, 100) % 10 == 0);
                            else
                                computerContinue = false;

                        } while (computerContinue);

                    }
                    playerFirst = !playerFirst;
                }

                Console.Write("Computer cards: ");
                for (int i = 0; i < countComputerCards; i++)
                {
                    Console.Write(computerCards[i].Rank + " " + computerCards[i].Suit + "; ");
                }

                Console.WriteLine();
                Console.WriteLine($"Computer points: {computerPoins}");

                bool playerWin = true;
                //results

                if (computerPoins == 21)
                    playerWin = false;
                else if (computerPoins < 21 && playerPoints > 21)
                    playerWin = false;
                else if (playerPoints > 21 && computerPoins > 21)
                {
                    if (playerPoints - computerPoins > 0)
                        playerWin = false;
                }
                else if (playerPoints < 21 && computerPoins < 21)
                {
                    if (computerPoins - playerPoints > 0)
                        playerWin = false;
                }

                if (countPlayerCard == 2 && playerPoints == 22)
                    playerWin = true;
                else if (countComputerCards == 2 && computerPoins == 22)
                    playerWin = false;

                Console.WriteLine();
                Console.WriteLine("*************RESULT***********");
                Console.WriteLine();

                if (playerPoints == 21 && computerPoins == 21 || playerPoints == computerPoins)
                {
                    Console.WriteLine("It happens! This is draw!");
                    draws++;
                }
                else if (playerWin)
                {
                    Console.WriteLine("Congratulation! You Win!");
                    wins++;
                }
                else
                {
                    Console.WriteLine("Somebody LOOOOOOSER!");
                    loses++;
                }
                Console.WriteLine();
                Console.WriteLine("Play one more time?(yes-y, no-n)");
                OneMoreGame = Console.ReadLine();
            } while (OneMoreGame.ToUpper() == "Y");

            Console.WriteLine();
            Console.WriteLine($"Some statistics: wins={wins}, loses={loses}, draws={draws}");
        }
    }
}





