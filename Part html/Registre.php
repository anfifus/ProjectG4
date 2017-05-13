<?php
header("Content-Type: application/json; charset=UTF-8");
//Informacio de la bd que rebra les dades
    $host = "localhost";
    $user = "id191282_anfifus";
    $pass = "a1991123456";
    $dbName = "id191282_project";
//1 Connecta amb l'usuari que administrara la bd
	$db = mysqli_connect($host,$user,$pass);
//2 Connecta l'usuari amb la bd que vol utilitzar
	mysqli_select_db($db,$dbName);
//Informacio a transmetre

	$UserName = $_POST['Iuser'];//Usuari
	$pass = $_POST['Ipass'];//Password
	$mail = $_POST['Iemail'];//Email
        $Cpass = $_POST['pass'];//Permet controlar que el password sigui correcta
        $Cmail = $_POST['mail'];//Permet controlar que l'email sigui correcta
        
        
   $existUser = mysqli_query($db,"SELECT * FROM Usuaris WHERE nom = '$UserName'");//Compara l'usuari amb els usuaris guardats si hagues coincidencia la guardaria
   $users = mysqli_num_rows($existUser);//Emmagatzema el numero de coincidencies
if($Cpass == "true" && $Cmail == "true" && $users == 0){//Permis que confirma si el password i l'email estan correctes
    
    $res = mysqli_query($db,"INSERT INTO Usuaris(nom,pass,email) VALUES ('$UserName','$pass','$mail');");//Inserta un nou usuari
 
 echo "funciona";
}
else
{
    echo "No funciona per un error d'usuari o contrasenya o email";
}
mysqli_close($db);
?>