<?php
//Informacio de la bd que rebra les dades
    $host = "localhost";
    $user = "id191282_anfifus";
    $pass = "a1991123456";
    $dbName = "id191282_project";
//1 Connecta amb l'usuari que administrara la bd
    $db = mysqli_connect($host,$user,$pass);
//2 Connecta l'usuari amb la bd que vol utilitzar
	mysqli_select_db($db,$dbName);
       $records = mysqli_query($db,"SELECT * FROM Victories INNER JOIN Usuaris ON Victories.id = Usuaris.id ORDER BY Victories.Marcador ASC;");
        $elements = mysqli_num_rows($records);
   
   while($contador < $elements)
   {
      $mostrarRecords = mysqli_fetch_object($records);//Aconseguim cada record
      $criptic = md5($mostrarRecords->pass);//Encriptem la contrasenya per md5
      echo("$mostrarRecords->id:$mostrarRecords->nom:$criptic:$mostrarRecords->Marcador;");//Passem a unity l'usuari, la id,la contrasenya i el seu marcador

      $contador++;
   }
mysqli_close($db);
?>