<?php
/* RamboBot public alpha */
class V{
    protected $protected;
    function p(){
        return [get_class($this->protected),"P"];
    }
}
function generateKnowledge($l,$o){
    $j = generateResist();
    $knowledge = [ $j[0][0], $j[1][0], $j[1][1], $j[2], $j[4] ];
    if($l[0] != ""){
        $am = [
            $j[0][0]=>[count(array_keys($l, $j[0][0])),count(array_keys($o, $j[0][0]))],$j[1][0]=>[count(array_keys($l, $j[1][0])),count(array_keys($o, $j[1][0]))],$j[1][1]=>[count(array_keys($l, $j[1][1])),count(array_keys($o, $j[1][1]))],$j[2]=>[count(array_keys($l, $j[2])),count(array_keys($o, $j[2]))],$j[4]=>[count(array_keys($l, $j[4])),count(array_keys($o, $j[4]))]
        ];
        $ma = [];
        foreach ($am as $key => $value) {
            $ma[]=round(( $value[1]/count($l)) *100  );
        }
        return array("k"=>$knowledge,"a"=>array_search(max($ma), $ma));
    }else {
        return array("k"=>$knowledge,"a"=>rand(1,4));
    }
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