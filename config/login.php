<?php
    session_start(); 
    include("configuration.php");

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
       $url = loggedInUserIsAdmin() ? "admin_dashboard.php" : "dashboard.php";
       echo "<script>window.location='$url';</script>";
       echo"<h2>Error occured:</h2> Please click <a href='$url'>here</a>."; 
       exit();
    }
    
    /* Checks if current logged in user is admin
    -------------------------------------------- */
    function loggedInUserIsAdmin()
    {
        $username = $_SESSION['m_user'];
        return userIsAdmin($username);
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