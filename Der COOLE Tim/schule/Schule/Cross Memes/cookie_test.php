<?php 
    $data = $_COOKIE["hi"];
    $data++;
    setcookie("hi",$data,0);
    session_start();
    echo $data;
    if ($data == 10) {
        session_destroy();
    }
?>