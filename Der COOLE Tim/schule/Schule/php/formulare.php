<<!DOCTYPE unspecified PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<body>
<?php
$name = "";
$alter = "";
$antwort = "";

if (isset($_POST["Speichern"])) {
    $name = $_POST["tName"]; 
    $alter = $_POST["tAlter"];
    $antwort = "Ihr Name ist ".$name."<br>"."Ihr Alter ist ".$alter;
}
echo $antwort;
?>
<button>Back</button>
</body>
</html>