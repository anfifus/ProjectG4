<?php
//Informacio de la bd que rebra les dades
    $host = "localhost";
    $user = "id191282_anfifus";
    $pass = "a1991123456";
    $dbName = "id191282_project";
//1 Connecta amb l'usuari que administrara la bd
    $db = mysqli_connect($host,$user,$pass);
//2 Selecciona la base de dades a utilitzar	
    mysqli_select_db($db,$dbName);
//Dades que utilitzarem
	$login = $_POST["nom"];
	$clau = $_POST["pass"];
//--------------------	
$val = mysqli_query($db,"SELECT id FROM Usuaris WHERE nom = '$login' and pass = '$clau' ;");		
                
	if(mysqli_num_rows($val) > 0)
	{
		$object = mysqli_fetch_object($val);//Transforma a objecte el resultat obtingut del select		
		echo $object->id;//Aconsegueix la id
	}
        else
       {
             echo var_dump($object->id);//Canviar la forma d'exposar l'error
               
       }

 ?>