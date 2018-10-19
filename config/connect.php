<?php
    include("configuration.php");
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