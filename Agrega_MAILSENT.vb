Public Class Agrega_MAILSENT
    Public Shared Function Graba(ByVal WTIPO As Integer, ByVal WMAIL As String, ByVal WREGISTRO As Decimal, ByVal WERROR As String, ByVal WMOTIVO As String)
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        Dim sql As String = ""
        Dim werrorval As Integer = 0
        sql = "Execute insert_mailsent @WTIPO,@WMAIL,@WREGISTRO,@WERROR,@WMOTIVO"
        Dim cmd As New SqlClient.SqlCommand(sql, cn)
        cmd.Parameters.AddWithValue("@WTIPO", WTIPO)
        cmd.Parameters.AddWithValue("@WMAIL", WMAIL)
        cmd.Parameters.AddWithValue("@WREGISTRO", WREGISTRO)
        cmd.Parameters.AddWithValue("@WERROR", WERROR)
        cmd.Parameters.AddWithValue("@WMOTIVO", WMOTIVO)
        cn.Open()
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            werrorval = 1
        End Try
        cn.Close()
        Return werrorval
    End Function

End Class
