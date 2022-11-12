Imports System.IO
Imports System.Data.OleDb

Partial Public Class contacto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usuario") Is Nothing Then
            Dim MisDatos As HtmlAnchor = Nothing
            MisDatos = CType(Master.FindControl("lkMisDatos"), HtmlAnchor)
            MisDatos.Visible = False
        End If
    End Sub

    Private Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim sResult As String = ""
        txtErrorBody.Text = ""
        LabelErrorAdjunto.Text = ""
        Try
            If txtBody.Text.Length > 1000 Then
                txtErrorBody.Text = "Máximo 1000 caracteres"
                Return
            End If
            Dim sNombre As String = txtNombre.Text.Trim
            Dim sApellido As String = txtApellido.Text.Trim
            Dim sMailAddress As String = txtFrom.Text.Trim
            Dim sSubject As String = "*TEST* " + txtSubject.Text.Trim
            Dim sBody As String = txtBody.Text.Trim

            If Len(RTrim(sNombre)) = 0 Then
                txtErrorBody.Text = "Ingrese su Nombre"
                Return
            End If
            If Len(RTrim(sApellido)) = 0 Then
                txtErrorBody.Text = "Ingrese su Apellido"
                Return
            End If
            If Len(RTrim(sMailAddress)) = 0 Then
                txtErrorBody.Text = "Ingrese su Mail"
                Return
            End If
            If Len(RTrim(sSubject)) = 0 Then
                txtErrorBody.Text = "Ingrese el Asunto del Mail"
                Return
            End If

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

            Dim woperador As String = "SendMail"
            If UploadImporta.HasFile Then
                Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                If Extension <> ".doc" And Extension <> ".docx" And Extension <> ".jpg" And Extension <> ".jepg" And Extension <> ".pdf" Then
                    LabelErrorAdjunto.Text = "Formato de Archivo Incorrecto"
                    Return
                End If
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                Dim wfile As String = "C:\Web\" + RTrim(woperador) + "\" + wdir
                Try
                    MkDir(wfile)
                Catch ex As Exception
                    Return
                End Try
                Dim FilePath As String = wfile + "\" + FileName
                UploadImporta.SaveAs(FilePath)
                Dim attach As System.Net.Mail.Attachment
                attach = New System.Net.Mail.Attachment(FilePath)
                correo.Attachments.Add(attach)
            End If
            Dim wmensaje As String = ""
            smtp.Host = MAIL_SMTP_SERVER
            smtp.Credentials = New System.Net.NetworkCredential(MAIL_CREDENTIALS_USER_FORM, MAIL_CREDENTIALS_PASSWORD_FORM)
            smtp.EnableSsl = MAIL_ENABLE_SSL
            Try
                smtp.Send(correo)
            Catch ex As Exception
                Dim mensaje As String = ex.Message
            End Try
            If wmensaje = "" Then
                Response.Redirect("confirmaMail.aspx?m=" & "Este mensaje lo recepcionará el sector REGISTRO del INT y será respondido POR MAIL a la brevedad. Tenga a bien chequear con frecuencia la cuenta de correo que indicó en el formulario" & sResult)
            Else
                Response.Redirect("confirmaMail.aspx?m=" & "El mensaje no fue enviado " & sResult)
            End If

        Catch ex As Exception
            Dim mensaje As String = ex.Message
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If Not Session("usuario") Is Nothing Then
            Response.Redirect("menuFinal.aspx")
        Else
            Response.Redirect("index.aspx")
        End If
    End Sub

    Private Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnVolver.Click
        If Not Session("usuario") Is Nothing Then
            Response.Redirect("menuFinal.aspx")
        Else
            Response.Redirect("index.aspx")
        End If
    End Sub
End Class