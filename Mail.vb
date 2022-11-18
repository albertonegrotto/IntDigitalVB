Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.IO

Public Module Mail
    Dim cn As New SqlClient.SqlConnection(SqlConex)

    Public TIPO_PERSONA As Integer = 1
    Public TIPO_REGISTRO As Integer = 2

    'Individual Física
    Public MAIL_ALTA_INDIV_FIS As Integer = 10
    Public CONF_ALTA_INDIV_FIS As Integer = 11
    Public MAIL_MODIF_INDIV_FIS As Integer = 21
    Public CONF_MODIF_INDIV_FIS As Integer = 22

    'Individual Jurídica
    Public MAIL_ALTA_INDIV_JUR As Integer = 30
    Public CONF_ALTA_INDIV_JUR As Integer = 31
    Public MAIL_MODIF_INDIV_JUR As Integer = 41
    Public CONF_MODIF_INDIV_JUR As Integer = 42

    'Registro
    Public MAIL_ALTA_REGISTRO As Integer = 50
    Public CONF_ALTA_REGISTRO As Integer = 51
    Public MAIL_MODIF_REGISTRO As Integer = 60
    Public CONF_MODIF_REGISTRO As Integer = 61

    'Integrante
    Public MAIL_BAJA_INTEGRANTE_A_INTEGRANTE As Integer = 70
    Public MAIL_BAJA_INTEGRANTE_A_PROVINCIA As Integer = 71
    Public MAIL_BAJA_INTEGRANTE_A_RESPONSABLE As Integer = 80

    Public MAIL_MODIF_INTEGRANTE_A_RESPONSABLE_FIS As Integer = 90
    Public CONF_MODIF_INTEGRANTE_A_RESPONSABLE_FIS As Integer = 91
    Public MAIL_MODIF_INTEGRANTE_A_RESPONSABLE_JUR As Integer = 100
    Public CONF_MODIF_INTEGRANTE_A_RESPONSABLE_JUR As Integer = 101

    'Desvinculación Integrante
    Public MAIL_DESVINCULACION_INTEGRANTE_A_INTEGRANTE As Integer = 110
    Public MAIL_DESVINCULACION_INTEGRANTE_A_PROVINCIA As Integer = 111
    Public MAIL_DESVINCULACION_INTEGRANTE_A_RESPONSABLE As Integer = 112

    Public Function SendMail(ByVal sMailTo As String, _
                             ByVal sSubject As String, _
                             ByVal sBody As String) As String

        Dim correo As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient
        Dim receptor As String

        correo = New System.Net.Mail.MailMessage()
        correo.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")

        Dim wregistro As Decimal = 0
        If System.Web.HttpContext.Current.Session("CodRegistro") IsNot Nothing Then
            Try
                wregistro = System.Web.HttpContext.Current.Session("CodRegistro")
            Catch ex As Exception
                wregistro = 0
            End Try
        Else
            Try
                Dim quien As usuario = CType(System.Web.HttpContext.Current.Session("usuario"), usuario)
                wregistro = quien.Usuario
            Catch ex As Exception
                wregistro = 0
            End Try
        End If

        ' Agregar testing
        sSubject = "*TEST* " + sSubject

        receptor = sMailTo
        correo.To.Add(receptor)
        correo.Subject = sSubject
        sBody += "<br />"

        Dim wmensaje As String = ""
        Dim werrorval As Integer = 0

        Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(sBody, Nothing, "text/plain")

        'Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString("<img src=cid:companylogo>" + "<br />" + sBody + "<img src=cid:pielogo>", Nothing, "text/html")
        Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString("<img src=cid:companylogo>" + "<br />" + sBody, Nothing, "text/html")
        'Dim logo As New LinkedResource("D:\FUENTES\INTeatroDigVB_Mobile\INTeatroDig\images\plataforma.jpg")
        'Dim pie As New LinkedResource("D:\FUENTES\INTeatroDigVB_Mobile\INTeatroDig\images\pie_email.png")
        Dim logo As New LinkedResource("C:\inetpub\wwwroot\IntDig_Prueba\images\plataforma.jpg")
        'Dim pie As New LinkedResource("C:\inetpub\wwwroot\IntDig_Prueba\images\pie_email.png")

        logo.ContentId = "companylogo"
        'pie.ContentId = "pielogo"
        htmlView.LinkedResources.Add(logo)
        'htmlView.LinkedResources.Add(pie)
        correo.AlternateViews.Add(plainView)
        correo.AlternateViews.Add(htmlView)

        'correo.Body = "<html><head>"
        'correo.Body += "</head><body>"
        'correo.Body += sBody
        'correo.Body += "</body></html>"

        correo.IsBodyHtml = True
        correo.Priority = System.Net.Mail.MailPriority.Normal
        'End of Body
        Dim ht As Integer = receptor.IndexOf("hotmail")
        If ht = -1 Then

            correo.From = New System.Net.Mail.MailAddress(MAIL_SENDER)
            smtp.Host = MAIL_SMTP_SERVER
            smtp.Credentials = New System.Net.NetworkCredential(MAIL_CREDENTIALS_USER, MAIL_CREDENTIALS_PASSWORD)
            smtp.EnableSsl = MAIL_ENABLE_SSL
            Try
                smtp.Send(correo)
            Catch ex As Exception
                Return ex.Message
                wmensaje = ex.Message
            End Try
            werrorval = Agrega_MAILSENT.Graba(5, receptor, wregistro, wmensaje, sSubject)
        Else

            'correo.From = New System.Net.Mail.MailAddress("noreply@jwksoluciones.com.ar")
            correo.From = New System.Net.Mail.MailAddress("intdigital@inteatro.gob.ar")
            Dim smtph As New System.Net.Mail.SmtpClient
            smtph.Host = "mail.jwksoluciones.com.ar"
            smtph.Port = 587
            smtp.EnableSsl = True
            smtph.Credentials = New System.Net.NetworkCredential("noreply@jwksoluciones.com.ar", "ThEhnL4ekDSx")
            Try
                smtph.Send(correo)
            Catch ex As Exception
                Return ex.Message
                wmensaje = ex.Message
            End Try
            werrorval = Agrega_MAILSENT.Graba(5, receptor, wregistro, wmensaje, sSubject)
        End If

        'Mail de Control

        Dim correoc As New System.Net.Mail.MailMessage
        Dim smtpc As New System.Net.Mail.SmtpClient

        correoc = New System.Net.Mail.MailMessage()
        correoc.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")

        correoc.From = New System.Net.Mail.MailAddress(MAIL_SENDER)
        receptor = MAIL_CONTROL
        correoc.To.Add(receptor)

        correoc.Subject = sSubject

        'Body
        correoc.Body = "<html><head></head><body>"
        'correoc.Body += "<U><B><B /><U /><BR /><BR />"
        'correoc.Body += "<B><B /><BR /><BR />"
        correoc.Body += "<BR /><BR />"
        correoc.Body += "<HR />"
        correoc.Body += "<BR />"

        correoc.Body += sBody

        correoc.Body += "</body></html>"

        correoc.IsBodyHtml = True
        correoc.Priority = System.Net.Mail.MailPriority.Normal
        'End of Body

        smtpc.Host = MAIL_SMTP_SERVER

        smtpc.Credentials = New System.Net.NetworkCredential(MAIL_CREDENTIALS_USER, MAIL_CREDENTIALS_PASSWORD)
        smtpc.EnableSsl = MAIL_ENABLE_SSL
        Try
            smtpc.Send(correoc)
            Return "OK"
        Catch ex As Exception
            Return "Falló envío a " & MAIL_CONTROL
        End Try

    End Function

    Public Function GetTextoAviso(ByVal nTipoMensaje As Integer, _
                                  Optional ByVal sRegistro As String = "", _
                                  Optional ByVal sIdRegistro As String = "", _
                                  Optional ByVal sNombreRegistro As String = "", _
                                  Optional ByVal sNombreIntegrante As String = "", _
                                  Optional ByVal sCUIL As String = "") As String
        Dim s As String

        s = ""

        If nTipoMensaje = MAIL_ALTA_INDIV_FIS Then
            s = "Se ha procesado satisfactoriamente su trámite de Alta Individual en el Registro Nacional del Teatro " &
            "Independiente. Debe clickear en el link que figura al final de este mensaje, con el fin de ACTIVAR SU  " &
            "CUENTA DE USUARIO. Al hacerlo, se le abrirá en el navegador de internet, una página de confirmación " &
            "de activación, desde la cual podrá iniciar sesión por primera vez en la plataforma de INTeatroDigital,  " &
            "ingresando su CUIL y su Contraseña."

        ElseIf nTipoMensaje = MAIL_ALTA_INDIV_JUR Then
            s = "Se ha procesado satisfactoriamente su trámite de Alta Individual en el Registro Nacional del Teatro " &
            "Independiente. Debe clickear en el link que figura al final de este mensaje, con el fin de ACTIVAR SU  " &
            "CUENTA DE USUARIO. Al hacerlo, se le abrirá en el navegador de internet, una página de confirmación " &
            "de activación, desde la cual podrá iniciar sesión por primera vez en la plataforma de INTeatroDigital,  " &
            "ingresando su CUIT y su Contraseña."

        ElseIf nTipoMensaje = CONF_ALTA_INDIV_FIS Or nTipoMensaje = CONF_ALTA_INDIV_JUR Then
            s = "Se ha procesado satisfactoriamente su trámite de Alta Individual en INTeatro Digital. A partir de este momento usted podrá ser responsable o integrante de los distintos tipos de registro del Registro Nacional del Teatro Independiente."

        ElseIf nTipoMensaje = MAIL_MODIF_INDIV_FIS Then
            s = "Usted ha realizado el trámite de Actualización de Datos de su Alta Individual en INTeatroDigital. " &
                "Estamos trabajando en el procesamiento de sus datos. Debe clickear en el siguiente link con el fin " &
                "de validar su identidad como usuario. En breve recibirá en esta dirección de correo electrónico la  " &
                "confirmación definitiva del trámite."

        ElseIf nTipoMensaje = MAIL_MODIF_INDIV_JUR Then
            s = "Usted ha realizado el trámite de Actualización de Datos de su Alta Individual en INTeatroDigital. " &
                "Estamos trabajando en el procesamiento de sus datos. Debe clickear en el siguiente link con el fin " &
                "de validar su identidad como usuario. En breve recibirá en esta dirección de correo electrónico la  " &
                "confirmación definitiva del trámite."

        ElseIf nTipoMensaje = CONF_MODIF_INDIV_FIS Or nTipoMensaje = CONF_MODIF_INDIV_JUR Then
            s = "Se ha procesado satisfactoriamente su trámite de Actualización de Alta Individual en INTeatro Digital."


        ElseIf nTipoMensaje = MAIL_ALTA_REGISTRO Then
            s = "Usted ha realizado el trámite de Registro de " & sRegistro & " en INTeatro Digital. Estamos trabajando en el procesamiento de sus datos. "
            s += "Debe clickear en el link que figura al final de este mensaje, con el fin de validar su identidad como usuario. "
            s += "Al hacerlo, se le abrirá en el navegador de internet, la plataforma de INTeatro Digital directamente en la página del 'Menú de Impresión', desde la cual deberá imprimir por duplicado el archivo PDF conteniendo la 'Planilla de Confirmación de Datos', firmar en original ambos ejemplares tanto usted como todos los integrantes vinculados y enviar a la Representación del I.N.T.correspondiente a su Provincia junto con la siguiente documentación: En el caso que el responsable sea una persona física: adjuntar copia debidamente certificada de la primera y segunda hoja del DNI; en el caso que el responsable sea una persona jurídica: adjuntar copia debidamente certificada del Acta Constitutiva, del Estatuto, de la inscripción de Personería Jurídica, del Acta de Designación de Autoridades vigente y de la 1º y 2º hoja del DNI de los integrantes de la Comisión Directiva (Presidente, Vicepresidente, Secretario y Tesorero). "
            s += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá en esta dirección de correo electrónico la confirmación definitiva del trámite."

        ElseIf nTipoMensaje = CONF_ALTA_REGISTRO Then
            s = "Se ha procesado satisfactoriamente su trámite de Registro de " & sRegistro & " en INTeatro Digital."
            's = "Se ha procesado satisfactoriamente su trámite de Registro de " & sRegistro & " en INTeatro Digital y se le ha asignado el siguiente Nº de Registro: " & sIdRegistro & ".-"
            's += "A partir de este momento, podrá imprimir su constancia desde el ‘Menú de Impresión’ de la plataforma INTeatro Digital."


        ElseIf nTipoMensaje = MAIL_MODIF_REGISTRO Then
            s = "Usted ha realizado el trámite de Actualización de Registro de " & sRegistro & " en INTeatro Digital. Estamos trabajando en el procesamiento de sus datos. "
            s += "Debe clickear en el link que figura al final de este mensaje, con el fin de validar su identidad como usuario. "
            s += "Al hacerlo, se le abrirá en el navegador de internet, la plataforma de INTeatro Digital directamente en la página del 'Menú de Impresión', desde la cual deberá imprimir por duplicado el archivo PDF conteniendo la 'Planilla de Confirmación de Datos', firmar en original ambos ejemplares tanto usted como todos los integrantes vinculados y enviar a la Representación del I.N.T.correspondiente a su Provincia. "
            s += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá en esta dirección de correo electrónico la confirmación definitiva del trámite."

        ElseIf nTipoMensaje = CONF_MODIF_REGISTRO Then
            s = "Se ha procesado satisfactoriamente su trámite de Actualización de Registro de " & sRegistro & " en INTeatro Digital A partir de este momento, podrá imprimir su nueva constancia desde el ‘Menú de Impresión’ de la plataforma INTeatro Digital."

        ElseIf nTipoMensaje = MAIL_BAJA_INTEGRANTE_A_INTEGRANTE Or nTipoMensaje = MAIL_BAJA_INTEGRANTE_A_PROVINCIA Then
            s = "Usted ha sido desvinculado como integrante del / de la " & sRegistro & " " & sNombreRegistro & " en el Registro Nacional del Teatro Independiente. "
            s += "A partir de este momento deja de tener vinculación con el Registro Nº " & sIdRegistro


        ElseIf nTipoMensaje = MAIL_BAJA_INTEGRANTE_A_RESPONSABLE Then
            s = "El/la integrante " & sNombreIntegrante & " se ha desvinculado como integrante del Registro " & sRegistro & " " & sNombreRegistro & ". "
            s += "Usted deberá realizar a la brevedad el trámite de Actualización de Registro en la plataforma INTeatro Digital para poder realizar cualquier otro trámite ante el INT."

        ElseIf nTipoMensaje = MAIL_MODIF_INTEGRANTE_A_RESPONSABLE_FIS Or nTipoMensaje = MAIL_MODIF_INTEGRANTE_A_RESPONSABLE_JUR Then
            s = "El/la integrante " & sNombreIntegrante & " Nº de CUIL " & sCUIL & " ha actualizado datos personales en la plataforma INTeatro Digital. "
            s += "Debe clickear en el link que figura al final de este mensaje; al hacerlo, se le abrirá en el navegador de internet, "
            s += "la plataforma de INTeatro Digital directamente en la página del 'Menú de Impresión', desde la cual deberá imprimir por "
            s += "duplicado el archivo PDF conteniendo la 'Planilla de Confirmación de Datos', firmar en original ambos ejemplares tanto "
            s += "usted como todos los integrantes vinculados y enviar a la Representación del I.N.T.correspondiente a su Provincia. "
            s += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá en esta dirección de correo electrónico la confirmación definitiva del trámite."

        ElseIf nTipoMensaje = CONF_MODIF_INTEGRANTE_A_RESPONSABLE_FIS Or nTipoMensaje = CONF_MODIF_INTEGRANTE_A_RESPONSABLE_JUR Then
            s = "Se ha procesado satisfactoriamente la actualización en INTeatro Digital. A partir de este momento, usted podrá imprimir su nueva constancia desde el ‘Menú de Impresión’ de la plataforma INTeatro Digital."

        ElseIf nTipoMensaje = MAIL_DESVINCULACION_INTEGRANTE_A_INTEGRANTE Or nTipoMensaje = MAIL_DESVINCULACION_INTEGRANTE_A_PROVINCIA Then
            s = "Ud ha sido desvinculado como integrante del {0} en el Registro Nacional del Teatro Independiente. A partir de este momento deja de tener vinculación con el Registro N° {1}"

        ElseIf nTipoMensaje = MAIL_DESVINCULACION_INTEGRANTE_A_RESPONSABLE Then
            s = "El/la integrante {0} se ha desvinculado como integrante del Registro {1}. Debe clickear en el link que figura al final de este mensaje; al hacerlo se le abrirá en el navegador de internet, la plataforma de INTeatro Digital, en la cual deberá iniciar sesión y posteriormente, en la sección 'Imprimir Planillas y Constancias' deberá imprimir por duplicado el archivo PDF conteniendo la 'Planilla de Confirmación de Datos', firmar en original ambos ejemplares tanto Ud. como todos los integrantes vinculados y enviar por correo a la Representación del I.N.T. correspondiente a su Provincia"
            s += "<br/>Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá en esta dirección de correo electrónico la confirmación definitiva del trámite"
        End If

        Return s

    End Function

    Public Function GetLink(ByVal nTipoMensaje As Integer, _
                            ByVal nId As Integer, _
                            Optional ByVal nRegisDig As Integer = 0, _
                            Optional ByVal nIdIntegrante As Integer = 0) As String

        Dim sURL As String
        Dim sRedirect As String
        Dim s As String

        sURL = ""
        sRedirect = ""

        'Parámetros
        't = Tipo de registración, p = persona física, j = persona jurídica, r = registro
        'o = Operación, a = alta, m = modificación, k = modificación campos clave
        'u = Id de la persona física / jurídica o registro

        If nTipoMensaje = MAIL_ALTA_INDIV_FIS Then
            sRedirect = "menuFinal.aspx?t=p&o=a&u=" & nId.ToString

        ElseIf nTipoMensaje = MAIL_MODIF_INDIV_FIS Then
            sRedirect = "ConfActuIndiv.aspx?u=" & nId.ToString & "&t=p&o=m"

        ElseIf nTipoMensaje = MAIL_ALTA_INDIV_JUR Then
            sRedirect = "menuFinal.aspx?t=j&o=a&u=" & nId.ToString

        ElseIf nTipoMensaje = MAIL_MODIF_INDIV_JUR Then
            sRedirect = "ConfActuIndiv.aspx?t=j&o=m&u=" & nId.ToString


        ElseIf nTipoMensaje = MAIL_ALTA_REGISTRO Then
            sRedirect = "confirmarRegistracion.aspx?t=r&o=a&u=" & nId.ToString & "&r=" & nRegisDig.ToString

        ElseIf nTipoMensaje = MAIL_MODIF_REGISTRO Then
            'sRedirect = "confirmarRegistracion.aspx?t=r&o=m&u=" & nId.ToString & "&r=" & nRegisDig.ToString
            sRedirect = "confirmarRegistracion.aspx?t=r&o=m&u=" & nId.ToString & "&r=" & nRegisDig.ToString

        ElseIf nTipoMensaje = MAIL_MODIF_INTEGRANTE_A_RESPONSABLE_FIS Then
            sRedirect = "confirmarRegistracion.aspx?t=p&o=k&u=" & nId.ToString & "&i=" & nIdIntegrante

        ElseIf nTipoMensaje = MAIL_MODIF_INTEGRANTE_A_RESPONSABLE_JUR Then
            sRedirect = "confirmarRegistracion.aspx?t=j&o=k&u=" & nId.ToString & "&i=" & nIdIntegrante

        ElseIf nTipoMensaje = MAIL_DESVINCULACION_INTEGRANTE_A_RESPONSABLE Then
            sRedirect = "registroImpresion.aspx"

        End If

        sURL = MAIL_WEB_SERVER & MAIL_LOGIN_PAGE & "?ReturnURL="
        sRedirect = System.Web.HttpUtility.UrlEncode(sRedirect)
        sURL += sRedirect

        s = "<a href='" & sURL & "'>Click para confirmar</a><br /><br />"
        s += sURL

        Return s

    End Function

    Public Function GetLinkImpresion(ByVal nId As Integer) As String

        Dim sURL As String
        Dim sRedirect As String

        sURL = ""
        sRedirect = ""

        sURL = "Si desea imprimir los formularios una vez confirmada la registración, hacer click sobre el siguiente vínculo:" & "<br />"
        sRedirect = MAIL_WEB_SERVER & "reportRegistro.aspx?accion=P&codigo=" & nId.ToString

        sURL = MAIL_WEB_SERVER & MAIL_LOGIN_PAGE & "?redirect="
        sRedirect = System.Web.HttpUtility.UrlEncode(sRedirect)
        sURL += sRedirect

        Return sURL

    End Function

    Function GetMailTo(ByVal sId As String, _
                       ByVal nTipo As Integer) As String

        Dim sSQLCmd As String = ""
        Dim sMailTo As String
        sMailTo = ""

        If nTipo = TIPO_PERSONA Then
            sSQLCmd = "SELECT email " &
                                "FROM Regisdig " &
                                "WHERE codigo = " & sId
        ElseIf nTipo = TIPO_REGISTRO Then
            sSQLCmd = "SELECT email " &
                                "FROM Registro " &
                                "WHERE codigo = " & sId
        End If
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sSQLCmd, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            sMailTo = dr.GetString(0).Trim
        End While
        dr.Close()
        cn.Close()
        Return sMailTo

    End Function

    Public Function GetMailBody(ByVal nIdCodigoRegistro As Integer) As String
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim sBody As String = ""

        'Load Registro
        sSQLCmd = "select sector, convert(varchar(17), fechAlta,113) from registro where codigo = " & nIdCodigoRegistro

        MyConnection = New SqlConnection()
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
        MyConnection.Open()

        MyCommand = New SqlCommand(sSQLCmd, MyConnection)
        MyReader = MyCommand.ExecuteReader()

        If MyReader.Read() Then
            sBody = "Tipo: " & Funciones.GetNombreSector(MyReader.Item(0).ToString.Trim) & "<br />"
            sBody += "Fecha: " & MyReader.Item(1).ToString.Trim & "<br />"
        End If

        MyReader.Dispose()
        MyCommand.Dispose()
        MyConnection.Dispose()

        Return sBody

    End Function

    Public Function SendMailContacto(ByVal sNombre As String, _
                                        ByVal sApellido As String, _
                                        ByVal sMailAddress As String, _
                                        ByVal sSubject As String, _
                                        ByVal sBody As String) As String

        Dim correo As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient
        Dim Receptor As String

        correo = New System.Net.Mail.MailMessage()
        correo.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")

        correo.From = New System.Net.Mail.MailAddress(MAIL_SENDER_FORM)
        Receptor = MAIL_SENDER_FORM
        correo.To.Add(Receptor)

        correo.Subject = sSubject

        'Body
        correo.Body = "<html><head></head><body>"
        correo.Body += "<U><B><B /><U /><BR /><BR />"
        correo.Body += "Fecha " & Format(Today(), "dd/MM/yyyy") & "<BR />"
        correo.Body += "<HR />"
        correo.Body += "<BR />"

        correo.Body += "La persona " & sNombre & " " & sApellido & " " & "<br />"
        correo.Body += "con cuenta de correo electrónico " & sMailAddress & " " & "<br />"
        correo.Body += "ha completado el formulario de contacto: " & "<br />"
        correo.Body += sBody

        correo.Body += "<br/>"
        correo.Body += "<a href=""mailto:" & sMailAddress & "?subject=Re:" & sSubject & "&body=" & sBody & """>Responder</a>"
        correo.Body += "<br/>"


        correo.Body += "</body></html>"

        correo.IsBodyHtml = True
        correo.Priority = System.Net.Mail.MailPriority.Normal
        'End of Body

        smtp.Host = MAIL_SMTP_SERVER
        smtp.Credentials = New System.Net.NetworkCredential(MAIL_CREDENTIALS_USER_FORM, MAIL_CREDENTIALS_PASSWORD_FORM)
        smtp.EnableSsl = MAIL_ENABLE_SSL
        Try
            smtp.Send(correo)
            Return "OK"
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    'Public Function SendMail(ByVal sMailTo As String, _
    '                         ByVal sBody As String, _
    '                         ByVal sIdRegistro As String) As String

    '    Dim correo As New System.Net.Mail.MailMessage
    '    Dim smtp As New System.Net.Mail.SmtpClient
    '    Dim Receptor As String

    '    correo = New System.Net.Mail.MailMessage()

    '    correo.From = New System.Net.Mail.MailAddress(MAIL_SENDER)
    '    Receptor = sMailTo
    '    correo.To.Add(Receptor)

    '    'correo.Subject = "Solicitud de Inscripción N º " & wcodi

    '    'Body
    '    correo.Body = "<html><head></head><body>"
    '    correo.Body += "<U><B><B /><U /><BR /><BR />"
    '    correo.Body += "Fecha " & Format(Today(), "dd/mm/yyyy") & "<BR />"
    '    correo.Body += "<HR />"
    '    correo.Body += "<BR />"

    '    correo.Body += "Se han recepcionado los siguientes datos: " & "<br />"
    '    correo.Body += sBody

    '    'correo.Body += "http://www.google.com/search?hl=en&source=hp&biw=1163&bih=726&q=marcelo+zanolin&aq=f&aqi=g10&aql=f&oq=" & "<BR /><BR />"
    '    correo.Body += MAIL_WEB_SERVER & "confirmarRegistracion.aspx?u=" & sIdRegistro & "<BR /><BR />"

    '    correo.Body += "Si desea imprimir los formularios una vez confirmada la registración, hacer click sobre el siguiente vínculo:" & "<br />"
    '    correo.Body += MAIL_WEB_SERVER & "reportRegistro.aspx?accion=P&codigo=" & sIdRegistro & "<BR /><BR />"

    '    correo.Body += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
    '    correo.Body += "que ve mas arriba en su navegador de internet.<br />"
    '    correo.Body += "</body></html>"

    '    correo.IsBodyHtml = True
    '    correo.Priority = System.Net.Mail.MailPriority.Normal
    '    'End of Body

    '    smtp.Host = MAIL_SMTP_SERVER
    '    smtp.Credentials = New System.Net.NetworkCredential(MAIL_CREDENTIALS_USER, MAIL_CREDENTIALS_PASSWORD)
    '    smtp.EnableSsl = True
    '    Try
    '        smtp.Send(correo)
    '        Return "OK"
    '    Catch ex As Exception
    '        Return ex.Message
    '    End Try

    'End Function

End Module