<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="preguntasFrecuentes.aspx.vb" Inherits="INTeatroDig.preguntasFrecuentes" 
    title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Preguntas Frecuentes</title>
    <link rel="stylesheet" href="style.css"/>
	<link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css'/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="Datos" runat="server" class="container-fluid">
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-6 col-centered" style="text-align: center">
          <h2>Preguntas Frecuentes</h2>
      </div>
      <div class="col-md-4 col-left">
          <asp:Button ID="BtnVolver" runat="server" class="btn btn-primary" Text="Volver" />
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-10 col-center">
	   <ol>
		<li class="pregunta">
			<span class="preguntaP">
				¿A dónde me comunico o con quién me contacto si tengo un inconveniente con la Plataforma de INTeatroDigital?
			</span>
			<span class="preguntaR">
				TODAS las consultas técnicas, reclamos o sugerencias referidas al uso de la Plataforma de INTeatroDigital debe canalizarlas por escrito mediante el <b>“Formulario de Contacto”</b> que figura en la barra superior de la misma,
                sin necesidad estar logueado. En dicha sección debe describir lo más detallado posible el motivo de la consulta o solicitud y puede adjuntar uno o varios archivos si fuera necesario. Al confirmar el envío de ese formulario,
                le llegará un mail al sector ‘Registro’ de la DFPAT (Dirección de Fomento y Promoción de Actividades Teatrales del INT) y desde allí se lo responderán a la brevedad, desde la cuenta de correo:
                <a href="info.intdigital@inteatro.gob.ar" target="_blank"><b>info.intdigital@inteatro.gob.ar</b></a><br />Esa cuenta de correo electrónico ES LA ÚNICA DISPONIBLE PARA REALIZAR CONSULTAS sobre la plataforma de <b>INTeatroDigital</b>
                (además del mencionado <b>“Formulario de Contacto”</b>).
			</span>
		</li>
		<li class="pregunta">
			<span class="preguntaP">
				¿Qué es el Registro Nacional del Teatro Independiente?, ¿Para qué sirve?, ¿Hay alguna reglamentación?
			</span>
			<span class="preguntaR">
				El Registro Nacional del Teatro Independiente es la base de datos de la actividad teatral independiente del <b>Instituto Nacional del Teatro (INT)</b>. Estar registrado es un requisito indispensable para acceder a cualquiera
                de los beneficios que otorga el Organismo (Art. 2º del Decreto Reglamentario 991/97). La Reglamentación vigente puede leerse ingresando aquí: <a href="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm"
                target="_blank"><b>Reglamentacion del Registro</b></a> 
			</span>
		</li>
		<li class="pregunta">
			<span class="preguntaP">
				¿Qué es INTeatroDigital?
			</span>
			<span class="preguntaR">
				<b>INTeatroDigital</b> es un servicio web el cual se realizan trámites “on-line” ante el INT (alta individual, registro, etc.). Es un mecanismo de tramitación mucho más eficiente, que aumenta la calidad del funcionamiento 
                de este Organismo y contribuye a la creación de una administración pública más eficaz en el uso de recursos y más transparente en sus medios.			
			</span>
		</li>
		<li class="pregunta">
			<span class="preguntaP">
				¿Qué implica estar incluido en el Registro Nacional del Teatro Independiente?
			</span>
			<span class="preguntaR">
                Implica, por un lado, poder acceder a las distintas líneas de subsidios que otorga el INT. Por otro lado, el ‘Responsable’ de un ‘Registro’ ante el INT, lo es por las derivaciones de la percepción de beneficios de la Ley 24.800, 
                y pasible de aplicársele las sanciones establecidas en el artículo 29 y 30 de la ley 24.800 si hubiere incumplimiento de las obligaciones contraídas en los convenios de concertación con el INT. Asimismo, todos los ‘Integrantes’ 
                que figuran y que firman en conformidad, son solidariamente responsables entre sí por las mismas derivaciones y pasibles de aplicárseles las mismas sanciones si hubiere incumplimiento de las obligaciones contraídas en los
                convenios de concertación con el INT.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Qué es el ‘Alta Individual’?
			</span>
			<span class="preguntaR">
				El ‘Alta Individual’ es un proceso sencillo de ingreso inicial de datos, para personas Humanas o Jurídicas, el cual se realiza en forma totalmente “on-line”, de forma particular, sin gestores ni intermediarios,
                y que tiene carácter de declaración jurada. El ‘Alta Individual’ genera una cuenta de usuario, identificada por el N° de CUIL/CUIT.
			</span>
		</li>
		<li class="pregunta">
			<span class="preguntaP">
				¿Quiénes deben hacer el ‘Alta Individual’?
			</span>
			<span class="preguntaR">
				El ‘Alta Individual’ es un trámite obligatorio para todos aquellos que vayan a formar parte de algún tipo de Registro, y optativo para aquellos que simplemente quieran dejar sus datos en la base de datos del INT.<br />
                Es necesario ser ciudadano argentino nativo, por opción o naturalizado, con cinco años de residencia en el País y tener como mínimo dieciocho (18) años de edad. En el caso de los menores emancipados, pueden realizar 
                su ‘Alta Individual’ pero deben presentar copia autenticada de la documentación por la cual le ha sido otorgada la emancipación.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo se realiza el ‘Alta Individual’?, ¿Qué documentación hay que presentar?
			</span>
			<span class="preguntaR">
				El ‘Alta Individual’ se realiza a través de la página web del INT (<a href="http://inteatro.gob.ar"target="_blank"><b>www.inteatro.gob.ar</b></a>), accediendo a la plataforma de <b>INTeatroDigital</b> (Registro Digital) 
                y una vez allí, en la opción <b>“Realizar Alta Individual”</b> se completa el formulario (no se debe presentar ninguna documentación ni se debe imprimir ninguna constancia, este proceso es totalmente “on-line”).<br />
                El sistema le solicitará que ingrese una contraseña, la cual es importante <b><u>no olvidar</u></b> ya que será necesaria para realizar cualquier gestión “on-line” ante el INT.<br />
                Por último, debe activar su cuenta de usuario desde el mail que recibirá en la casilla de correo que consignó.<br />
                Esa cuenta de correo debe ser <b><u>“personal”</u></b> y de chequeo habitual y permanente, ya que en ella recibirá la confirmación del procesamiento de todos los trámites que gestione a través de INTeatroDigital,
                y desde esa misma cuenta deberá confirmar sus respectivas validaciones. <br />
                <b><u>IMPORTANTE:</u></b> Si su proveedor de correo es <b>“gmail.com”</b>, <b>“hotmail.com”</b> o <b>“live.com”</b>, tenga la previsión de incorporar a la libreta de direcciones (o a la lista segura de remitentes), 
                las direcciones <b><u>intdigital@inteatro.gob.ar</u></b> e <b><u>info.intdigital@inteatro.gob.ar</u></b> para que ningún mensaje proveniente de esta plataforma sea considerado como “spam” por su proveedor de correo.
			</span>
		</li>	
		<li class="pregunta">
			<span class="preguntaP">
				¿Qué actividades/proyectos deben registrarse?
			</span>
			<span class="preguntaR">
				Deben registrarse los Grupos de Teatro (Independiente, Comunitario o Vocacional), las Salas o Espacios de Teatro Independiente, los Espectáculos Concertados, los Eventos, las Publicaciones (Periódicas o Eventuales)
                y las Entidades/Sociedades vinculadas a la actividad teatral (las cuales deben contar con Nº de Personería Jurídica), que quieran acceder a los distintos subsidios del INT.<br /> 
                También deben registrarse los Asistentes Técnicos vinculados a la actividad teatral.
			</span>
		</li>	
		<li class="pregunta">
			<span class="preguntaP">
				Para registrar una actividad, ¿es necesario tener realizado el ‘Alta Individual’?
			</span>
			<span class="preguntaR">
				Sí, ya que para realizar un ‘Registro’ es necesario que tanto el ‘Responsable’ del mismo como los demás ‘Integrantes’ del proyecto tengan cuentas de usuario en <b>INTeatroDigital</b> asociadas a sus Nº de CUIL/CUIT. 	
			</span>
		</li>	
        <li class="pregunta">
			<span class="preguntaP">
			¿Cómo se realiza el trámite de ‘Registro’?, ¿Qué documentación hay que presentar?
			</span>
			<span class="preguntaR">
				El Grupo, la Sala o Espacio Teatral, la Publicación, el Evento o el Espectáculo Concertado, deberá determinar quién será el ‘Responsable’ ante el INT. Es esa persona únicamente quien debe realizar el trámite de ‘Registro’, 
                el cual se realiza a través de la página web del INT (<a href="http://inteatro.gob.ar"target="_blank"><b>www.inteatro.gob.ar</b></a>), accediendo a la plataforma de <b>INTeatroDigital</b> (Registro Digital) con SU usuario personal,
                y una vez allí, en la sección <b>”Registros” / “Nuevo Registro”</b> se completa el formulario. En esa instancia el ‘Responsable’ incorpora (si, los hubiera) a los demás ‘Integrantes’, quienes también deben tener
                previamente realizada su ‘Alta Individual’.<br />
                Cuando se finaliza la carga de datos en el formulario de ‘Registro’ y se guardan, le llegará al ‘Responsable’ un mail de confirmación (a la casilla de correo consignada en SU ‘Alta Individual’) 
                conteniendo un link que deberá validar, y las demás indicaciones necesarias para concluir el trámite de ‘Registro’ (descarga de constancia, documentación a adjuntar, etc.).					
			</span>
		</li>	
        <li class="pregunta">
			<span class="preguntaP">
				¿Qué significa ser el ‘Responsable’ en un ‘Registro’?
			</span>
			<span class="preguntaR">
				Significa ser el ‘Responsable’ de ese Grupo, Sala, Evento, etc. para todo trámite ante el INT. En el caso de ser beneficiario de un subsidio, será el responsable de cobro, por lo cual deberá contar con Nº de CUIT 
                y ‘Alta de Beneficiario de Pagos’ en el Sistema Integrado de Administración Financiera del Ministerio de Economía y Finanzas Públicas.<br /> 
                Puede ser ‘Responsable’ de un ‘Registro’ una Persona Física o una Entidad/Sociedad con Personería Jurídica.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
			    ¿Se puede ser ‘Responsable’ de distintos Registros?
			</span>
			<span class="preguntaR">
				Sí.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Dónde se presentan las ‘Constancias de Registro’?, ¿Qué documentación se tiene que anexar?
			</span>
			<span class="preguntaR">
				Las ‘Constancias de Registro’  las deberá emitir y descargar únicamente el ‘Responsable’ del mismo desde la sección <b>”Imprimir Constancias”</b> y enviarlas adjuntas a un mensaje de correo electrónico dirigido a la 
                Representación Provincial del INT correspondiente a la Provincia donde declare ejercer la actividad 
                (<a href=http://www.inteatro.gob.ar/Contacto/RepresentacionesProvinciales"target="_blank"><b>Representaciones Provinciales</b></a>).<br />
                <b><u>IMPORTANTE:</u></b> el ‘Responsable’ de un Registro no podrá emitir y descargar la correspondiente ‘Constancia de Registro’ (y finalizar el trámite de ‘Registro’) hasta que no hayan validado su vinculación al mismo
                TODOS los integrantes incorporados al proyecto.<br />
                * Para los Registros de Sala o Espacio Teatral, de Grupo -los 3 tipos-, de Evento, de Publicación y de Espectáculo Concertado:<br />
                 &nbsp;&nbsp; -	En el caso que el ‘Responsable’ sea una Persona Física, adjuntar (además de la ‘Constancia de Registro’): copia de frente y dorso de DNI.<br />
                 &nbsp;&nbsp; -	En el caso que el ‘Responsable’ sea una Entidad/Sociedad, adjuntar (además de la ‘Constancia de Registro’): copia de frente y dorso de DNI del responsable firmante (designado).<br />
                * Para el ‘Registro de Asistente Técnico’, adjuntar (además de la ‘Constancia de Registro’): copia de frente y dorso de DNI.<br />
                * Para el ‘Registro de Entidad’, adjuntar (además de la ‘Constancia de Registro’): copia de frente y dorso de DNI de cada autoridad ingresada, copia de Estatuto, copia de Acta Constitutiva o Primer Acta,
                  copia de Resolución de Personería Jurídica, copia de Certificado de Vigencia y copia de Constancia de AFIP -indefectiblemente en el momento del ‘Registro’-, y copia de Acta con Autoridades Vigentes
                (si la tuviere actualizada al momento de la gestión), sino podrá enviarla más adelante, cuando se lo solicite la DFPAT (Dirección de Fomento y Promoción de Actividades Teatrales del INT).<br />
                * Para el ‘Registro de Sociedad’, adjuntar (además de la ‘Constancia de Registro’): copia de frente y dorso de DNI de cada autoridad ingresada, copia de Contrato Social, copia de Resolución de Personería 
                  Jurídica (si corresponde), copia de Certificado de Vigencia y copia de Constancia de AFIP -indefectiblemente en el momento del ‘Registro’-.<br />	
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				Los ‘Integrantes’ (no ‘Responsables’) de la actividad registrada tienen que realizar algún trámite?
			</span>
			<span class="preguntaR">
				Si, cada ‘Integrante’ que fue incorporado (por parte del ‘Responsable’ del mismo) a una actividad registrada deberá ‘validar su vinculación’ al proyecto, ingresando a la plataforma de INTeatroDigital
                (Registro Digital) con SU usuario personal, y en la sección ‘Mis Vinculaciones’ (que figura en la pantalla principal) tildar la confirmación en el correspondiente proyecto.<br />
                <b><u>IMPORTANTE:</u></b> el ‘Responsable’ de un Registro no podrá emitir y descargar la correspondiente ‘Constancia de Registro’ (y finalizar el trámite de ‘Registro’) hasta que no hayan validado 
                su vinculación al mismo TODOS los integrantes incorporados al proyecto.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				Si se hace el trámite “on-line” pero la constancia no se envía a la Representación Provincial, ¿se considera realizado el trámite de ‘Registro’?
			</span>
			<span class="preguntaR">
				No. El trámite finaliza cuando toda la documentación requerida llega desde la Representación Provincial a la sede central del INT, sector ‘Registro’ de la DFPAT (Dirección de Fomento y Promoción de 
                Actividades Teatrales del INT) y allí se le asigna un Nº de Registro.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo se obtiene una ‘Constancia de Registro’?
			</span>
			<span class="preguntaR">
                Una vez asignado el Nº de Registro, el ‘Responsable” puede emitir y descargar (en formato PDF) la correspondiente ‘Constancia de Registro’, accediendo a la plataforma de <b>INTeatroDigital</b> (Registro Digital) 
                con SU usuario personal, y una vez allí, en la sección <b>“Imprimir Constancias”</b> elige la actividad/proyecto del cual desea imprimir la constancia. La misma no se presenta ante el INT,
                salvo que éste lo requiera particularmente.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo se actualizan datos personales?
			</span>
			<span class="preguntaR">
				Los datos personales se actualizan accediendo a la plataforma de INTeatroDigital (Registro Digital) con SU usuario personal, y una vez allí, en la sección <b>“Datos Personales” / “Actualización de Datos”</b>
                se completa el formulario.<br />
                Para <u>reemplazar</u> el “Currículum Vitae” adjuntado previamente, no es necesario realizar una ‘Actualización de Datos’, lo puede reemplazar desde el menú principal,
                en la sección <b>“Datos Personales” / “Documentación Adjunta”</b>.
			</span>
		</li>	
        <li class="pregunta">
			<span class="preguntaP">
				¿Cómo se actualizan datos de un ‘Registro’?
			</span>
			<span class="preguntaR">
				Los datos del ‘Registro’ los puede actualizar sólo el ‘Responsable’, accediendo a la plataforma de <b>INTeatroDigital</b> (Registro Digital) con SU usuario personal, y una vez allí, en la sección 
                <b>“Registros” / “Actualización de Registro”</b> completa el formulario.<br />
                Toda la documentación adjuntada previamente en el momento del ‘Registro’ o de la ‘Actualización de Registro’ puede ser reemplazada desde el menú principal, en la sección <b>“Datos Personales” / “Documentación Adjunta”</b>,
                sin necesidad de realizar una nueva ‘Actualización de Registro’.<br />
                <u>Esto aplicable a</u>: Fotos, Planos, Listado Total del Equipamiento, Equipamiento Adquirido con Subsidios del INT, Plano Escénico, Planta de Luz y Foto del Espacio Escénico (para Salas o Espacios Teatrales); y
                a: Listado Total del Equipamiento, Equipamiento Adquirido con Subsidios del INT y Trayectoria del Grupo (para Grupos -los 3 tipos-)
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo se actualiza un ‘Registro de Capacitador Técnico’?
			</span>
			<span class="preguntaR">
				Los datos del Registro de Asistente Técnico se pueden actualizar accediendo a la plataforma de <b>INTeatroDigital</b> (Registro Digital) con SU usuario personal, y una vez allí, en la sección 
                <b>“Registros” / “Actualización de Registro”</b> agrega o reemplaza en el formulario los datos consignados originalmente. El campo “Plan de Trabajo” de cada “Especialidad” ingresada no puede editarse,
                en el caso de necesitar modificar la información, se debe “Borrar” la “Especialidad” e ingresarla nuevamente desde “Agregar Especialidad”.<br />
                Para <u>reemplazar</u> el “Currículum Vitae” o la “Foto” adjuntados previamente no es necesario realizar una ‘Actualización de Registro’, los puede reemplazar desde el menú principal,
                en la sección <b>“Datos Personales” / “Documentación Adjunta”</b>.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo se realiza un cambio de responsable?
			</span>
			<span class="preguntaR">
                El designado “nuevo” ‘Responsable’ debe realizar su ‘Alta Individual’ (o la ‘Actualización de Datos’ si ya tenía alta previa) y posteriormente, junto con el “actual” (saliente) ‘Responsable’ 
                deben redactar una nota, firmada por ambos y escaneada, donde soliciten el cambio. Toda la explicación detallada sobre esta gestión se encuentra en la sección <b>“Registros” / “Cambio de Responsable”</b>.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo puede alguien desvincularse de un ‘Registro’?
			</span>
			<span class="preguntaR">
                Un ‘Integrante’ puede realizar su desvinculación de un ‘Registro’ accediendo a la plataforma de <b>INTeatroDigital</b> (Registro Digital) con SU usuario personal, y una vez allí, en la sección
                <b>“Datos Personales” / “Desvincularme de un Registro”</b> elige el proyecto del cual desea desvincularse. Una vez realizado, le llegará un mail al ‘Responsable’ notificando dicha desvinculación
                y solicitando la ‘Actualización del Registro’	
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Los datos ingresados son públicos?
			</span>
			<span class="preguntaR">
				La denominación, la localidad y la Provincia a la que pertenecen serán de acceso público y difundidas por los diversos canales de comunicación del INT. Los demás datos, para ser publicados, 
                deberán ser autorizados por el ‘Responsable’ desde la plataforma de <b>INTeatro Digital</b>, en el momento de realizar el ‘Registro’ o ‘Actualización de Registro’.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Qué pasa si se olvida la clave personal?
			</span>
			<span class="preguntaR">
				El usuario puede recuperar su contraseña olvidada a través de la página web del INT (<a href="http://inteatro.gob.ar"target="_blank"><b>www.inteatro.gob.ar</b></a>),
                accediendo a la plataforma de INTeatroDigital (Registro Digital), y una vez allí, en la opción <b>“Olvidé mi Contraseña”</b> deberá colocar la “respuesta secreta” a la “pregunta de seguridad”
                que consignó al hacer SU ‘Alta Individual’. Luego, recibirá un mail indicándole la clave correspondiente.<br />
                En esa misma sección de ‘Recupero de Contraseña’ se describe cómo puede solicitar el ‘Reemplazo de la cuenta de correo electrónico’ consignada previamente en su alta, o el ‘Reseteo’ de su clave.
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Por qué al momento de emitir para descargar la ‘Constancia de Registro’ no se abre el documento en formato PDF?
			</span>
			<span class="preguntaR">
				La plataforma de INTeatroDigital está optimizada para para navegadores “Google Chrome”, “Microsoft Edge” y “Mozilla Firefox”, y se requiere que en la PC desde donde se accede,
                esté instalado el programa “Adobe Reader”.<br />
                Ante la imposibilidad de generar las planillas en formato PDF, se sugiere asegurarse de que no se esté abriendo la plataforma con otro navegador, verificar que no estén bloqueadas 
                las ventanas emergentes (pop-up) y constatar que en esa PC esté instalada una versión actualizada del programa “Adobe Reader” (puede descargarse desde: 
                <a href=https://get.adobe.com/reader/?loc=es"target="_blank"><b>Get Adobe Reader</b></a>)
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Cómo puedo convertir a formato PFD mis archivos de documentación para adjuntar?
			</span>
			<span class="preguntaR">
                Existen muchos conversores de PDF online; desde el INT recomendamos “PDF24” al cual se accede desde: <a href="https://tools.pdf24.org/es/convertidor-pdf#xToPdf"target="_blank"><b>Convertidor PDF</b></a><br />
                Una vez que se ingresa al mencionado link se debe clickear en el botón “SELECCIONAR ARCHIVOS” y se abrirá una ventana de explorador desde la cual se deberá buscar y seleccionar el archivo a convertir.<br />
                <b><u></u>IMPORTANTE:</u></b> si en este paso se selecciona más de un archivo, el sistema va a generar un único fichero .ZIP que contendrá todos los archivos en formato .PDF (uno por cada archivo seleccionado).<br />
                Una vez que esté/n seleccionado/s el/los archivo/s, se debe clickear en el botón “CONVERTIR A PDF” y comenzará el proceso de conversión. Al cabo de unos segundos, el archivo convertido estará
                disponible para su descarga mediante el botón “DESCARGAR”.<br />
                Para más información puede ver un video instructivo en: <a href="http://serviciosweb.inteatro.gob.ar:8082/intranet/index.php/s/32ToW4o9YQKmJCj"target="_blank"><b>Video Instructivo Convertidor PDF24</b></a><br />
                Si necesitara reducir el “peso” del archivo (ya que el sistema sólo admite adjuntar ficheros de hasta 10 Mb.), con el mismo aplicativo PDF24 puede comprimirlo,
                ingresando a: <a href="https://tools.pdf24.org/es/comprimir-pdf"target="_blank"><b>Como Comprimir PDF</b></a><br />
                Una vez que se ingresa al mencionado link se debe clickear en el botón “SELECCIONAR ARCHIVOS” y se abrirá una ventana de explorador desde la cual se deberá buscar y seleccionar el archivo a convertir.<br />
                <b><u>IMPORTANTE:</u></b> si en este paso se selecciona más de un archivo, el sistema va a generar un único fichero .ZIP que contendrá todos los archivos comprimidos (uno por cada archivo seleccionado).<br />
                Una vez que esté/n seleccionado/s el/los archivo/s, se debe clickear en el botón “COMPRIMIR” y comenzará el proceso de compresión. Al cabo de unos segundos, 
                el archivo convertido estará disponible para su descarga mediante el botón “DESCARGAR”.<br />
                Para más información puede ver un video instructivo en: <a href="http://serviciosweb.inteatro.gob.ar:8082/intranet/index.php/s/AHXXLmRqqyGqZTE"target="_blank"><b>Video Instructivo Comprimir PDF</b></a> 
			</span>
		</li>		
		<li class="pregunta">
			<span class="preguntaP">
				¿Por qué tengo que volver a ingresar mi mail y mi celular cada vez que actualizo datos?
			</span>
			<span class="preguntaR">
				Porque esos 2 campos de datos contienen la “información de contacto”, por lo tanto es imprescindible que esté permanentemente actualizada y vigente para que el INT no pierda comunicación con los usuarios.			
			</span>
			</li>
					<li class="pregunta">
			<span class="preguntaP">
				¿Por qué la Plataforma vuelve a la pantalla de ‘Inicio de Sesión’, sin que haya clickeado en el botón de ‘Inicio’?
			</span>
			<span class="preguntaR">
                Porque la sesión de uso de la plataforma excedió los 20 minutos inactivos. Simplemente debe volver a ingresar su CUIT y Contraseña y podrá continuar normalmente su navegación en la misma pantalla donde se encontraba.
			</span>x
		</li>
	   </ol>
      </div>
      <div class="col-md-1 col-left">
      </div>
    </div>

   </div>
    <script type="text/javascript">
        $("#MenuPreguntas").addClass("active");
    </script>
</asp:Content>
