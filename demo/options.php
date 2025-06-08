<?php

    $_POST = json_decode(file_get_contents("php://input"), true);

    if (isset($_GET) && isset($_GET['action'])) {
        $action = filter_input(INPUT_GET, 'action', FILTER_SANITIZE_STRING);
        switch ($action) {
            case 'getUsers':
                getUsers();
                break;
            default:
                actionNotSupported($action);
        }
    } else {
        header('HTTP/1.1 400 Bad request');
        echo 'Bad request.';
    }

    function getUsers() {
        $resultArray = array();

        $resultArray[] = array(
            'username' => 'maxmu',
            'firstname' => 'Max',
            'lastname' => 'Mustermann',
            'isLocked' => true,
            'years' => array(2017, 2018)
        );

        $resultArray[] = array(
            'username' => 'root',
            'firstname' => 'Universal',
            'lastname' => 'User',
            'isLocked' => false,
            'years' => array(2017, 2018)
        );

        $resultArray[] = array(
            'username' => 'sarah_s',
            'firstname' => 'Sarah',
            'lastname' => 'Sash',
            'isLocked' => false,
            'years' => array(2017, 2018)
        );

        echo json_encode($resultArray);
    }

    function actionNotSupported($action) {
        header('HTTP/1.1 400 Bad request');
        echo "Action " . htmlspecialchars($action, ENT_QUOTES, 'UTF-8') . " is not supported.";
    }

?>

