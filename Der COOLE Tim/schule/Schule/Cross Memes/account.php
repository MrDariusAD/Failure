<html>
<head>
<link rel="stylesheet" href="./source/account.css">
<meta charset="utf-8" />
<meta >
<title>Account von <?php 
session_start(); 
echo $_SESSION['user'];
?></title>
</head>
<body>
<?php 
$sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
$account = "";
foreach ($sql->query("SELECT * FROM meme.user") as $row) {
    if($row['username'] == $_SESSION['user']) {
        $account = $row;
        break;
    }
}
?>
<form action="index.php">
		<div id="content">
			<input type="submit" name="back" value="Back to Page" id="back_button">
		</div>
	</form>
	<div id="content">
		<h1 id="daten">Accountdetails of
<?php 
    echo $_SESSION['user'];
?> </h1>
<form method="post">
		<p id="daten">First Name <input type="text" name="first" value="<?php echo $account['vorname']?>"> </p>
		<p id="daten">Last Name <input type="text" name="last" value="<?php echo $account['nachname']?>">  </p>
		<p id="daten">Nick Name <input type="text" name="nick" value="<?php echo $account['username']?>">  </p>
		<p id="daten">Age <input type="number" name="age" value="<?php echo $account['age']?>">  </p>
		<p id="daten">Password <input type="password" value="" name="pw"></p>
		<input type="submit" name="confirm" value="Confirm changes" id="button">
	</form>
	<a onclick="return confirm('Do you want to delete your account')">
	<form action="delect_formular.php" method="post">
		<input type="submit" name="delect" value="Delect account" id="button">
	</form></a>
	</div>
	<?php if(isset($_POST['delect'])) {
	    session_destroy();
	}
	
	if(isset($_POST['confirm'])) { ?>
	<div id="content">
	<?php  
	    $newFrist = $_POST['first'];
	    $newLast = $_POST['last'];
	    $newNick = $_POST['nick'];
	    $newAge = $_POST['age'];
	    $newPW = $_POST['pw'];
	    if($newFrist != "" && $newFrist != $account['vorname']) {
	        $sql->query("UPDATE `user` SET `vorname` = '$newFrist' WHERE `user`.`idUser` = ".$account['idUser']);
	    }
	    if($newLast != "" && $newLast != $account['nachname']) {
	       $sql->query("UPDATE `user` SET `nachname` = '$newLast' WHERE `user`.`idUser` = ".$account['idUser']);
	    }
	    if($newNick != "" && $newNick != $account['username']) {
	       $sql->query("UPDATE `user` SET `username` = '$newNick' WHERE `user`.`idUser` = ".$account['idUser']);
	    }
	    if($newAge != "" && $newAge != $account['age']) {
	       if($newAge < 12) {
	           echo "Your age has to be at least 12 years";
	           return;
	       }
	       $sql->query("UPDATE `user` SET `age` = '$newAge' WHERE `user`.`idUser` = ".$account['idUser']);
	    }
	    if($newPW != "" && $newPW != $account['password']) {
	       $sql->query("UPDATE `user` SET `password` = '$newPW' WHERE `user`.`idUser` = ".$account['idUser']);
	    }
	    ?>
	<p id="info"><?php echo "Your account-details has been updated" ?></p>
	</div>
	<?php }?>
</body>
</html>