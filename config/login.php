<?php
    session_start(); 
    include("configuration.php");

    /* Checks if user is currently logged in
    ------------------------------ */
    function userLoggedIn()
    {
        $session = $_SESSION;
        if(empty($_SESSION['m_user'])||empty($_SESSION['m_password']))
        {
            return FALSE;
        }
        return checkCorrectUser($_SESSION['m_user'], $_SESSION['m_password']);
    }

    
    
    function userIsAdmin($username)
    {
        global $adminName;
        return $username == $adminName;    
    }  
      

    function checkCorrectUser($username, $password)
    {
        if($username=="admin"&&$password=="admin")
        {
            return TRUE;
        }
        elseif($username=="root"&&$password=="admin")
        {
            return TRUE;
        }
        else{
            return FALSE;
        }
    }
?>