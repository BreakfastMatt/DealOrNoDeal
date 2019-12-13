using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal
{
    class Program
    {

        //This will be console-based version of the gameshow Deal or No Deal
        static void Main(string[] args)
        {
            //Variables
            int secretCase;  //The case that is randomly selected to be beside the contestant (contains an unknown amount)
            List<int> potentials; //The potential value of the secretCase (this will be used to determine the bankers offer)                //{Might change the type of this variable}
            int offer;  //This will be the offer that the banker has made to the contestant.
            int[] boxes = new int[22];  //there are 22 boxes in the game containing between 1p and £250000.  
                                        //This will contain value of each box.  (i.e. boxes[1] = amount of money in box 1)
            List<int> gameFloor; //This will be the (numbered) boxes left on the gamefloor

        }
    }
}
