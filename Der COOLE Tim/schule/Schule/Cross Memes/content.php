<html>
<head>
<title>Content</title>
<link rel="stylesheet" href="./source/content.css">
</head>
<body>
<?php 
session_start();
// Wird ausgefürt wenn der img tag gesetzt wurde (Dient nur zu errorvermeidung im Falle eines Hotlinks)
if(isset($_GET['img'])) {
    // Die id unter der der jeweilige Content erreichbar ist
    $id = $_GET['img'];
    // Dateiformat des Contents zB: 'JPG'
    $file_tag = "";
    $sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
    // Titel des Contents
    $upload_titel = "";
    foreach ($sql->query("SELECT ending, titel from upload where idupload= ".$id) as $row) {
        $file_tag = $row['ending'];
        $upload_titel = $row['titel'];
        break;
    }
?>
<?php
if(isset($_POST['delect'])) {
    $delete_id = $_POST['delect'];
    $sql->query("DELETE FROM `meme`.`kommentare` WHERE `idkommentare`='".$delete_id."';
");
}
    
// Wird ausgeführt wenn der User Etwas in die Kommentierzeile reingeschrieben und Enter gedrückt hat
if(isset($_POST['comment']) && $_POST['comment'] != "" && isset($_SESSION['user'])) {
    // Fügt Kommentar in Datenbank hinzu
    $sql->query("INSERT INTO `meme`.`kommentare` (`inhalt`, `autor`, `idupload`) VALUES ('".$_POST['comment']."', '".$_SESSION['idUser']."', '".$id."')");
}
?>
	<form action="index.php" method="post">
		<div id="content">
			<input type="submit" name="back" value="Back to Page" style="margin: 20">
		</div>
	</form>
	<div id="content">
		<h1 id="titel">
			<?php echo $upload_titel ?><br>
		</h1>
	</div>
	<div id="content">
	<div id="image">
		<a href="./uploads/<?php echo $id.".".$file_tag ?>"> 
				<?php if(0 == strcasecmp($file_tag, "PNG") || 0 == strcasecmp($file_tag, "JPG") || 0 == strcasecmp($file_tag, "jpeg") || 0 == strcasecmp($file_tag, "GIF")) { ?>
					<img width="60%" src="./uploads/<?php echo $id.".".$file_tag ?>" id="image_content">
				<?php } elseif (0 == strcasecmp($file_tag, "mp4") || 0 == strcasecmp($file_tag, "wmv") || 0 == strcasecmp($file_tag, "mov")) { ?>
					<video width="60%" src="./uploads/<?php echo $id.".".$file_tag ?>" controls id="video"></video>
				<?php } ?>
		</a>
		</div>
	</div>
	<?php if(isset($_SESSION['user'])) {?>
	<div id="content">
	<form method="post">
		<input name="comment" type="text" id="write_comment">
	</form>
	<h1 id="titel" style="margin-left: 0%;">Comments</h1>
	<?php 
	   $all_comments = $sql->query("select kommentare.idkommentare, user.idUser, user.username, kommentare.inhalt from kommentare
inner join user on kommentare.autor = user.idUser
where kommentare.idupload = $id");
	   foreach ($all_comments as $comment) {
	?>
	<div id="comment">
	<div>
		<p id="titel" style="margin-left: -80%;"><?php echo $comment['username']?>: <?php echo $comment['inhalt'] ?> </p>
	</div>
	<?php 
	$permission = "";
	foreach ($sql->query("Select permission from user where username = '".$_SESSION['user']."'") as $row) {
	   $permission = $row['permission'];
	}
	if($permission >= 5 || $_SESSION['user'] == $comment['username']) {?>
	<div id="delete_button">
		<form method="post">
			<button name="delect" value="<?php echo $comment['idkommentare'] ?>"><img alt="Delete Comment" src="./source/dump.png"></button>
		</form>
	</div>
	<?php }?>
	</div>
	<?php }?>
	</div>
<?php }} else {?>
<div id="content">
<h1 id="titel" style="color: red;">
	<?php echo "NO HOTLINK"; ?>
</h1>
</div>
<?php }?>
</body>
</html>