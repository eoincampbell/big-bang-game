<?php

$input = fgets(STDIN);

$inputpieces = explode(" ", $input);

counterPreferenceBot(str_split($inputpieces[0]), str_split($inputpieces[1]));

function counterPreferenceBot($myHandHistory = null, $opponentHandHistory = null)
{


//No Arugments supplied
    if($myHandHistory == null)
    {
        //Random!
        $choices = array("R", "P", "S", "L", "V");
        echo $choices[rand(0, 4)];
    }
    else
    {
        //Arguments supplied
        //Make list of avaiable options
        $r = 0;
        $p = 0;
        $s = 0;
        $l = 0;
        $v = 0;
        foreach($opponentHandHistory as $round => $oppentChoice)
        {
            //Find opponents weaknesses on which it used most.
            if($oppentChoice == "R"){$p++; $v++;}
            else if($oppentChoice == "P"){$s++; $l++;}
            else if($oppentChoice == "S"){$v++; $r++;}
            else if($oppentChoice == "L"){$s++; $r++;}
            else if($oppentChoice == "V"){$l++; $p++;}

            //But avoid falling into whatever clever trap the enemy uses.
            if($myHandHistory[$round] == "R" AND ($oppentChoice == "P" OR $oppentChoice == "V"))
            {   $r--;       
            }else if($myHandHistory[$round] == "P" AND ($oppentChoice == "S" OR $oppentChoice == "L"))
            {   $p--;
            }else if($myHandHistory[$round] == "S" AND ($oppentChoice == "V" OR $oppentChoice == "R"))
            {   $s--;
            }else if($myHandHistory[$round] == "L" AND ($oppentChoice == "S" OR $oppentChoice == "R"))
            {   $l--;
            }else if($myHandHistory[$round] == "V" AND ($oppentChoice == "L" OR $oppentChoice == "P"))
            {   $v--;
            }
        }

        $result = array("R" => $r, "P" => $p, "S" => $s, "L" => $l, "V" => $v);
        //Handsomely stolen code from Micheal Angel to sort those 5 ints into the highest for the right choice.
        $maxvalue = max($result);
        while(list($key,$value)=each($result)){
            if($value==$maxvalue)$maxindex=$key;
        }
        $choice = array("m"=>$maxvalue,"i"=>$maxindex);

        echo $choice['i'];  
    }   
}

?>