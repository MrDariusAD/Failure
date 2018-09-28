
<html>
<body>
<span>
<?php
if (isset($_POST['delect'])) {
    session_start();
    $nick = $_SESSION['user'];
    $sql = new  PDO('mysql:host=localhost;dbname=meme', 'root', 'geheim');
    $id = "";
    foreach ($sql->query("Select * from user") as $row)  {
        if($row['username'] == $nick) {
            $id = $row['idUser'];
            $sql->query("DELETE FROM `dislikes` WHERE `dislikes`.`user_idUser` = ".$id);
            $sql->query("DELETE FROM `likes` WHERE `likes`.`user_idUser` = ".$id);
            $sql->query("DELETE FROM `kommentare` WHERE `autor` = ". $id);
            $sql->query("DELETE FROM `upload` WHERE `uploader`= ".$id);
            $sql->query("DELETE FROM `user` WHERE `user`.`idUser` = ".$id);
            echo "Your account has been deleted";
            session_destroy();
        }
    }
    
}
?>
</span>
<p>Continue on the main page</p>
<a href="index.php"> <span>Main Page</span> </a>
</body>
</html>