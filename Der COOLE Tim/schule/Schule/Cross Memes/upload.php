<html>
<head>
<link rel="stylesheet" href="./source/upload.css">
<meta charset="utf-8" />
<title>Upload new Meme</title>
</head>
<body>
<?php
session_start();
if (isset($_SESSION['user'])) {
    if ($_SESSION['permission'] >= 1) {
        ?>
	<form action="index.php" method="post">
		<div id="kopfzeile">
			<input type="submit" name="back" value="Back to Page" id="button">
		</div>
	</form>
	<form method="post" enctype="multipart/form-data">
		<div id="kopfzeile">
			<h1 id="text">Upload an Image</h1>
			<div>
				<p id="text">
					Titel: <input type="text" name="titel" maxlength="250">
				</p>
				<input type="file" name="bild" value="Meme Picture" id="text" />
			</div>
			<input type="submit" name="upload" value="Upload" id="upload_button" />
			<p id="text">
			<?php
        $input = "";
        $sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
        // Check if image file is a actual image or fake image
        if (isset($_POST["upload"])) {
            $path = $_FILES['bild']['name'];
            $ext = pathinfo($path, PATHINFO_EXTENSION);
            if(0 == strcasecmp($ext, "mp4") ||
                0 == strcasecmp($ext, "png") ||
                    0 == strcasecmp($ext, "jpg") ||
                        0 == strcasecmp($ext, "gif") ||
                            0 == strcasecmp($ext, "jpeg") ||
                                 0 == strcasecmp($ext, "mov") ) {
                $file_name = "";
                foreach ($sql->query('SELECT * FROM `upload` WHERE 1 order BY idupload DESC LIMIT 1') as $row) {
                    $file_name = $row['idupload'];
                }
                $file_name = $file_name + 1;
                $destination = "./uploads/" . $file_name . ".$ext";
                $sql = new PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
                $name = $_POST['titel'];
                $destination = "./uploads/" . $file_name . ".$ext";
                $datum = getdate();
                $datum = $datum['year'] . "-" . $datum['mon'] . "-" . $datum['mday'];
                foreach ($sql->query("Select idUser as user from user where username = '".$_SESSION['user']."'") as $row) {
                    $sql->query("INSERT INTO `meme`.`upload` (`idupload`, `uploader`, `datum`, `titel`, `ending`, `online`) VALUES ('$file_name', '".$row['user']."', '$datum', '$name', '$ext', 1)");
                    break;
                }
                move_uploaded_file($_FILES['bild']['tmp_name'], $destination);
                } else {
                    if(!isset($ext)) {
                        echo "No File Selected";
                        return;
                    }
                    echo $ext." is not an allowed format";
                }
        }
        ?>
		</p>
		</div>
	</form>
	<?php } else { ?>
	<form action="index.php">
		<div id="kopfzeile">
			<input type="submit" name="back" value="Back to Page" id="button">
		</div>
	</form>
	<div id="nologin_frame">
		<h1 style="color: red;">You are not allowed to upload anything</h1>
		<h2>If this not the case. Contact the Admin</h2>
		<h3>racer4308@gmail.com</h3>
	</div>
	<?php
    }
} else {
    ?>
   <form action="index.php">
		<div id="kopfzeile">
			<input type="submit" name="back" value="Back to Page" id="button">
		</div>
	</form>
	<div id="nologin_frame">
		<h1 style="color: red;">You must be logged in to upload anything</h1>
	</div>
	
	
	<?php }?>
</body>
</html>

