<?php
    $path = $_SERVER['DOCUMENT_ROOT'];
    include($path."/config/configuration.php");

    $mysqli = getDbConnection();

    function getDbConnection()
    {
        global $servername;
        global $username;
        global $password;
        global $dbasename;
        return new mysqli($servername, $username, $password, $dbasename);
    }
?>