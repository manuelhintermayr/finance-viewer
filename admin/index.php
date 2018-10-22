<?php 
    $path = $_SERVER['DOCUMENT_ROOT'];
    include($path."/config/login.php");
    
    if(!userLoggedIn())
    {
        redirect("../logout.php");
    }
    else
    {
        if(!loggedInUserIsAdmin())
        {
            redirectToCorrectDashboard();
        }
        else
        {
//START INSERT GENERATED CODE HERE            
            ?>

           <p>Dashboard for admin user. <a href='..\logout.php'>Logout</a></p>

    <?php
//END INSERT GENERATED CODE HERE
        }       
    }
?>