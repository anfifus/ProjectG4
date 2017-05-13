<?php
//Informacio de la bd que rebra les dades
	$host = "localhost";
    $user = "id191282_anfifus";
    $pass = "a1991123456";
    $dbName = "id191282_project";
//Connexio a la bd
	$db = mysqli_connect($host,$user,$pass);
//Seleccio de la bd	
	mysqli_select_db($db,$dbName);
//Valors obtinguts d'unity	
        $id = $_POST["id"];
	$hores = $_POST["Hores"];
	$min = $_POST["Minuts"];
	$seg = $_POST["Segons"];
//Obtencio de marcador passant tot a minuts	
	$marcador = ($hores*60)+($min)+($seg/60);
//Obtenim si hi ha contingut guardat
	$contingutGuardar = mysqli_query($db,"SELECT * FROM Victories;");
	
	$quantitatRecords = mysqli_num_rows($contingutGuardar);
//Si no hi ha contingut a guardar
	if($quantitatRecords == 0)
	{
			$records = mysqli_query($db,"INSERT INTO Victories(id,Marcador,Hores,Minuts,Segons) VALUES ('$id','$marcador','$hores','$min','$seg');");//inserim les dades id marcador,hores,minuts,segons
echo $id;
	}
	else//Cas contrari
	{
		if($quantitatRecords > 0)//Si hi ha mes de 0 elements
		{			
			$valors = mysqli_query($db,"SELECT * FROM Victories WHERE Marcador = '$marcador';");//Compara els marcadors amb l'obtingut
                        $numVal = mysqli_num_rows($valors);//Obte el numero de coincidencies
                        if($numVal == 0){//Si no hi ha cap el guardara
			     $records = mysqli_query($db,"INSERT INTO Victories(id,Marcador,Hores,Minuts,Segons) VALUES ('$id','$marcador','$hores','$min','$seg');"); //Guarda el resultat amb la id de l'usuari
                         $records = mysqli_query($db,"ALTER TABLE Victories ORDER BY Marcador ASC;");//Ordena la taula per ordre ascendent
                        }
		}
	}
	mysqli_close($db);//tanca la bd

?>