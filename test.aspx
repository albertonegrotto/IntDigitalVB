<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test.aspx.vb" Inherits="INTeatroDig.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    	<script src="js/jquery-1.8.2.min.js" type="text/javascript"></script> 
    <script src="js/jquery-ui-1.9.0.custom/js/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
	
	<script type="text/javascript" src="jquery.easing.1.3.js"></script>
	<script src="js/s3Slider.js" type="text/javascript"></script> 

    <title>Página sin título</title>
    	<style type="text/css">
		#divcssmenu
		{
			display:none;
			width: auto;
			position: absolute;
			top:100px;
		}
		
		.move:hover #divcssmenu{display:block;}
		

		#divcssmenu li:hover ul{display:block;}
		
		#divcssmenu li ul{display:none;}


		#divcssmenu ul {
			position: relative;
			display: block;
			margin: 0; 
			padding: 6px; 
			background: white url(images/ovr.png) repeat-x 0 -350px; 
			line-height: 100%; 
			border-radius: 1em; 
			font: normal 1.5em Arial, Helvetica, sans-serif; 
			-webkit-border-radius: 5px; 
			-moz-border-radius: 5px; 
			border-radius: 5px; 
			-webkit-box-shadow: 0 1px 3px rgba(0,0,0, .4); 
			-moz-box-shadow: 0 1px 3px rgba(0,0,0, .4);
			box-shadow: 0px 0px 25px 5px rgba(0, 0, 0, 0.4);
			border-bottom: solid 0.1em #ababab;
			border-right: solid 0.1em #ababab;
			border-top: solid 0.1em white;
			border-left: solid 0.1em white;		}

		#divcssmenu li{
			list-style: none; 

		}	
		#divcssmenu li a{
			margin: 5px 5px; 
			padding: 6px; 
			position: relative; 
			border-bottom: solid 0.1em #ababab;
			border-right: solid 0.1em #ababab;
			border-top: solid 0.1em white;
			border-left: solid 0.1em white;
			background-color: white;
			border-radius: .2em; 
			display: block;

		}

		#divcssmenu li a:hover{
			border-bottom: solid 0.1em white;
			border-right: solid 0.1em white;
			border-top: solid 0.1em #ababab;
			border-left: solid 0.1em #ababab;
			background-color: #58afde;
			color: white;
		}
		

		#divcssmenu li a img{
			border: none;
		}
		#divcssmenu li img{

		}
		.spanli{
			display: inline-block;
			vertical-align: super;
		}

        .tabla
        {
        	position:relative;
        	left:100%;
			display: none;
			margin-left:5em;
		}
		
		.fila{
			display: table-row;
			font-size:smaller;
		}

		.col{

			display: table-cell;
		}

        li li a
        {
        	white-space:nowrap;
        }
        
        

	</style>	
<%--	<script type="text/javascript">
		$(document).ready(function() {
		// Actualizamos el fondo al cargar la pagina
        $('.menu > li').bind('mouseenter',function(){
		var $elem = $(this);
		//$elem.find('a').css('width', '398px');
		$elem.find('.int')
			 .stop(true)
			 .animate({
				'width':'50%',
				'height':'100px',
				'left':'0px'
			 },400,'easeOutBack')
			 .andSelf()
			 .find('.wrap')
		     .stop(true)
			 .animate({'left':'50%'},500,'easeOutBack');
			 var $b = $elem.find('#cssmenu');
			$elem.setTimeout($b.show('slow'), 1000);
			}).bind('mouseleave',function(){
				var $elem = $(this);
				//$elem.find('a').css('width', '199px');
				var $sub_menu = $elem.find('#cssmenu');
				if($sub_menu.length)
					//$sub_menu.hide().css('left','0px');
					$sub_menu.hide();
				
				$elem.find('.int')
					 .stop(true)
					 .animate({
						'width':'0px',
						'height':'0px',
						'left':'0px'},400)
					 .andSelf()
					 .find('.wrap')
					 .stop(true)
					 .animate({'left':'0px'},500);
			});


		});
	</script>
--%>
</head>
<body>
    <form id="form1" runat="server">
        <div id="marco">
    	    <ul class="menu">
		        <li class="move">
			        <img class="int" src="images/int.jpg" alt=""/>
			        <span  class="wrap">
				        <img class="bg" alt="Datos Personales" src="images/btnDatos.png"/>
			        </span>

	        	    <div id='divcssmenu'>
			            <ul>
				            <li><a href="#"><img src="images/actualizaDatos.png" alt=""><span class="spanli">Actualización de Datos</span></a></li>
				            <li class="consub"><a href="#"><img src="images/desvincular.png" alt=""><span class="spanli">Desvinculación de Registro</span></a>
				                    <ul class="tabla">
				<div class="fila"><li class='col'><a href="#"><img src="images/actualizaDatos.png" alt=""><span class="spanli">Sala Teatro Independiente</span></a></li>
				<li class='col'><a href="#"><img src="images/desvincular.png" alt=""><span class="spanli">Grupo Teatro Independiente</span></a></li></div>
				<div class="fila"><li class='col'><a href="#"><img src="images/cambioclave.png" alt=""><span class="spanli">Grupo Comunitario</span></a></li>
				<li class='col'><a href="#"><img src="images/actualizaDatos.png" alt=""><span class="spanli">Grupo Vocacional</span></a></li></div>
				<div class="fila"><li class='col'><a href="#"><img src="images/desvincular.png" alt=""><span class="spanli">Asistente Técnico</span></a></li>
				<li class='col'><a href="#"><img src="images/cambioclave.png" alt=""><span class="spanli">Espectáculo Concertado</span></a></li></div>
				<div class="fila"><li class='col'><a href="#"><img src="images/desvincular.png" alt=""><span class="spanli">Publicación</span></a></li>
				<li class='col'><a href="#"><img src="images/cambioclave.png" alt=""><span class="spanli">Entidad/Sociedad</span></a></li></div>
				<div class="fila"><li class='col'><a href="#"><img src="images/desvincular.png" alt=""><span class="spanli">Evento</span></a></li></div>


			</ul>	
				            </li>
				            <li><a href="#"><img src="images/cambioclave.png" alt=""><span class="spanli">Cambio de Contraseña</span></a></li>
			            </ul>
                    </div>
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
