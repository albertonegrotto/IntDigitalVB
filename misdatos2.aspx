<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="misdatos2.aspx.vb" Inherits="INTeatroDig.misdatos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<link rel="stylesheet" href="estilo.css"/>
	<link rel="stylesheet" href="style.css"/>
	<script src="js/jquery-1.8.2.min.js" type="text/javascript"></script> 
    <script src="js/jquery-ui-1.9.0.custom/js/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
	
	<script type="text/javascript" src="jquery.easing.1.3.js"></script>
	<script src="js/s3Slider.js" type="text/javascript"></script> 
	<script type="text/javascript" src="jquery.easing.1.3.js"></script>
	<link href="menu_assets/styles.css" rel="stylesheet" type="text/css"/>
	<link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css'/>
	


    <link href="js/jquery-ui-1.9.0.custom/css/sunny/jquery-ui-1.9.0.custom.css" rel="stylesheet"
        type="text/css" />
	
	
	<link rel="stylesheet" href="/resources/demos/style.css" />
	<title>INTeatroDigital</title>
	<script type="text/javascript">
		function drilldown(){
			if($('.mnuli2').is(':hidden')){
				$('.mnuli2').slideDown();
			}
			else{
				$('.mnuli2').slideUp();
			}
			
		}
	</script>
	<script type="text/javascript">
		function updateBackground() {
					//alturamain = jQuery("#main").height();
					//$("#main").height(alturamain);
					//anchomisreg = jQuery("#misRegistros").width();
					//$("#misRegistros ul").width(anchomisreg - 40);
			screenWidth = jQuery(".sdt_wrap").width();
			screenHeight = jQuery(".sdt_wrap").height();
			var bg = jQuery(".bg");
			// Proporcion horizontal/vertical. En este caso la imagen es cuadrada
			ratio = 1;
			if (screenWidth/screenHeight > ratio) {
			$(bg).height("auto");
			$(bg).width("100%");
			//$('#s3sliderContent').css('min-height', $(bg).height()*1.2);
			} else {
			$(bg).width("auto");
			$(bg).height("100%");
			}

			// Si a la imagen le sobra anchura, la centramos a mano
			if ($(bg).width() > 0) {
			$(bg).css('left', (screenWidth - $(bg).width()) / 2);
			}

		}
		$(document).ready(function() {
		// Actualizamos el fondo al cargar la pagina
		updateBackground();
		$( "#tabs" ).tabs();
		$(window).bind("resize", function() {
		// Y tambien cada vez que se redimensione el navegador
		updateBackground();
		});
        $('#sdt_menu > li').bind('mouseenter',function(){
		var $elem = $(this);
		//$elem.find('a').css('width', '398px');
		$elem.find('.int')
			 .stop(true)
			 .animate({
				'width':'100%',
				'height':'100px',
				'left':'0px'
			 },400,'easeOutBack')
			 .andSelf()
			 .find('.sdt_wrap')
		     .stop(true)
			 .animate({'left':'100%'},500,'easeOutBack')
			 .andSelf()
			 .find('.sdt_active')
		     .stop(true)
			 .animate({'height':'100px'},300,function(){
			var $sub_menu = $elem.find('.sdt_box');
			if($sub_menu.length){
				var top = "auto";
				//if($elem.parent().children().length == $elem.index()+1)
				//	top = '0';
				$sub_menu.show().animate({'height':top},500, 'easeOutBack');
			}
		});
			}).bind('mouseleave',function(){
				var $elem = $(this);
				//$elem.find('a').css('width', '199px');
				var $sub_menu = $elem.find('.sdt_box');
				if($sub_menu.length)
					//$sub_menu.hide().css('left','0px');
					$sub_menu.hide();
				
				$elem.find('.sdt_active')
					 .stop(true)
					 .animate({'height':'100px'},300)
					 .andSelf().find('.int')
					 .stop(true)
					 .animate({
						'width':'0px',
						'height':'0px',
						'left':'0px'},400)
					 .andSelf()
					 .find('.sdt_wrap')
					 .stop(true)
					 .animate({'left':'0px'},500);
			});


		});
	</script>
</head>
<body>
    <form id="form1" runat="server">
	<div id="backgroundHeader">
		<div id="foto"></div>
	</div>
	<div id="backgroundBody">
		<div id="main">
				<div id='cssmenu'>
					<ul>
					   <li class='active '><a href='index.aspx'><span>Inicio</span></a></li>
					   <li><a href='#'><span>Agregar a Favoritos</span></a></li>
					   <li><a href='preguntasFrecuentes.aspx'><span>Preguntas Frecuentes</span></a></li>
					   <li><a href='#'><span>Formulario de Contacto</span></a></li>
					</ul>
				</div>
		        <div id="tododatos">		
			        <div id="izq2" style="background-color:#dedede;">
				    <!--<div id="menuizq">-->
					    <ul id="sdt_menu" class="sdt_menu">
						    <li class="move">
							    <a  href="#">
								    <img class="int" src="images/int.jpg" alt=""/>
								    <span class="sdt_active"></span>
								    <span  class="sdt_wrap" style="background-color:green;">
									    <img class="bg" alt="Datos Personales" src="images/btnDatos.png"/>
									    <span class="sdt_link"></span>
									    <span class="sdt_descr"></span>
								    </span>
							    </a>
							    <div class="sdt_box">
								    <ul class="mnu">
									    <li class="mnuli"><a href="#">Actualización de Datos</a></li>
									    <li class="mnuli"><a href="#">Desvinculación de Registros</a></li>
									    <li class="mnuli"><a href="#">Cambio de Contraseña</a></li>
								    </ul>
							    </div>
						    </li>
						    <li class="move">
							    <a href="#">
								    <img class="int" src="images/int.jpg" alt=""/>
								    <span class="sdt_active"></span>
								    <span class="sdt_wrap">
									    <img class="bg" alt="Registros" src="images/btnRegistors.png"/>
									    <span class="sdt_link"></span>
									    <span class="sdt_descr"></span>
								    </span>
							    </a>
							    <div class="sdt_box">
								    <ul class="mnu">
									    <li class="mnuli"><a onclick="drilldown();"><span id="regs">Nuevo Registro <span id="flecha">&#10140;</span></span></a></li>
									    <li class="mnuli2"><a href="registroAsistenteTecnico.aspx?accion=A&codigo=0">Asistente Técnico</a></li>
									    <li class="mnuli2"><a href="#">Becario</a></li>
									    <li class="mnuli2"><a href="registroEspectaculo.aspx?accion=A&codigo=0">Espectáculo Concertado</a></li>
									    <li class="mnuli2"><a href="#">Evento</a></li>
									    <li class="mnuli2"><a href="registroGrupo.aspx?accion=A&codigo=0&S=3">Grupo Comunitario</a></li>
									    <li class="mnuli2"><a href="registroGrupo.aspx?accion=A&codigo=0&S=2">Grupo Teatro Indepediente</a></li>
									    <li class="mnuli2"><a href="registroGrupo.aspx?accion=A&codigo=0&S=4">Grupo Vocacional</a></li>
									    <li class="mnuli2"><a href="registroONG.aspx?accion=A&codigo=0">Organismos Oficiales</a></li>
									    <li class="mnuli2"><a href="#">Proyecto de Investigación</a></li>
									    <li class="mnuli2"><a href="registroPublicacion.aspx?accion=A&codigo=0">Publicación</a></li>
									    <li class="mnuli2"><a href="#">Sala teatro independiente</a></li>
									    <li class="mnuli"><a href="#">Actualización de Registro</a></li>
									    <li class="mnuli"><a href="#">Cambio de Responsable</a></li>
								    </ul>
							    </div>
						    </li>
						    <li class="move">
							    <a href="#">
								    <img class="int" src="images/int.jpg" alt=""/>
								    <span class="sdt_active"></span>
								    <span class="sdt_wrap">
									    <img class="bg" alt="Impresión" src="images/btnPrint.png"/>
									    <span class="sdt_link"></span>
									    <span class="sdt_descr"></span>
								    </span>
							    </a>
						    </li>
					    </ul>	
				      <!--   <div id="imagen" style="position:relative; width:100%; height:200px">
				            <img alt="INTeatro" id="logoInteatro" src="images/INTSombreado.png" />
				        </div>
				   </div>-->

			        </div>
            	    <div id="centro2" class="centro">
				        <div id="upanddown">
				            <div id="MisDatosHeader">
                            <asp:Label ID="lblNombre" class="title" runat="server" Text="Label"></asp:Label>
				        </div>
					        <div id="DivUp">
					        <div id="divcodigo" class="divdato"><%=getCodigo()%></div>
					        <div id="divpersona" class="divdato"><%=getPersona()%></div>
					        <div id="divcuit" class="divdato"><%=getCuit()%></div>
					        <div id="divsexo" class="divdato"><%=getSexo()%></div>
					        <div id="divdomicilio" class="divdato"><%=getDomicilio()%></div>
					        <div id="divcpostal" class="divdato"><%=getPCPostal()%></div>
					        <div id="divprovincia" class="divdato"><%=getProvincia()%></div>
					        <div id="divemail" class="divdato"><%=getEmail()%></div>
					        <div id="divtel" class="divdato"><%=getTel()%></div>
					        <div id="divcelu" class="divdato"><%=getCelu()%></div>
					    </div>
					        <div id="DivDown">
						    <div id="tabs">    
							    <ul>        
								    <li><a href="#tabs-1">Mis Registros</a></li>        
								    <li><a href="#tabs-2">Mis Vinculaciones</a></li>        
							    </ul>    
							    <div id="tabs-1">  
                                    <asp:GridView ID="grillamisregistros" runat="server" BackColor="LightGoldenrodYellow" 
                                        BorderColor="Tan" BorderWidth="1px" CellPadding="2" 
                                        DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="None">
                                        <FooterStyle BackColor="Tan" />
                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                            HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" 
                                        SelectCommand="SELECT codigo,
                                                sector, 
                                                denominacion, 
                                                email, 
                                                pagina, 
                                                fechAlta, 
                                                fechBaja 
                                            FROM registro 
                                            WHERE responsable = cast(@codigo as integer) AND
                                                    fechBaja IS NULL
                                            ORDER BY codigo">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="codigo" SessionField="codigo" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
							    </div>    
							    <div id="tabs-2">        
							    </div>    
						    </div>
					    </div>
			            </div>
		            </div>	
	            </div>
	    </div>
	</div>
	<div id="divisorBodyFooter"></div>
	<div id="backbroundFooter">
		    <div id="feet">
                        Av. Santa Fe 1243 7º Piso (C1059ABG) / Ciudad Autónoma de Buenos Aires, República Argentina <img border="0" src="images/flagArgentina.gif" width="16" height="11" alt="República Argentina" /><br />
                        Teléfono (011) 4815-6661<br />
                        Optimizado para <a class="links" href="http://www.microsoft.com/windows/ie/downloads/default.asp" target="_blank">Internet Explorer 4.+</a> <a class="links" href="http://www.getfirefox.com/" target="_blank">Mozilla Firefox</a><br />
                        Versión 1.0.41 29/05/2012
	    </div>
	</div>

    </form>
</body>
</html>
