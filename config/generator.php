<?php
/* Generates HTML Header
------------------------------ */
function do_html_header($title) {
	echo "<!DOCTYPE html>";
	echo "<html>";
	echo "<head>";
	echo "<meta charset=\"utf-8\" />";
	echo "<title>Finance Viewer - $title</title>";
	echo "</head>";
	echo "<body>";
}

/* Generates HTML Footer
------------------------------ */
function do_html_footer() {
	echo "</body>";
	echo "</html>";
}

?>