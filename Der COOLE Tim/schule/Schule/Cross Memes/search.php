<html>
<head>
<link rel="stylesheet" href="./source/search.css">
<meta charset="utf-8" />
<title>Search</title>
</head>
<body>
<form action="index.php">
		<div id="back">
			<input type="submit" name="back" value="Back to Page" id="button">
		</div>
	</form>
	<?php
	if(isset($_POST['search'])) {
	session_start();
	$sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
	$statment = $sql->prepare("SELECT * FROM meme.upload where titel like :suche");
	$statment->execute(array(':suche' => "%".$_POST['search_bar']."%"));
	foreach ($statment as $bild) {
    if (! ($bild['online']))
        continue;
    $a = $bild['idupload'];
    if(isset($_SESSION['user'])) {
        $all_like = $sql->query("SELECT count(upload_idupload) as count FROM meme.likes where user_idUser=$a");
        $all_dislike = $sql->query("SELECT count(upload_idupload) as count FROM meme.dislikes where user_idUser=$a");
    }
    ?>
		<div id="content">
			<div id="titel">
				<p><?php echo $bild['titel']?></p>
			</div>
			<div>
				<a href="content.php?img=<?php echo $bild['idupload']?>">
				<?php if(0 == strcasecmp($bild['ending'], "PNG") || 0 == strcasecmp($bild['ending'], "JPG") || 0 == strcasecmp($bild['ending'], "jpeg") || 0 == strcasecmp($bild['ending'], "GIF")) { ?>
					<img width="460" src="./uploads/<?php echo $bild['idupload'].".".$bild['ending'] ?>" id="image_content">
				<?php } elseif (0 == strcasecmp($bild['ending'], "mp4") || 0 == strcasecmp($bild['ending'], "wmv") || 0 == strcasecmp($bild['ending'], "mov")) { ?>
					<video width="460px" src="./uploads/<?php echo $bild['idupload'].".".$bild['ending'] ?>" controls id="video"></video>
				<?php } ?>
			</a>
			</div>
			<?php if(isset($_SESSION['user'])) {?>
			<div id="opinion" >
				<form method="post">
				<?php
				
    $like = "";
    $dislike = "";
    foreach ($all_like as $single) {
        $like = $single['count'];
    }
    foreach ($all_dislike as $single) {
        $dislike = $single['count'];
    }
    ?>
					<p id="opinion_text" ><?php echo $like ?>  
					<input type="submit" name="like<?php echo $bild['idupload'] ?>" value="Like"> | <?php echo $dislike ?>  
					<input type="submit" name="dislike<?php echo $bild['idupload'] ?>" value="Dislike">
					</p>
				</form>
			</div>
			<?php } ?>
		</div>
		<?php 
    }
}
?>
</body>
</html>