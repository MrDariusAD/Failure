<html>
<head>
<link rel="stylesheet" href="./source/login.css">
<meta charset="utf-8" />
<title>Login</title>
</head>
<body>
	<form action="index.php" method="post">
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
				Nickname: <input name="nick">
			</p>
			<p id="text">
				Current Age:<input type="number" name="age">
			</p>
			<p id="text">
				Password: <input type="password" name="pw">
			</p>
			<p id="text" style="color: red;">
				Warning: The password isn't saved encoded. Please don't use an important password
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
			</p>
			<p id="text">
				Merken <input type="checkbox" name="merken">
			</p>
			<input type="submit" name="login" value="Login" id="button">

		</div>
	</form>
	<?php 
	if(isset($_POST["login"]) || isset($_POST["rig"])) {
	?>
		<div id="kopfzeile">
		<p id="text">
		<?php 
$first = "";
$last = "";
$nick = "";
$age = "";
$password = "";
//Login
if(isset($_POST["login"])) {
    $nick = $_POST["nick"];
    $password = $_POST["password"];
    //Überprüft ob alle Felder ausgefühlt wurde
    if($nick == "" || $password == "") {
        echo "You have to fill out all text-fields";
        return ;
    }
    //Stellt Verbindung zur SQL-Datenbank her
    $sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
    //Lässt sich alle ids, Usernamen und Passworter aus der Datenbank ausgeben
    $all_user = $sql->query("select idUser,username,password,permission from meme.user");
    foreach ($all_user as $user) {
        if($nick == $user["username"] && $password == $user["password"]) {
            echo "You have been logged in successfully";
            session_start();
            $_SESSION['user'] = $user['username'];
            $_SESSION['idUser'] = $user['idUser'];
            $_SESSION['permission'] = $user['permission'];
            return;
        }
    }
    echo "No matching account was found";
}
//Register
if(isset($_POST["rig"])) {
    $nick = $_POST["nick"];
    $password = $_POST["pw"];
    $first = $_POST["first"];
    $last = $_POST["last"];
    $age = $_POST["age"];
    //Überprüft ob alle Felder ausgefühlt wurde
     if($first == "" || $last == "" || $nick == "" || $age == "" || $password == "") {
       echo "You have to fill out all text-fields"; 
       return ;
    }
    //Überprüft ob der User schon 12 oder älter ist
    if($age < 12) {
        echo "You are not old enough!";
        return;
    }
    //Stellt Verbindung zur SQL-Datenbank her
    $sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
    //Lässt sich alle ids und Usernamen aus der Datenbank ausgeben
    $all_user = $sql->query("select idUser,username from meme.user");
    $lastID = 0;
    foreach ($all_user as $user) {
        $lastID = $user["idUser"];
        //Überprüft ob der Username schon vergeben ist
        if($user["username"] == $nick) {
            echo "The Nickname is already taken";
            return ;
        }
    }
    $lastID = $lastID+1;
    //Erstellt einen neuen User in der Datenbank
    //$sql->query("INSERT INTO `meme`.`user` (`idUser`, `vorname`, `nachname`, `username`, `age`, `password`) VALUES ('$lastID','$first', '$last', '$nick', '$age', '$password')"); 
    $make = $sql->prepare("INSERT INTO `meme`.`user` (`idUser`, `vorname`, `nachname`, `username`, `age`, `password`) VALUES ( :id , :first , :last , :nick , :age , :password )");
    $make->execute(array('id' => $lastID, ':first' => $first,'last' => $last,':nick' =>$nick,':age' => $age,':password' => $password));
    $make->fetchAll();
    echo "Your account has been created. Congratulations!";
    session_start();
    $_SESSION['user'] = $nick;
    $_SESSION['idUser'] = $user['idUser'];
    $_SESSION['permission'] = 1;
}
?>
</p>
		</div>
		<?php 
	}
		?>
<div id="kopfzeile" >
	<h1 id="text">Rules</h1>
	<h2 id="text" style="color: red;">You accepting the following rules, via login or registration</h2>
		<span id="text">
1.	You have to be at least 12 years old.<br>
2. It is not permitted to upload pornographic, brutal, racial, discriminatory, offensive or right-wing images.<br>
3. All people who are seen in the pictures must have given the consent that this photo may be uploaded. This also applies to pictures of children. This decision can not be accepted through custody.<br>
4. The site may not be used for advertising purposes.<br>
5. It is not allowed to spam pictures.<br>
6. It is not allowed to write indecent, brutal, racial, discriminatory, insulting or right-wing extremist comments.<br><br>
<br>
</span>
	</div>
</body>
</html>