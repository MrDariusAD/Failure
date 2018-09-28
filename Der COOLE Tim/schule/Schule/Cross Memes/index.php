<html>
<head>
<link rel="stylesheet" href="./source/index.css">
<meta charset="utf-8" />
<title>Project Meme</title>
</head>
<body>
	<div id="kopfzeile">
		<div id="all_button">
			<form action="" method="post">
				<button id="no_button" value="Rmd" name="rmd" style="float: left; ">
					<img alt="Random" src="./source/rmd.png" height="25" >
				</button>
				<button id="no_button" value="Hot" name="hot" style="float: left; ">
					<img id="img_pos" alt="Hot" src="./source/hot.png" height="25" >
				</button>
			</form>
			<form action="search.php" method="post">
			
				<input id="button" style="float: left;" name="search_bar">
				<button id="no_button" name="search" value="Search" type="submit" style="float: left; margin-left: -35">
					<img id="img_pos" alt="Search" src="./source/search.png" height="25">
				</button>
			</form>
			<?php
			session_start();
			if(isset($_SESSION['user'])) {
			?>
		<?php
		
}
		if(isset($_POST['rmd'])) {
		    echo "RMD";
		}

if (isset($_SESSION['user'])) {
    ?>
    	<div id="topbutton">
        	<form action="account.php">
        		<button type="submit" name="account" value="account" id="button" style="float: right;"><img alt="Account" src="./source/small.png"></button>
        	</form>
    	</div>
    	<div id="topbutton">
    		<form method="post">
    			<button type="submit" name="logout" value="Logout" id="button" style="float: right;">Logout</button>
    		</form>
		</div>
			<!-- Wenn der User noch nicht eingeloggt ist wird ein login key angezeigt -->
		<?php }else {?>
		<div id="topbutton">
    		<a href="login.php">
    			<img alt="Login" src="./source/key.png" height="25" id="no_button" style="float: right;">		
    		</a>
		</div>
		<?php }
		if (isset($_SESSION['user'])) {
		?>
		<form action="upload.php">
			<button id="button" style="float: right;">Upload</button>
		</form>
		<?php } ?>
		</div>
	</div>
	<div id="all_content">
<?php
if (isset($_POST['logout'])) {
    session_destroy();
    header("Refresh:0");
}
// SELECT * FROM meme.upload order by datum
$sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
if(isset($_POST['delete'])) {
    //Lösche Content aus Ordner
    foreach ($sql->query("SELECT ending from `upload` where idupload = ".$_POST['delete']) as $content) {
        unlink("./uploads/".$_POST['delete'].".".$content['ending']);
        break;
    }
    //Lösche mit Content zusammenhängende likes
    $sql->query("DELETE FROM `likes` WHERE `likes`.`upload_idupload` = ".$_POST['delete']);
    //Lösche Content
    $sql->query("DELETE FROM `upload` WHERE `upload`.`idupload` = ".$_POST['delete']);
}
$bilder = $sql->query("SELECT * FROM meme.upload order by idupload DESC");
if(isset($_POST['like'])) {
    $sql->query("INSERT INTO `likes`(`user_idUser`, `upload_idupload`) VALUES ( ".$_SESSION['idUser'].", ".$_POST['like'].")");
}
if(isset($_POST['dislike'])) {
    $sql->query("DELETE FROM likes where user_idUser = ".$_SESSION['idUser']." and upload_idupload = ".$_POST['dislike']);
}
// Jeder einzelde upload
foreach ($bilder as $bild) {
    if (! ($bild['online']))
        continue;
    $a = $bild['idupload'];
    if(isset($_SESSION['user'])) {
        $all_like = $sql->query("SELECT count(upload_idupload) as count FROM meme.likes where upload_idupload=$a");
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
    foreach ($all_like as $single) {
        $like = $single['count'];
    }
    ?>
					<p id="opinion_text" ><?php echo $like ?>  
					<?php 
					$liked = "";
					foreach ($sql->query("SELECT COUNT(`user_idUser`) AS has FROM `likes` WHERE `user_idUser` = ".$_SESSION['idUser']." AND `upload_idupload` = ".$bild['idupload']) as $row) {
					    $liked = $row['has'];
					}
					if ($liked == 0) {
					?>
					<button type="submit" name="like" value="<?php echo $bild['idupload'] ?>">Like</button>
					<?php } elseif ($liked == 1) { ?>
					<button type="submit" name="dislike" value="<?php echo $bild['idupload'] ?>">Dislike</button>
					<?php } ?>
					<?php if($_SESSION['idUser'] == $bild['uploader'] || $_SESSION['permission'] >= 7) { ?>
					<button onclick="return confirm('Do you want to delete this Upload')" name="delete" value="<?php echo $bild['idupload'] ?>">Delete</button>
					<?php }?>
					</p>
				</form>
			</div>
			<?php } ?>
		</div>
		<?php 
}
		?>
	</div>
	<div id="bottom_line">
		<a href="impressum.php" id="bottom_text">Impressum </a>
	</div>
</body>
</html>