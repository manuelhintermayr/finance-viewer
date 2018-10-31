<?php
    //TODO: reverse ! on both first ifs
    error_reporting(E_ERROR | E_PARSE);

    $path = $_SERVER['DOCUMENT_ROOT'];
    include($path."/config/login.php");
    $_POST = json_decode(file_get_contents("php://input"),true);

    if(userLoggedIn())
    {
        header('HTTP/1.1 403 Forbidden');
        echo "Not logged in.";
    }
    else{
        if(loggedInUserIsAdmin())
        {
            header('HTTP/1.1 403 Forbidden');
            echo "Logged in user is not admin.";
        }
        else{
            if(isset($_GET) && isset($_GET['action']))
            {
                $action = $_GET['action'];
                switch($action)
                {
                    case 'getUsers':
                        getUsers();
                        break;
                    default:
                        actionNotSupported($action);
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo 'Bad request.'; 
            }
            
            
        }
    }

    function getUsers()
    {
        global $mysqli;

        global $mysqli;
        $sql = "SELECT * FROM fv_users;";
        $result = $mysqli->query($sql);
        
        $resultArray = array();
        if ($result){
            while($row = $result->fetch_assoc()) 
            {
                $user = array(
                    'username' => $row['u_name'],
                    'firstname' => $row['u_firstName'],
                    'lastname' => $row['u_lastName'],
                    'isLocked' => $row['u_isLocked']=="0"?FALSE:TRUE,
                    'years' => array(2017, 2018)
                );
                $resultArray[] = $user;
            }
            echo json_encode($resultArray);
        }
        else{
            echo '{}';
        }
    }

    function actionNotSupported($action)
    {
       header('HTTP/1.1 400 Bad request');
       echo "Action $action is not supported."; 
    }

?>

