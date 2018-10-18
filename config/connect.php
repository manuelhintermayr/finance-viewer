<?php
    include("configuration.php");
    $mysqli = new mysqli($servername, $username, $password, $dbasename);

    function getDbConnection()
    {
        global $servername;
        global $username;
        global $password;
        global $dbasename;
        return new mysqli($servername, $username, $password, $dbasename);
    }

?>