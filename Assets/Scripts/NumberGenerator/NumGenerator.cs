using System.Linq;
using UnityEngine;

public class NumGenerator : MonoBehaviour
{
    public static int GenerateNumber(int range){
        int sizeOfSample = 1000;
        int[] sample = new int[sizeOfSample];
        int multipleModes=0;

        //Generate 1000 random values from 1 to range
        for (int index =0; index<sizeOfSample; index++ ){
            sample[index] = Random.Range(1,range+1);
        }
        
        int[] mode = {0,0};                                 //Saves number and number of repetitions
        int repetitions;                                    //saves how many times a number repeats

        //Iterates between the range to check how many times a value repeats in the array
        for (int numberToCheck=1; numberToCheck <= range; numberToCheck++ ){
            //number of times a value repeats
            repetitions = sample.Count(s => (s == numberToCheck));

            //if that value is higher than the higher number of repetitions then savenumber and repetitions
            //also set the number of times that happens to 1
            if(repetitions>mode[1]){
                //number and repetitions are saved
                mode[0] = numberToCheck;
                mode[1] = repetitions;
                multipleModes =1;
            }
            //if the number of repetitions is repeated increase the counter for multiple modes
            else if (repetitions==mode[1]){
                multipleModes++;
            }
        }
        //If the mode is repoeated, generate list again
        if (multipleModes>1){
            return GenerateNumber(range);
        }
        //if not, return the number generated
        else {
            return mode[0];
        }
        
    }
}
