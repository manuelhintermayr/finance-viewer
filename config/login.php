<?php
    session_start(); 
    $path = $_SERVER['DOCUMENT_ROOT'];
    include($path."/config/configuration.php");
    include($path."/config/generator.php");
    include($path."/config/connect.php");

    /* Checks if user is currently logged in
    ---------------------------------------- */
    function userLoggedIn()
    {
        $session = $_SESSION;
        if(empty($_SESSION['m_user'])||empty($_SESSION['m_password']))
        {
            return FALSE;
        }
        return checkCorrectUser($_SESSION['m_user'], $_SESSION['m_password']);
    }

    /* Redirects to correct dashboard depending on user type
    -------------------------------------------------------- */
    function redirectToCorrectDashboard()
    {
        $url = loggedInUserIsAdmin() ? "admin" : "dashboard";
        redirect($url);
    }

    /* Gets Url for the username
    ---------------------------- */
    function getUrlForUser($username)
    {
        return userIsAdmin($username) ? "admin" : "dashboard";
    }
    
    /* Checks if current logged in user is admin
    -------------------------------------------- */
    function loggedInUserIsAdmin()
    {
        if(isset($_SESSION['m_user']))
        {
            $username = $_SESSION['m_user'];
            return userIsAdmin($username);            
        }
        else{
            return FALSE;
        }

    }

    /* Checks if user is admin
    -------------------------- */
    function userIsAdmin($username)
    {
        global $adminName;
        return $username == $adminName;    
    }  
      
    /* Checks if given user credentials are correct
    ----------------------------------------------- */
    function checkCorrectUser($username, $password)
    {
        global $adminName;
        global $adminPw;

        if($username==$adminName&&$password==encrypt($adminPw))
        {
            return TRUE;
        }

        return checkUserCredentialsInDb($username, $password);
    }

    /* Get FirstName for username 
    ----------------------------- */
    function getFirstNameForUsername($username)  
    {
        global $mysqli;

        if(userIsAdmin($username))
        {
            return "Administrator";
        }

        $sql = "SELECT * FROM fv_users where u_name = '$username';";
        
        $result = $mysqli->query($sql);
        
        if ($result){
            while($row = $result->fetch_assoc()) 
            {
                return $row['u_firstName'];
            }
        }
            
        return "[undefined]";   
    }

    /* Check for correct user credentials in database
    ------------------------------------------------- */
    function checkUserCredentialsInDb($username, $password)
    {
        global $mysqli;
        $sql = "SELECT * FROM fv_users;";
        $result = $mysqli->query($sql);
        
        if ($result){
            while($row = $result->fetch_assoc()) 
            {
                if($username==$row['u_name']&&$password==$row['u_password'])
                {
                    if($row['u_isLocked']=="0")
                    {
                        return TRUE;    
                    }
                }
            }
        }

        return FALSE;
    }

    /* Sets user credentials 
    ------------------------ */
    function setUserCredentials($username, $password)
    {
        $_SESSION['m_user']=$username;
        $_SESSION['m_password']=$password;
    }

    /* clears credentials
    -------------------- */
    function clearCredentials()
    {
        $_SESSION['m_user']="";
        $_SESSION['m_password']="";
    }
?>