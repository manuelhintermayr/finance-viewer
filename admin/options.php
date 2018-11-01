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
                    case 'setView':
                        setView();
                        break;
                    case 'addUser':
                        addUser();
                        break;
                    case 'removeUser':
                        removeUser();
                        break;
                    case 'updateUser':
                        updateUser();
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
                    'years' => getYearsForUser($row['u_name'])
                );
                $resultArray[] = $user;
            }
            echo json_encode($resultArray);
        }
        else{
            echo '{}';
        }
    }

    function addUser()
    {
        global $mysqli;

        if(isset($_POST['id'])
        &&isset($_POST['username'])
        &&isset($_POST['firstname'])
        &&isset($_POST['lastname'])
        &&isset($_POST['isLocked'])
        &&isset($_POST['password'])
        &&$_POST['username']!=''
        &&$_POST['username']!=' '
        &&$_POST['firstname']!=''
        &&$_POST['firstname']!=' '
        &&$_POST['lastname']!=''
        &&$_POST['lastname']!=' '
        &&$_POST['password']!=''
        &&$_POST['password']!=' ')
        {
            $username = mysql_real_escape_string($_POST['username']);
            $firstname = mysql_real_escape_string($_POST['firstname']);
            $lastname = mysql_real_escape_string($_POST['lastname']);
            $isLocked = $_POST['isLocked']=='' ? '0' : mysql_real_escape_string($_POST['isLocked']);
            $password = encrypt($_POST['password']);
            $years = array("2018", "2019");
            
            if(!strpos($username, ' '))
            {
                $newUser = array(
                    'id' => $_POST['id'],
                    'username' => $username,
                    'origianlUsername'=> $username,
                    'firstname'=> $firstname,
                    'lastname'=> $lastname,
                    'isLocked'=> $isLocked =="0"?FALSE:TRUE,
                    'years' => $years
                );

                $sqlCreateUser = "INSERT INTO `fv_users` (`u_name`, `u_password`, `u_isLocked`, `u_firstName`, `u_lastName`) VALUES ('$username', '$password', '$isLocked', '$firstname', '$lastname');";
                $resultCreateUser = $mysqli->query($sqlCreateUser);
                if($resultCreateUser==1)
                {
                    foreach($years as $year) 
                    {
                        $sqlCreateYear = "INSERT INTO `fv_years` (`y_year`, `y_u_name`) VALUES ('$year', '$username');";
                        $resultCreateYear = $mysqli->query($sqlCreateYear);
                    }

                    echo json_encode($newUser);
                }
                else{
                    header('HTTP/1.1 400 Bad request');
                    echo "Could not create new user. SQL Execution failed."; 
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "Username should not contain whitespaces."; 
            }
        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Not all values are set.";
        }
    }
    function removeUser()
    {
        global $mysqli;

        if(isset($_POST['username']))
        {
            $username = mysql_real_escape_string($_POST['username']);

            //update this method to also remove views when they are being implemented
            $sqlDeleteYears ="DELETE FROM `fv_years` WHERE `fv_years`.`y_u_name` = '$username'";
            $resultDelteYears = $mysqli->query($sqlDeleteYears);
            if($resultDelteYears==1)
            {
                $sqlDeleteUser = "DELETE FROM `fv_users` WHERE `fv_users`.`u_name` = '$username'";
                $resultDeleteUser = $mysqli->query($sqlDeleteUser);  
                if($resultDeleteUser==1)
                {
                    echo json_encode(array('message' => "User deleted."));
                }                  
                else{
                    header('HTTP/1.1 400 Bad request');
                    echo "Could not delete user \"$username\".";
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "Could not delete years for the user \"$username\".";
            }

        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Username is not set.";
        }    
    }

    function updateUser()
    {
        var_dump($_POST);
    }

    function setView()
    {
       if(isset($_POST['username']))
       {
           $_SESSION['m_view_username'] = $_POST['username'];
           echo json_encode(array('message' => "Username for view set."));
       } 
       else{
           header('HTTP/1.1 400 Bad request');
           echo "Username is not set.";
       }
    }

    function actionNotSupported($action)
    {
       header('HTTP/1.1 400 Bad request');
       echo "Action $action is not supported."; 
    }

?>

