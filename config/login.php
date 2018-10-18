<?php
    include("configuration.php");
    
    function userIsAdmin($username)
    {
        global $adminName;
        return $username == $adminName;    
    }    

    function checkCorrectUser($username, $password)
    {
        if($username=="admin"&&$password=="admin")
        {
            return TRUE;
        }
        elseif($username=="root"&&$password=="admin")
        {
            return TRUE;
        }
        else{
            return FALSE;
        }
    }
?>