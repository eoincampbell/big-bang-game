<?php
/* RamboBot public beta */
class V{
    protected $protected;
    function p(){
        return [get_class($this->protected),"P"];
    }
}


function generateKnowledge($l,$o){

    $j = generateResist();
    $knowledge = [ $j[0][0], $j[1][0], $j[1][1], $j[2], $j[4], mt_rand(0,1), (100/3) ];
    if($l[0] != ""){
        $am = [
            $j[0][0]=>[count(array_keys($l, $j[0][0])),count(array_keys($o, $j[0][0]))],$j[1][0]=>[count(array_keys($l, $j[1][0])),count(array_keys($o, $j[1][0]))],$j[1][1]=>[count(array_keys($l, $j[1][1])),count(array_keys($o, $j[1][1]))],$j[2]=>[count(array_keys($l, $j[2])),count(array_keys($o, $j[2]))],$j[4]=>[count(array_keys($l, $j[4])),count(array_keys($o, $j[4]))]
        ];
        $ma = [];
        $y=0;
        foreach ($am as $key => $value) {
            $ma[]=round(( $value[1]/count($o)) * 100 );
            $y+=round(( $value[1]/count($o)) * 100 );
        }
        $mu=$ma[(array_search(max($ma), $ma))];
        $me=($y / count($am));
        $mo=[];
        foreach ($ma as $key => $value) {
            if($value==0.0){$mo[]=$key;}
        }
        $i=$knowledge;
        unset($i[5]);unset($i[6]);
        foreach ($mo as $no) {
            unset($i[$no]);
        }
        if(($mu-$me)>$knowledge[6]){            
            $e=array_search(max($ma), $ma);
        }elseif (count($o) > $knowledge[6]) {
            if(count($mo)>0){
                $e=array_rand($i);
            }else{
                $e=array_search(max($ma), $ma);
            }
        }else {
            $e=array_rand($i);
        }
    }else {
        $e=mt_rand(0,4);
    }
    return array("k"=>$knowledge,"a"=>$e);
}

function generateResist (){
    $t = (string)float;
    $f=["S",'f'];
    $b=(string)$f;
    $z=new V();$u=$z->p();$x = 'u';
    $d=$f[1];
    $resist =[$$d[0][0][0],$$x[0],ucfirst(substr($b, 2,1)),$$x[0][1][0],ucfirst(substr($t, 1,1))  ];
    return $resist;
}

$h=generateKnowledge(str_split($argv[1]),str_split($argv[2]));

switch ($h["k"][$h["a"]]) {
    case $h["k"][0]:
        echo $h["k"][3];
        exit;
    case $h["k"][1]:
        echo $h["k"][2];
        exit;
    case $h["k"][2]:
        echo $h["k"][4];
        exit;
    case $h["k"][3]:
        echo $h["k"][1];
        exit;
    case $h["k"][4]:
        echo $h["k"][3];
        exit;

    default:
        exit;
}
?>