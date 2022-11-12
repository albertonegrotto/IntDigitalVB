Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class confirmarRegistracion
    Inherits System.Web.UI.Page
    Dim quien As usuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sId As String
        Dim sTipoRegistracion As String
        Dim sTipoOperacion As String = ""
        Dim sIdIntegrante As String


        If Not Page.IsPostBack Then
            quien = CType(Session("usuario"), usuario)
            Session("codigo") = quien.Codigo
            Session("Cuit") = quien.Usuario
            Session("USER_ID") = quien.Codigo

            sTipoRegistracion = Request.QueryString("t").ToUpper
            If Not Request.QueryString("o") Is Nothing Then
                sTipoOperacion = Request.QueryString("o").ToUpper

            End If
            sId = Request.QueryString("u")
            sIdIntegrante = Request.QueryString("i")

            If sId = "" Then Return

            If sTipoRegistracion = "P" Then 'Persona Física
                lblTitulo.Text = "Confirmación Persona Física"
                lblRegistro.Text = "Persona Física"
            ElseIf sTipoRegistracion = "J" Then 'Persona Jurídica
                lblTitulo.Text = "Confirmación Persona Jurídica"
                lblRegistro.Text = "Persona Jurídica"
            Else 'Registro
                lblTitulo.Text = "Confirmación Registro"
                lblRegistro.Text = "Registro"
            End If

            If sTipoOperacion = "K" Then
                lblTitulo.Text = "Confirmación Integrante"
                lblRegistro.Text = "Integrante"
            End If

            If UpdateEstado(sId, sTipoRegistracion) Then

                'Envío mail de confirmación
                Dim sResult As String
                Dim sSubject As String
                Dim sBody As String

                sBody = ""

                If sTipoRegistracion = "P" Then

                    If sTipoOperacion = "A" Then
                        sSubject = "Confirmación de Inscripción de Persona Física Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_ALTA_INDIV_FIS) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(sId, TIPO_PERSONA), sSubject, sBody)

                    ElseIf sTipoOperacion = "M" Then
                        sSubject = "Confirmación de Modificación de Persona Física Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_MODIF_INDIV_FIS) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(sId, TIPO_PERSONA), sSubject, sBody)

                    ElseIf sTipoOperacion = "K" Then
                        sSubject = "Confirmación de Modificación de Integrante Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_MODIF_INTEGRANTE_A_RESPONSABLE_FIS) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(sIdIntegrante, TIPO_PERSONA), sSubject, sBody)

                    End If

                ElseIf sTipoRegistracion = "J" Then

                    If sTipoOperacion = "A" Then
                        sSubject = "Confirmación de Inscripción de Persona Jurídica Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_ALTA_INDIV_JUR) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(sId, TIPO_PERSONA), sSubject, sBody)

                    ElseIf sTipoOperacion = "M" Then
                        sSubject = "Confirmación de Modificación de Persona Jurídica Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_MODIF_INDIV_JUR) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(sId, TIPO_PERSONA), sSubject, sBody)

                    ElseIf sTipoOperacion = "K" Then
                        sSubject = "Confirmación de Modificación de Integrante Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_MODIF_INTEGRANTE_A_RESPONSABLE_JUR) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(sIdIntegrante, TIPO_PERSONA), sSubject, sBody)

                    End If

                ElseIf sTipoRegistracion = "R" Then

                    If sTipoOperacion = "A" Then
                        'sSubject = "Confirmación de Inscripción de Registro Nº " & sId
                        sSubject = "Confirmación de Inscripción Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_ALTA_REGISTRO, GetTipoRegistro(GetIdSector(sId))) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(Session("USER_ID"), TIPO_PERSONA), sSubject, sBody)
                        ViewState("ENVIAR_A_IMPRESION") = True

                    ElseIf sTipoOperacion = "M" Then
                        'sSubject = "Confirmación de Modificación de Registro Nº " & sId
                        sSubject = "Confirmación de Modificación Nº " & sId
                        sBody += Mail.GetTextoAviso(CONF_MODIF_REGISTRO, GetTipoRegistro(GetIdSector(sId))) & "<br />"
                        sBody += "<br />"
                        sResult = SendMail(Mail.GetMailTo(Session("USER_ID"), TIPO_PERSONA), sSubject, sBody)
                        ViewState("ENVIAR_A_IMPRESION") = True

                    End If

                End If

                lblResultado.Text = "Confirmado"
            Else
                lblResultado.Text = "No se pudo actualizar, por favor intente nuevamente"
            End If

        Else

        End If

    End Sub

    Private Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEnviar.Click
        Dim sId As String
        sId = Request.QueryString("u")
        If ViewState("ENVIAR_A_IMPRESION") Is Nothing Then
            Response.Clear()
            Response.Redirect("menuFinal.aspx", False)
        Else
            Response.Clear()
            Response.Redirect("reportRegistro.aspx?accion=p&codigo=" & sId.ToString, False)
        End If
    End Sub

    Private Function UpdateEstado(ByRef sId As String, _
                                  ByVal sTipoRegistracion As String) As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String

        Try
            If sTipoRegistracion.ToUpper = "P" Or sTipoRegistracion.ToUpper = "J" Then  'Persona Física o Jurídica
                sSQLCmd = "UPDATE RegisDig " & _
                               "SET confirmado = 1 " & _
                             "WHERE codigo = " & sId
            ElseIf sTipoRegistracion.ToUpper = "R" Then  'Registro
                sSQLCmd = "UPDATE Registro " & _
                               "SET confirmado = 1 " & _
                             "WHERE codigo = " & sId
            End If
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()
            MyConnection.Dispose()

        Catch ex As Exception
            UpdateEstado = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        UpdateEstado = True

    End Function

End Class