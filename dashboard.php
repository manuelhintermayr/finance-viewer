<?php 
    include("config/login.php");
    do_html_header("Dashboard");

    if(!userLoggedIn())
    {
        redirect("logout.php");
    }
    else
    {
        if(loggedInUserIsAdmin())
        {
            redirectToCorrectDashboard();
        }
        else
        {
            echo "<p>Dashboard for normal user. <a href='logout.php'>Logout</a>"; 
        }    
    }
?>