<?php
    include("config/login.php");
    $_POST = json_decode(file_get_contents("php://input"),true);

    if(isset($_POST) && isset($_POST['username'])) {
        $username = $_POST['username'];
        $password = encrypt($_POST['password']);

        $works = checkCorrectUser($username, $password);
        setUserCredentials($username, $password);
        if($works) {
            $userPrefs = array(  
                'name' => getFirstNameForUsername($username),
                'url' => getUrlForUser($username)
            );
            echo json_encode($userPrefs);
        }
        else {
            header('HTTP/1.1 403 Forbidden');
            echo "Login invalid {user: ".$username."}";
        }
    }
    else {
        header('HTTP/1.1 400 Bad request');
        echo 'Bad request. Please go to <a href="index.php">this</a> page and try again.'; 
    }
?>