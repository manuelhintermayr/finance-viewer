<?php
/* Generates HTML Header
------------------------ */
function do_html_header($title) {
	echo "<!DOCTYPE html>";
	echo "<html>";
	echo "<head>";
	echo "<meta charset=\"utf-8\" />";
	echo "<title>Finance Viewer - $title</title>";
    echo "<script src=\"js/react.development.js\" crossorigin></script>";
    echo "<script src=\"js/react-dom.development.js\" crossorigin></script>";
    echo "<link rel=\"stylesheet\" href=\"css/bootstrap.min.css\">";
    echo "<link rel=\"stylesheet\" href=\"css/style.css\">";
	echo "</head>";
	echo "<body>";
}

/* Generates HTML Footer
------------------------- */
function do_html_footer() {
	echo "</body>";
	echo "</html>";
}

/* Redirects to url 
------------------- */
function redirect($url) {
    echo "<script>window.location='$url';</script>";
    echo"<h2>Error occured:</h2> Please click <a href='$url'>here</a>."; 
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