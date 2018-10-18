<?php 
    include("config/login.php");
    do_html_header("Admin Dashboard");

    if(!loggedInUserIsAdmin())
    {
        redirectToCorrectDashboard();
    }
    else
    {
       echo "<p>Dashboard for admin user. <a href='logout.php'>Logout</a>"; 
    }
?>