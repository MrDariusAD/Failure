<?php 
$daten = array("Berlin" => "Deutschland", "Rom" => "Italien", "Paris" => "Frankreich","Warschau" => "Polen");
foreach ($daten as $stadt => $land) {
    echo $stadt." ist die Hauptstadt von ".$land."<br>";
}
?>