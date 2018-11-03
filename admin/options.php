<?php
    error_reporting(E_ERROR | E_PARSE);

    $path = $_SERVER['DOCUMENT_ROOT'];
    include($path."/config/login.php");
    $_POST = json_decode(file_get_contents("php://input"),true);

    if(!userLoggedIn())
    {
        header('HTTP/1.1 403 Forbidden');
        echo "Not logged in.";
    }
    else{
        if(!loggedInUserIsAdmin())
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
                    case 'setPassword':
                        setPassword();
                        break;
                    case 'addYear':
                        addYear();
                        break;
                    case 'removeYear':
                        removeYear();
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
            $password = encrypt( encryptSHA($_POST['password']) );
            $years = array("2018", "2019");
            
            if(!strpos($username, ' '))
            {
                $sqlCreateUser = "INSERT INTO `fv_users` (`u_name`, `u_password`, `u_isLocked`, `u_firstName`, `u_lastName`) VALUES ('$username', '$password', '$isLocked', '$firstname', '$lastname');";
                $resultCreateUser = $mysqli->query($sqlCreateUser);
                if($resultCreateUser==1)
                {
                    foreach($years as $year) 
                    {
                        $sqlCreateYear = "INSERT INTO `fv_years` (`y_year`, `y_u_name`) VALUES ('$year', '$username');";
                        $resultCreateYear = $mysqli->query($sqlCreateYear);
                    }

                    $newUser = array(
                        'id' => $_POST['id'],
                        'username' => $_POST['username'],
                        'origianlUsername'=> $_POST['username'],
                        'firstname'=> $_POST['firstname'],
                        'lastname'=> $_POST['lastname'],
                        'isLocked'=> $isLocked =="0"?FALSE:TRUE,
                        'years' => $years
                    );

                    echo json_encode($newUser);
                }
                else{
                    header('HTTP/1.1 400 Bad request');
                    echo "Could not create a new user. SQL Execution failed."; 
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
        global $mysqli;

        if(isset($_POST['username'])
        &&isset($_POST['firstname'])
        &&isset($_POST['lastname'])
        &&isset($_POST['isLocked'])
        &&$_POST['username']!=''
        &&$_POST['username']!=' '
        &&$_POST['firstname']!=''
        &&$_POST['firstname']!=' '
        &&$_POST['lastname']!=''
        &&$_POST['lastname']!=' ')
        {
            $username = mysql_real_escape_string($_POST['username']);
            $firstname = mysql_real_escape_string($_POST['firstname']);
            $lastname = mysql_real_escape_string($_POST['lastname']);
            $isLocked = $_POST['isLocked']=='' ? '0' : mysql_real_escape_string($_POST['isLocked']);

            $sqlUpdateUser = "UPDATE `fv_users` SET `u_isLocked` = '$isLocked', `u_firstName` = '$firstname', `u_lastName` = '$lastname' WHERE `fv_users`.`u_name` = '$username'";
            $resultUpdateUser = $mysqli->query($sqlUpdateUser);
            if($resultUpdateUser==1)
            {
                $updatedUser = array(
                    'username' => $_POST['username'],
                    'firstname'=> $_POST['firstname'],
                    'lastname'=> $_POST['lastname'],
                    'isLocked'=> $isLocked =="0"?FALSE:TRUE,
                );

                echo json_encode($updatedUser);
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "Could not update the user. SQL Execution failed."; 
            }
        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Not all values are set.";
        }
    }

    function setPassword()
    {
        global $mysqli;

        if(isset($_POST['username'])
        &&isset($_POST['newPassword'])
        &&$_POST['username']!=''
        &&$_POST['username']!=' '
        &&$_POST['newPassword']!=''
        &&$_POST['newPassword']!=' ')
        {
            $username = mysql_real_escape_string($_POST['username']);
            $password = encrypt( encryptSHA($_POST['newPassword']) );

            $sqlSetPassword = "UPDATE `fv_users` SET `u_password` = '$password' WHERE `fv_users`.`u_name` = '$username'";
            $resultSqlSetPassword = $mysqli->query($sqlSetPassword);

            if($resultSqlSetPassword==1)
            {
                echo json_encode(array('message' => "Password set."));
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "Could not set password. SQL Execution failed."; 
            }
        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Not all values are set.";
        }
    }

    function addYear()
    {
        global $mysqli;

        if(isset($_POST['username'])
        &&isset($_POST['year'])
        &&$_POST['username']!=''
        &&$_POST['username']!=' '
        &&$_POST['year']!=''
        &&$_POST['year']!=' ')
        {
            $username = mysql_real_escape_string($_POST['username']);
            $newYear = mysql_real_escape_string($_POST['year']);
            
            if(ctype_digit($newYear)&&$newYear>0)
            {
                $sqlCreateYear = "INSERT INTO `fv_years` (`y_year`, `y_u_name`) VALUES ('$newYear', '$username');";
                $resultCreateYear = $mysqli->query($sqlCreateYear);
                if($resultCreateYear==1)
                {
                    $newYearObject = array('newYear' => $newYear);
                    echo json_encode($newYearObject);
                }
                else{
                    header('HTTP/1.1 400 Bad request');
                    echo "Could not create a new year entry. SQL Execution failed."; 
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "Value for new year is not valid."; 
            }
        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Not all values are set.";
        }
    }

    function removeYear()
    {
        global $mysqli;

        if(isset($_POST['username'])
        &&isset($_POST['year'])
        &&$_POST['username']!=''
        &&$_POST['username']!=' '
        &&$_POST['year']!=''
        &&$_POST['year']!=' ')
        {
            $username = mysql_real_escape_string($_POST['username']);
            $year = mysql_real_escape_string($_POST['year']);
            
            $sqlYearsForUser = "SELECT * FROM `fv_years` WHERE `y_u_name` = '$username'";
            $resultYearsForUser = $mysqli->query($sqlYearsForUser);
            
            if(($resultYearsForUser->num_rows)>1)
            {
                $sqlDeleteYear = "DELETE FROM `fv_years` WHERE `fv_years`.`y_year` = '$year' AND `fv_years`.`y_u_name` = '$username';";
                $resultDeleteYear = $mysqli->query($sqlDeleteYear);
                if($resultDeleteYear==1)
                {
                    echo json_encode(array('message' => "Year deleted."));
                }
                else{
                    header('HTTP/1.1 400 Bad request');
                    echo "Could not delete the year entry. SQL Execution failed."; 
                }
            }
            else{
                header('HTTP/1.1 400 Bad request');
                echo "There must be at least on year left."; 
            }
        }
        else{
           header('HTTP/1.1 400 Bad request');
           echo "Not all values are set.";
        }
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

