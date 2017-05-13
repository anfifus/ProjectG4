$(document).on('ready',function(){
   
   var controlPass = false;//Bool que controla el pass dins del javascript
   var controlMail = false;//Bool que controla l'email dins del javascript
   var dbParam;
   var BoolPass;//Bool que controla el pass que passarem al php
   var BoolEmail;//Bool que controla l'email que passarem al php
   // var method = $('#main form').attr('method');//A traves de la id del body va al form i aconsegueix el contingut de l'atribut pertinent.
   
      $('#main form').on('submit',(function(e){
              var nomUsuari = $('#username').val();
               var password = $('#pass').val();
               var email = $('#mail').val();
      e.preventDefault();
   //var pass1 = document.form.pass1.value;//A traves del document aconsegueix els noms fins arribar a aquell que vol aconseguir el valor.

               controlPass = segPass(password);//Obtenim si es correcta o no el password utilitzant un boolea
               controlMail = segEmail(email);//Obtenim si es correcta o no l'email utilitzant un boolea
               
               BoolPass = controlPass;//Passem el valor boolea a la variable que enviarem al php
               BoolEmail = controlMail;//Passem el valor boolea a la variable que enviarem al php
       $.ajax({                        
            url: 'Registre.php',//url: Es on passarem la url on anirem utilitzant l'ajax jquery
            type: 'post',//es la forma d'enviar les dades pot ser post o get
            data: {pass: BoolPass,mail:BoolEmail ,Iuser:nomUsuari,Ipass:password,Iemail:email},//on passarem les dades a enviar al php normalment format json
            dataType: 'text',//definirem quin tipus de valor retornarem
            error: function(jqXHR,estat,errorThrown){console.log(estat); console.log(errorThrown);},//Funcio que s'executa quan hi ha un error a la solicitud i que passa 3 valors el primer jqXHR un objecte, el segon un string que mostra possibles errors com: null,timeout,error,abort i parseerror i la tercera un string que mostra un error HTTP status per exemple Not Found i Internal Server Error
            timeout:10000,//Dona un temps d'espera a la solicitud
            success:function(data,textStatus,jqXHR){ $('#h3').text("Status:"+textStatus+", Data: "+data);}//Funcio que passa 3 valors i s'executa quan te exit l'enviament de dades passa tres valors data un valor de tipus definit al valor dataType, textStatus descriu com un string l'estatus
          });
   }));
     
});
function segPass(pass1)
{
   if(pass1.length < 6)
   {
         alert("Error es menor a 6");
         controlPass = false;
   }
   else
   {
       alert("Es correcte");
       controlPass = true;
   }
   return controlPass;
}
function segEmail(email)
{
   if(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email))
   {
        alert("Es correcte el correu electronic");
        controlMail = true;
   }
   else
   {
        alert("Es incorrecte el correu electronic");
        controlMail = false;
   }
   return controlMail;
}