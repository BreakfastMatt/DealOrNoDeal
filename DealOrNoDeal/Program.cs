using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal //ctrl + f5 to run
{
    class Program
    { 

        //Function that will randomly populate each of the boxes with one of the, potential, prize amounts.
        public void FillBoxes(ref double[] boxes)
        {
            //values from 1p to £250000.  
            List<double> remainingValues = new List<double>() {0.01,0.1,0.5,1,5,10,50,100,250,500,750,1000,3000,5000,10000,15000,20000,35000,50000,75000,100000,250000};
            Random rnd = new Random();
            //picks random value and assigns it to a box, then remove that value from list of remaining values.  Repeat till each box filled.
            for (int i = 0; i < 22; i++)  
            {
                int index = rnd.Next(0, remainingValues.Count);  
                boxes[i] = remainingValues[index]; 
                remainingValues.RemoveAt(index); 
            }
        }

        //debug function to print out the values of each of the boxes
        public void PrintBoxes(double[] boxes) 
        {
            string display = "Content of Boxes: \n";
            for(int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] >= 1)
                    display += (i+1) + ": £" + boxes[i] + "\n";
                else
                    display += (i+1) + ": " + (boxes[i] * 100) + "p\n";
            }
            Console.WriteLine(display);
        }

        //Will return a list of the boxes currently in play on the gamefloor
        public void DisplayCurrentGamefloor(List<int> floor)
        {
            string display = "Current Gamefloor:\n\n";
            floor.Sort();
            for(int i = 0; i < floor.Count; i++)
            {
                display += "Box " + floor[i] + "\n";
            }
            Console.WriteLine(display);
        }

        //Will simply format a number so that it looks nicer (i.e. print 1p instead of £0.01)
        public string FormatValue(double value)
        {
            if (value >= 1)
                return ( "£" + value );
            else
                return ( (value*100)+ "p" );
        }

        //Display a list of the potential value of the secret box.
        public void PrintPotentialValues(List<double> potentials)
        {
            string display = "Potential values of secret box are as follows: \n";
            for(int i = 0; i < potentials.Count; i++)
            {
                if(potentials[i] >= 1)
                    display +=  "£" + potentials[i] + "\n";
                else
                    display +=  (potentials[i] * 100) + "p\n";
            }
            Console.WriteLine(display);

        }

        //Will simply check if the value entered by the user is an integer.  
        public void SanitiseInput(ref int selection, string msg)
        {
            while (true)
            {
                Console.WriteLine(msg);
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                    break; //if it makes it here then the a valid integer was entered, otherwise it will hit the catch tag.
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }

        //Will simply check if the value entered is a valid answer (y or n)
        public void SanitiseCharInput(ref char answer)
        {
            while (true)
            {
                try
                {
                    answer = Convert.ToChar(Console.ReadLine().ToLower());
                    if (answer == 'y' ||answer == 'n')
                        break; //Input is good so break out of loop
                    else
                        throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide a valid answer (y/n)");
                }
            }
        }
        
        //Bankers offer is calculed based on the potential value of the secret box
        public void BankersOffer(ref double offer, List<double> potentials)
        {
            double avg = Math.Floor(potentials.Average()); //An average of the potentials of the secret box
            avg *= 1.15; //offer an amount slightly above the expected value of the box.
            offer = avg;
        }

        //This will be console-based version of the gameshow Deal or No Deal
        static void Main(string[] args) 
        {
            Program objRef = new Program();
            //Variables
            double secretBox;  //The Box that is randomly selected to be beside the contestant (contains an unknown amount)
            List<double> potentials; //The potential value of the secretBox (this will be used to determine the bankers offer)  
            double offer = 0;  //This will be the offer that the banker has made to the contestant.
            double[] boxes = new double[22];  //there are 22 boxes in the game containing between 1p and £250000.  
                                              //This will contain value of each box.  (i.e. boxes[1] = amount of money in box 1)
            List<int> gameFloor; //This will be the (numbered) boxes left on the gamefloor

            //Game Setup
            objRef.FillBoxes(ref boxes); //Fills each box with one of the random prize amounts. (each box contains a different amount)
            Random rdn = new Random();  int boxNum = rdn.Next(0, 22); 
            secretBox = boxes[boxNum];//select random box to be the secretBox. (remove this box from the gameFloor, but not from the list of potentials
            potentials = new List<double>(){ 0.01,0.1,0.5,1,5,10,50,100,250,500,750,1000,3000,5000,10000,15000,20000,35000,50000,75000,100000,250000};
            gameFloor = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }; //Just Box numbers.
            gameFloor.Remove(boxNum+1); //Remove the secretBox from the gameFloor.

            //Game Time
            Console.WriteLine("Start of game\n");
            Console.WriteLine("Box number " + (boxNum+1) + " was randomly chosen to be your secret box\n");

            bool finished = false;
            while (!finished)
            {
                if(potentials.Count == 2)
                {
                    Console.WriteLine("Since there are only two boxes remaining, you have two options: \n" +
                        "1: accept the banker's previous offer (£" + offer + ")\n" +
                        "2: open the secret box\n" +
                        "Select your option: \n");
                    int opt = Convert.ToInt32(Console.ReadLine());
                    if (opt == 1)
                        Console.WriteLine("You have accepted the bankers offer of £" + offer + ", congratulations.");
                    else
                        Console.WriteLine("You have rejected the bankers offer and have instead chosen to open the secret box which " +
                            "contains " + objRef.FormatValue(secretBox) + ", congratulations.");
                    finished = true;
                    break;
                }
                objRef.DisplayCurrentGamefloor(gameFloor);
                int selection = -1;
                while (true) {
                    objRef.SanitiseInput(ref selection, "Please select a box"); //ensures that the number entered is an integer.
                    if (gameFloor.Contains(selection))
                        break;
                    else
                        Console.WriteLine("Box is not in play on gamefloor");
                }
                Console.WriteLine("Box " + selection + " contained: " +  objRef.FormatValue(boxes[selection-1]) + "\n");
                potentials.Remove(boxes[selection - 1]); //Remove the box from the potential values available
                gameFloor.Remove(selection);

                objRef.PrintPotentialValues(potentials); //Display list of potential values
                objRef.BankersOffer(ref offer, potentials); //An offer is made by the banker.
                Console.WriteLine("The banker has made you an offer of £" + offer + ", would you like to accept it? (y/n)");
                char answer = ' ';
                objRef.SanitiseCharInput(ref answer); //will make sure that the input is a char and is either a 'y' or 'n'
                if (answer == 'y')
                {
                    Console.WriteLine("You have accepted the bankers offer of £" + offer + ", congratulations.");
                    finished = true;
                } //else nothing.
            }

            Console.WriteLine("\nEnd of game, thank you for playing\n");
        }
    }
}
