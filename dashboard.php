<?php 
    include("config/generator.php");
    include("config/login.php");
    do_html_header("Dashboard");

    if(loggedInUserIsAdmin())
    {
        redirectToCorrectDashboard();
    }
    else
    {
       echo "<p>Dashboard for normal user. <a href='logout.php'>Logout</a>"; 
    }
?>