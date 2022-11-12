Imports System.Data.SqlClient
Public Class dsInte

    Shared Sub CargaIntegrantes(ByRef ds As dsIntegrantes, ByVal codigo As Integer)
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        'Sectores / Grupos
        ' Dim rb As New dsIntegrantes
        ds = New dsIntegrantes
        cn.Open()
        Dim sql As String = "select a.idIntegrante, a.codigoRegistro, a.cuil, rtrim(ltrim(b.apellido))+ ', ' + rtrim(ltrim(b.nombre)) apellidoynombre from Integrantes a left join regisdig b on a.cuil = b.cuil where a.CodigoRegistro = " & codigo.ToString & " and a.fechaBaja is null"
        Dim cmd As New SqlCommand(sql, cn)
        Dim rd As SqlDataReader
        rd = cmd.ExecuteReader
        While rd.Read
            Dim row As DataRow
            row = ds.Integrantes.NewRow
            row("idIntegrante") = rd("idIntegrante")
            row("codigoRegistro") = rd("codigoRegistro")
            row("CUIL") = rd("CUIL")
            row("apellidoynombre") = rd("apellidoynombre")
            ds.Integrantes.AddIntegrantesRow(row)
        End While
        cn.Close()
    End Sub
    Shared Function YaExiste(ByVal ds As dsIntegrantes, ByVal cuil As String) As Boolean
        Try
            Dim cuentas = From c In ds.Integrantes Where c.cuil = cuil Select c
            If cuentas Is Nothing Then
                Return False
            Else
                If cuentas.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Shared Sub DesvinculaIntegrante(ByRef ds As dsIntegrantes, ByVal codigo As Integer, ByVal id As String)
        Dim filtro As String
        Dim dr As DataRow
        Dim dx As DataRow

        filtro = "idIntegrante = " & id
        dr = ds.Integrantes.Select(filtro)(0)
        If Convert.ToInt32(id) > 0 Then
            Dim row As DataRow
            row = ds.Borrados.NewRow
            row("idIntegrante") = dr("idIntegrante")
            row("codigoRegistro") = dr("codigoRegistro")
            row("CUIL") = dr("CUIL")
            row("apellidoynombre") = dr("apellidoynombre")
            ds.Borrados.AddBorradosRow(row)
        Else
            dx = ds.Agregados.Select(filtro)(0)
            ds.Agregados.RemoveAgregadosRow(dx)
        End If

        ds.Integrantes.RemoveIntegrantesRow(dr)
    End Sub

    Shared Sub AgregaIntegrantes(ByRef ds As dsIntegrantes, ByVal codigo As Integer, ByVal cuit As String)
        Dim dr As DataRow() = ds.Integrantes.Select("CUIL = '" & cuit & "'")
        If dr.Length = 0 Then
            Dim cn As New SqlClient.SqlConnection(SqlConex)
            cn.Open()
            Dim q As Integer = ds.Agregados.Rows.Count
            Dim cantidad As Integer = (q + 1) * -1
            Dim sql As String = "select rtrim(ltrim(apellido)) + ', ' + rtrim(ltrim(nombre)) apellidoynombre from regisdig where cuil = '" & cuit & "'"
            Dim cmd As New SqlCommand(sql, cn)
            Dim nombre As String = cmd.ExecuteScalar
            Dim row As DataRow
            row = ds.Agregados.NewRow
            row("idIntegrante") = cantidad
            row("codigoRegistro") = codigo
            row("CUIL") = cuit
            row("apellidoynombre") = nombre

            ds.Agregados.AddAgregadosRow(row)
            row = ds.Integrantes.NewRow
            row("idIntegrante") = cantidad
            row("codigoRegistro") = codigo
            row("CUIL") = cuit
            row("apellidoynombre") = nombre
            ds.Integrantes.AddIntegrantesRow(row)
            cn.Close()
        End If


    End Sub

    Shared Sub AceptaCambios(ByRef ds As dsIntegrantes, ByVal registro As Integer)
        If ds.Agregados.Rows.Count > 0 Then
            For Each row As DataRow In ds.Agregados.Rows
                insertaIntegrantes(registro, row("CUIL"))
            Next
        End If
        If ds.Borrados.Rows.Count > 0 Then
            For Each row As DataRow In ds.Borrados.Rows
                Desvincular(row("idIntegrante"))
            Next
        End If
    End Sub

    Shared Sub insertaIntegrantes(ByVal codigo As Integer, ByVal cuit As String)
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim cmd As New SqlCommand("nuevoIngresante", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim par0 As New SqlParameter("@codigo", DbType.Int32)
        cmd.Parameters.Add(par0)
        cmd.Parameters.Add(New SqlParameter("@cuit", DbType.String))
        cmd.Parameters(0).Value = codigo
        cmd.Parameters(1).Value = cuit
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Dim mensaje As String = ex.Message
        End Try
        cn.Close()
    End Sub

    Shared Sub Desvincular(ByVal id As Integer)
        Dim user As usuario = HttpContext.Current.Session("usuario")
        Dim operador As String = user.Codigo.ToString
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim cmd As New SqlCommand("desvinculaIntegrante_operador", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim par0 As New SqlParameter("@id", DbType.Int32)
        Dim par1 As New SqlParameter("@operador", DbType.String)
        cmd.Parameters.Add(par0)
        cmd.Parameters.Add(par1)
        cmd.Parameters(0).Value = id
        cmd.Parameters(1).Value = operador
        cmd.ExecuteNonQuery()
        cn.Close()
    End Sub

End Class
