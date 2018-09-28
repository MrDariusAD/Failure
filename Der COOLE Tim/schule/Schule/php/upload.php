<html>
<head>
<link rel="stylesheet" href="./source/upload.css">
<meta charset="utf-8" />
<title>Upload new Meme</title>
</head>
<body>
	<form action="index.php">
		<div id="kopfzeile">
			<input type="submit" name="back" value="Back to Page" id="button">
		</div>
	</form>
	<form>
		<div id="kopfzeile">
			<div>
				<input type="file" name="bild" value="Meme Picture" id="text">
			</div>
			<input type="submit" name="upload" value="Upload" id="upload_button">
		</div>
	</form>
</body>
</html>

<?php
$sql = mysqli_connect();
echo mysqli_get_client_info();
echo $sql;
$image_index;
$target_dir = "./uploads/";
$uploadOk = 1;
// Check if image file is a actual image or fake image
if (isset($_POST["upload"])) {
    $check = getimagesize($_FILES["fileToUpload"]["tmp_name"]);
    if ($check !== false) {
        echo "File is an image - " . $check["mime"] . ".";
        $uploadOk = 1;
    } else {
        echo "File is not an image.";
        $uploadOk = 0;
    }
}
mysqli_close($sql);
?>