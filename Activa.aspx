<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Activa.aspx.vb" Inherits="INTeatroDig.Activa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link rel="stylesheet" href="prueba.css"/>
    <link rel="stylesheet" href="estilo.css"/>
    <script src="js/jquery-1.8.2.min.js" type="text/javascript"></script>
	<script src="js/easyslider1.7/easyslider1.7/js/easySlider1.7.js" type="text/javascript"></script> 
	<link href="menu_assets/styles.css" rel="stylesheet" type="text/css"/>
	<link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css'/>
	<title>INTeatroDigital</title>
	<script type="text/javascript">
	    function validaUsuario() {
	        var red = getUrlVars()["ReturnURL"];
	        conf = false;
	        if (typeof red == 'undefined'){
	            var conf = false;
	        }
	        else{
	            if (red.substring(0, 21) == 'confirmarRegistracion') {
	                conf = true;
	            }
	        }
	        PageMethods.ValidaUsuario(document.getElementById("usuario").value, document.getElementById("pwd").value, conf, onSuccess, onFailure);

	    }

	    function onSuccess(result) {
	        if (!result) {
	            $('#usuario').addClass('error');
	            $('#mje').html('Revise el CUIT y la Clave Ingresada');
	            $('#helpUsuario2').addClass('helperror');
	            $('#helpUsuario2').slideDown('fast');
	        }
	        else {
	            $('#usuario').removeClass('error');
	            $('#helpUsuario2').removeClass('helperror');
	            $('#helpUsuario2').slideUp('fast');
	            __doPostBack('IniciaSession', document.getElementById("usuario").value + ',' + document.getElementById("pwd").value);

	        }
	    }

	    function onFailure(result) {
	        $('#usuario').addClass('error');
	        $('#mje').html('Revise el CUIT y la Clave Ingresada');
	        $('#helpUsuario2').addClass('helperror');
	        $('#helpUsuario2').slideDown('fast');
    
	    }
	</script>
	<script type="text/javascript">
		function tamaños(){
			$('#slider').css('width', '100%');
			var fotos = $('.item');
			var cantidad = fotos.length;
			ancho = jQuery("#slider").width();
			$('#slider').css('width', ancho);
			//var ancho = $('.item').width();
			//alert(screenWidth);
			$('.item').css('width', ancho);
			$('.lista').css('width', ancho * cantidad);
			//alert('slider:' + $('#slider').width() + '; item:' + $('.item').width() + '; lista:' + $('.lista').width());
		}
        function getUrlVars()
        {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for(var i = 0; i < hashes.length; i++)    {
           hash = hashes[i].split('=');
           vars.push(hash[0]);
           vars[hash[0]] = hash[1];
        }
        return vars;
        }
		$(document).ready(function(){
			var antes = $('#slider').html();
			tamaños();
			var ancho = $('#slider').width();
			$('#slider').easySlider({
				prevText: '<<',
				nextText: '>>',
				firstShow: false,
				lastShow: false,
				continuous: true,
		        numeric: true,
		        auto: true,
		        controlsShow: false,
		        incremento: ancho,
		        pause:3000

			});
			$('#helpUsuario2').slideUp('fast');
			$(window).bind("resize", function() {
				$('#slider').remove();
				var slr = '<div id="slider">' + antes + '</div>';
				$('.centro').html(slr + $('.centro').html());
				tamaños();
				var ancho = $('#slider').width();
				$('#slider').easySlider({
				prevText: '<<',
				nextText: '>>',
				firstShow: false,
				lastShow: false,
				continuous: true,
		        numeric: true,
		        auto: true,
		        controlsShow: false,
		        incremento: ancho
			});
				});

			$('#usuario').focusin(function(){
				if($('.error').length == 0){
					$('#usuario').addClass('focus');
					$('#mje').html('Ingrese los 11 números del CUIT (sin guiones)');
					$('#helpUsuario2').slideDown('fast');
				}

			});
			$('#usuario').focusout(function(){
				if($('.error').length == 0){
					$('#usuario').removeClass('focus');
					$('#helpUsuario2').slideUp('fast');
				}
			});
});

function response() {
    window.location = "InicioIndiv.aspx";
}
	</script>
	<script language="javascript" type="text/javascript">
		<!--
		function bookmarksite(title, url) {
			if (document.all)
				window.external.AddFavorite(url, title);
			else if (window.sidebar)
				alert("Presione Ctrl+D para marcar como favorito");
		}
		//-->
	</script>


</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
		<div id="backgroundHeader">
		    <%--<div id="foto"></div>--%>
                <div class="col-md-8 col-centered page-institucional" style="text-align: center">
                  <h2>Su Cuenta de Usuario ha sido Activada</h2>
                </div>	
                <br />
                <br />
            <div class="cuentaActivada">
                <a class="botonInicio" href="index.aspx">Inicie Sesión</a>
            </div>

	    </div>
<%--	    <div id="backgroundBody">
		    <div id="main">
			    <div id='cssmenu'>
				<ul>
				   <li class='active '><a href='index.aspx'><span>Inicio</span></a></li>
				   <li><a href="javascript:bookmarksite('Instituto Nacional del Teatro','http://www.inteatro.gob.ar')"><span>Agregar a Favoritos</span></a></li>
				   <li><a href='preguntasFrecuentes.aspx'><span>Preguntas Frecuentes</span></a></li>
				   <li><a href='contacto.aspx'><span>Formulario de Contacto</span></a></li>
				   <li><a href='http://www.inteatro.gob.ar'><span>Web INT</span></a></li>

				</ul>
			</div>
			    <div id="izq">
				<div id="login">
					<div id="titulo">Inicie Sesión</div>
					<div id="loginCont">
						<div class="login-wrap">
							<span class="label">CUIT/CUIL:</span>
							<div id="divUsuario" class="divInput">
								<input id="usuario" class="login" type="text">
								<div id="helpUsuario2" class="helpnormal">
									<p id="mje"> Ingrese los 11 números del CUIT (sin guiones)</p>
								</div>
							</div>
							<div id="divPwd" class="divInput" >
								<span class="label">Contraseña:</span>
								<input id="pwd" class="login" type="password">
							</div>
						</div>
							<a id="forgot" href="RecupContra.aspx">Olvidó su contraseña?</a>
							<input type="button" id="botonLogin" value="Iniciar Sesión" class="login" onclick="validaUsuario();"/>
                            <span id="todavia"> Todavía no realizó su Alta Individual?<br/> Hágalo aquí</span>
							<input type="button" id="botonAlta" value="Alta Individual" class="login" onclick="response();"/>
					</div>
				</div>
				<div style="position:relative; width:100%; margin-top:20px" >
			        <img id="imagen" src="http://inteatro.gob.ar/content/images/logo.png" />
				</div>
			</div>
			    <div id="centro" >
			    			<div id="imgTitulo">
			    <img src="images/plataforma.jpg" alt="inteatrodigital" /> 

			</div>
                 <img src="images/activar.png" />   
			    </div>
		    </div>
	    </div>--%>
	    <div id="divisorBodyFooter"></div>
            <br />
            <br />
            <br />
            <br />
	    <div id="backbroundFooter">
	        <div id="feet">
                        Av. Santa Fe 1243 7º Piso (C1059ABG) / Ciudad Autónoma de Buenos Aires, República Argentina <img border="0" src="images/flagArgentina.gif" width="16" height="11" alt="República Argentina" /><br />
                        Teléfono (011) 4815-6661 - interno 203<br />
                        Optimizado para <a class="links" href="http://www.microsoft.com/windows/ie/downloads/default.asp" target="_blank">Internet Explorer 4.+</a> <a class="links" href="http://www.getfirefox.com/" target="_blank">Mozilla Firefox</a><br />
                        Versión 2.0.1 04-01-2013</div>
	    </div>
	 </form>
</body>
</html>
