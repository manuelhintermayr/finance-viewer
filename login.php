<?php
    include("config/login.php");

    if(isset($_SERVER['HTTP_X_REQUESTED_WITH']) && $_SERVER['HTTP_X_REQUESTED_WITH'] == 'XMLHttpRequest') {
        $username = $_POST['user'];
        $password = encrypt($_POST['pass']);

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
            echo "Login invalid for user: ".$username;
        }
    }
    else {
        header('HTTP/1.1 404 Not Found');
        echo 'Please go to <a href="index.php">this</a> page.'; 
    }
?>