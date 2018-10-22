<?php

/* Redirects to url from root
----------------------------- */
function redirect($url) {
    $path = $_SERVER['HTTP_HOST'];
    echo "<script>window.location=window.location.protocol + '//' + '".$path."/".$url."';</script>";
    echo"<h2>Error occured:</h2> Please click <a href='".$path."/".$url."'>here</a>."; 
    exit();
}

/* Hashes a text
---------------- */
function encrypt($pwd)
{
    $password_hash = hash('sha512', $pwd);
	return $password_hash;
}

?>