<html>
<head>
<link rel="stylesheet" href="./source/login.css">
<meta charset="utf-8" />
<title>Login</title>
</head>
<body>
	<form action="index.php">
		<div id="kopfzeile">
			<input type="submit" name="back" value="Back to Page" id="button">
		</div>
	</form>
	<form method="post">
		<div id="kopfzeile">
			<p id="text">Sign up</p>
			<p id="text">
				First Name: <input name="first">
			</p>
			<p id="text">
				Last Name: <input name="last">
			</p>
			<p id="text">
				Nickname: <input name="nick" id="textpanel">
			</p>
			<p id="text">
				Age: <input name="age">
			</p>
			<p id="text">
				Password: <input type="password" name="pw">
			</p>
			<input type="submit" name="rig" value="Sign up" id="button">
		</div>
		</form>
		<form method="post">
		<div id="kopfzeile">
			<p id="text">Login</p>
			<p id="text">
				Nickname: <input name="nick">
			</p>
			<p id="text">
				Password: <input type="password" name="password" >
				Test: <input type="submit" name="test" >
			</p>
			<input type="submit" name="login" value="Login" id="button">

		</div>
	</form>
</body>
</html>

<?php 
$link = mysqli_connect();
$first = "";
$last = "";
$nick = "";
$age = "";
$password = "";
//Login
if(isset($_POST["login"])) {
    $nick = $_POST["nick"];
    $password = $_POST["password"];
    if($nick == "" || $password == "") {
        echo "You have to fill out all text-fields";
        return ;
    }
    $all_names = mysqli_query($link, "SELECT * FROM phpmyadmin.user");
    
    echo "Hi";
    echo $all_names + "f";
    echo "Hi";
    
//     foreach () {
        
//     }
}
//Register
if(isset($_POST["rig"])) {
    $nick = $_POST["nick"];
    $password = $_POST["pw"];
    $first = $_POST["first"];
    $last = $_POST["last"];
    $age = $_POST["age"];
    echo $first ;echo $last ;echo $nick;echo $age; echo $password;
     if($first == "" || $last == "" || $nick == "" || $age == "" || $password == "") {
       echo "You have to fill out all text-fields"; 
       return ;
    }
    if($age < 18) {
        echo "You are not old enough!";
        return;
    }
}
if(isset($_POST["test"])) {
    echo "Test";
    $link = mysqli_connect();
    $all_names = new PDO('mysql:host=localhost;dbname=mydb', 'root', '');
    $s = $all_names->query("select * from mydb.film");
    foreach ($s as $name) {
        
        echo $name['idFilm'];
    }
}
?>