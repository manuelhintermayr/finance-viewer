<?php 
    include("config/login.php");
    do_html_header("Admin Dashboard");
    
    if(!userLoggedIn())
    {
        redirect("logout.php");
    }
    else
    {
        if(!loggedInUserIsAdmin())
        {
            redirectToCorrectDashboard();
        }
        else
        {
           echo "<p>Dashboard for admin user. <a href='logout.php'>Logout</a>"; 
        }       
    }
?>