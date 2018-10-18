<?php
    include("config/login.php");

    clearCredentials();
    
    header("Location: index.php");
    die();
?>