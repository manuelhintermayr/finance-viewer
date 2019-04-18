<?php
    include("config/login.php");

    clearCredentials();

    session_unset();
    session_destroy();
    
    header("Location: /");
    die();
?>