﻿using System;
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

        //Debug function to print out the values of each of the boxes.
        public void PrintBoxes(ref double[] boxes)
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

        //This will be console-based version of the gameshow Deal or No Deal
        static void Main(string[] args) 
        {
            Program objRef = new Program();
            Program debugRef = new Program();//no reason to do this, but it's easier to see when debugging is done. (will be killed at end, look for: @DEBUG)

            //Variables
            int secretCase;  //The case that is randomly selected to be beside the contestant (contains an unknown amount)
            List<int> potentials; //The potential value of the secretCase (this will be used to determine the bankers offer)  
            int offer;  //This will be the offer that the banker has made to the contestant.
            double[] boxes = new double[22];  //there are 22 boxes in the game containing between 1p and £250000.  
                                              //This will contain value of each box.  (i.e. boxes[1] = amount of money in box 1)
            List<int> gameFloor; //This will be the (numbered) boxes left on the gamefloor

            //Game Setup
            objRef.FillBoxes(ref boxes);
            debugRef.PrintBoxes(ref boxes);  //@DEBUG
        }
    }
}
