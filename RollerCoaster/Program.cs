using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------------
        // Get all inputs
        string[] inputs = Console.ReadLine().Split(' ');
        int numberOfSeats = int.Parse(inputs[0]);
        int numberOfRemainingLaps = int.Parse(inputs[1]);
        int numberOfGroups = int.Parse(inputs[2]);

        int[] groups = new int[numberOfGroups];
        
        for (int i = 0; i < numberOfGroups; i++)
            groups[i] = int.Parse(Console.ReadLine());
        // --------------------------------------------------------------------------
        // Dictionary to save the result 
        // The key is the current index in the groups array
        // Value is a memorizedParam objects containing the money and the new index
        Dictionary<int, MemorizedParam> previousResults = new Dictionary<int, MemorizedParam>();
        
        // The result
        long money = 0;
        
        //The current index in the groups array
        int currentIndex = 0;
        
        while (numberOfRemainingLaps > 0)
        {
            // We check if the result has already calculate
            if(previousResults.ContainsKey(currentIndex))
                {
                    var previousResult = previousResults[currentIndex];
                    money += previousResult.MoneyEarned;
                    currentIndex = previousResult.NewIndex;
                }
                else
                {
                    int indexAtStart = currentIndex; // Used as key in the dictionary
                    long moneyBeforeStart = money;
                    
                    // At the begining, all seats are available
                    int availableSeats = numberOfSeats;
                    int numberOfGroupsInside = 0;
        
                    // We check if there are enough seats for the current group
                    while (availableSeats >= groups[currentIndex])
                    {
                        // All groups are already inside the roller coaster
                        if (numberOfGroupsInside >= numberOfGroups)
                            break;
                        
                        numberOfGroupsInside++;
                        
                        int numberOfPerson = groups[currentIndex];
                        money +=  numberOfPerson;
                        availableSeats -= numberOfPerson;
        
                        currentIndex++;
                        if (currentIndex >= numberOfGroups)
                            currentIndex = 0;
                    }
                    previousResults.Add(indexAtStart, new MemorizedParam(money - moneyBeforeStart, currentIndex));
                }
            
            numberOfRemainingLaps--;
        }
        
        Console.WriteLine(money);
    }
}

public class MemorizedParam
    {
        public long MoneyEarned { get; set; }
        public int NewIndex { get; set; }

        public MemorizedParam(long moneyEarned, int newIndex)
        {
            MoneyEarned = moneyEarned;
            NewIndex = newIndex;
        }
    }
