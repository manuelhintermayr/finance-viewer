<?php
    include("config/login.php");

    $_SESSION['m_user']="";
    $_SESSION['m_password']="";
    
    header("Location: index.php");
    die();
?>