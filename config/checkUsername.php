<?php
    function checkCorrectUser($username, $password)
    {
        if($username=="admin"&&$password=="admin")
        {
            return TRUE;
        }
    }
?>