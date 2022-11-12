Public Partial Class Desvincular
    Inherits System.Web.UI.Page
    Dim quien As usuario
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            quien = CType(Session("usuario"), usuario)
            HiddenField1.Value = quien.Codigo
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim id As Integer
        id = e.Keys(0)
        Dim desvinculante As New desvinculado(id)
        Dim CodigoRegistro As Decimal = 0
        Dim Sql As String = "select REGISTRO from REGISTRO where CODIGO=" & desvinculante.IDRegistro
        cn.Open()
        Dim Psqlr As New SqlClient.SqlCommand(Sql, cn)
        Dim drr As SqlClient.SqlDataReader = Psqlr.ExecuteReader
        While drr.Read()
            Try
                CodigoRegistro = drr.GetDecimal(0)
            Catch ex As Exception
                CodigoRegistro = 0
            End Try
        End While
        drr.Close()
        cn.Close()
        Session("Codigoregistro") = CodigoRegistro.ToString
        EnviarCorreoAlIntegrante(desvinculante)
        EnviarCorreoAProvincia(desvinculante)
        EnviarCorreoAlResponsable(desvinculante)
        ' e.Cancel = True
    End Sub

    Private Sub EnviarCorreoAlIntegrante(ByVal d As desvinculado)
        Dim CodigoRegistro As String = Session("Codigoregistro")
        Dim sResult As String
        Dim sSubject As String
        Dim sBody As String
        sSubject = "INTeatroDigital - Desvinculación como Integrante "
        sBody = "Estimado usuario de INTeatroDigital:" & "<br />"
        sBody += "Usted ha sido desvinculado como integrante de " & d.NombreRegistro & " en el Registro Nacional del Teatro Independiente. " & "<br />"
        sBody += "A partir de este momento deja de tener vinculación con el Registro N° " & CodigoRegistro & "." & "<br />"
        sBody += "<br />"
        sBody += "Gracias.<br />"
        sBody += "<br />"
        sBody += "INTeatroDigital.<br />"
        sResult = SendMail(d.correoIntegrante, sSubject, sBody)
    End Sub

    Private Sub EnviarCorreoAProvincia(ByVal d As desvinculado)
        Dim CodigoRegistro As String = Session("Codigoregistro")
        Dim sResult As String
        Dim sSubject As String
        Dim sBody As String
        sSubject = "INTeatroDigital - Desvinculación de un Integrante"
        sBody = String.Format(Mail.GetTextoAviso(MAIL_DESVINCULACION_INTEGRANTE_A_PROVINCIA), d.NombreRegistro, d.IDRegistro.ToString())
        sBody += "<br />"
        sBody = "REGISTRO de:" & d.TipoRegistro & " - " & d.NombreRegistro & "<br />"
        sBody += "N° DE REGISTRO:" & CodigoRegistro & "<br />"
        sBody += "Se ha confirmado la desvinculación al mencionado registro de " & RTrim(d.Nombre) & " " & RTrim(d.Apellido) & " - " & d.CUIL
        sResult = SendMail(d.correoProvincia, sSubject, sBody)
    End Sub

    Private Sub EnviarCorreoAlResponsable(ByVal d As desvinculado)
        Dim CodigoRegistro As String = Session("Codigoregistro")
        Dim sResult As String
        Dim sSubject As String
        Dim sBody As String
        sSubject = "INTeatroDigital - Desvinculación de un Integrante"
        sBody = "Estimado usuario de INTeatroDigital:" & "<br />"
        sBody += "Se ha procesado satisfactoriamente la desvinculación de  " & RTrim(d.Nombre) & " " & RTrim(d.Apellido) & " - " & d.CUIL & " de su Registro Nº " & CodigoRegistro & " - " & d.NombreRegistro & "<br />"
        sBody += "Debe clickear en el link que figura al final de este mensaje; al hacerlo, se le abrirá en el navegador de internet, la plataforma de INTeatroDigital, " & "<br />"
        sBody += "en la cual deberá iniciar sesión y posteriormente clickear en la sección 'Imprimir Constancias', desde la cual deberá emitir y  " & "<br />"
        sBody += "descargar la constancia de registro y enviarla por correo electrónico a la Representación del INT correspondiente a su Provincia." & "<br />"
        sBody += "<br />"
        sBody += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá " & "<br />"
        sBody += "en esta dirección de correo electrónico la confirmación definitiva del trámite de desvinculación de integrante." & "<br />"
        sBody += "<br />"
        sBody += Mail.GetLink(MAIL_DESVINCULACION_INTEGRANTE_A_RESPONSABLE, 0) & "<br />"
        sBody += "<br />"
        sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
        sBody += "que ve mas arriba en su navegador de internet.<br />"
        sBody += "<br />"
        sBody += "Gracias.<br />"
        sBody += "<br />"
        sBody += "INTeatroDigital.<br />"
        sResult = SendMail(d.correoResponsable, sSubject, sBody)
    End Sub

End Class