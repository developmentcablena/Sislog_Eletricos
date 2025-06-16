Imports System.Data
Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub executar()
        Dim dt As DataTable = ObterDadosBanco()
        Debug.WriteLine("exewcutar")
        gvRelatorio.Columns.Clear()

        ' Criar colunas dinamicamente com base nas colunas do DataTable

        Dim campo1 As New BoundField()
        campo1.DataField = "Transportadora"
        campo1.HeaderText = "Transportadora"
        gvRelatorio.Columns.Add(campo1)

        Dim campo6 As New BoundField()
        campo6.DataField = "TempoPadrao"
        campo6.HeaderText = "Tempo Padão"
        gvRelatorio.Columns.Add(campo6)

        Dim campo2 As New BoundField()
        campo2.DataField = "DataCadastro"
        campo2.HeaderText = "Data Cadastro"
        campo2.DataFormatString = "{0:dd/MM/yyyy}"
        campo2.HtmlEncode = False
        gvRelatorio.Columns.Add(campo2)

        Dim campo3 As New BoundField()
        campo3.DataField = "HorarioEntrada"
        campo3.HeaderText = "Horario Entrada"
        campo3.DataFormatString = "{0:HH:mm}"
        gvRelatorio.Columns.Add(campo3)

        Dim campo4 As New BoundField()
        campo4.DataField = "HorarioSaida"
        campo4.HeaderText = "Horario Saída"
        campo4.DataFormatString = "{0:HH:mm}"
        gvRelatorio.Columns.Add(campo4)

        Dim campo5 As New BoundField()
        campo5.DataField = "TempoPermanencia"
        campo5.HeaderText = "Tempo de Permanencia"
        gvRelatorio.Columns.Add(campo5)

        Dim campo8 As New BoundField()
        campo8.DataField = ""
        campo8.HeaderText = "Status"
        gvRelatorio.Columns.Add(campo8)

        Dim campo9 As New BoundField()
        campo9.DataField = "Observacao"
        campo9.HeaderText = "Observação"
        gvRelatorio.Columns.Add(campo9)


        dt.Columns.Add("TempoPermanencia", GetType(String))

        For Each row As DataRow In dt.Rows
            Dim entradaStr As String = row("HorarioEntrada").ToString()
            Dim saidaStr As String = row("HorarioSaida").ToString()

            If Not String.IsNullOrEmpty(entradaStr) AndAlso Not String.IsNullOrEmpty(saidaStr) Then
                Dim entrada As DateTime
                Dim saida As DateTime

                If DateTime.TryParse(entradaStr, entrada) AndAlso DateTime.TryParse(saidaStr, saida) Then
                    Dim permanencia As TimeSpan = saida - entrada

                    row("TempoPermanencia") = permanencia

                Else
                    row("TempoPermanencia") = "-"
                End If
            Else
                row("TempoPermanencia") = "-"
            End If
        Next



        ' Fazer o bind dos dados no GridView
        gvRelatorio.DataSource = dt
        gvRelatorio.DataBind()
    End Sub

    Public Sub executar2()
        Dim dt As DataTable = ObterDadosBanco2()
        Debug.WriteLine("exewcutar")
        gvRelatorio.Columns.Clear()

        ' Criar colunas dinamicamente com base nas colunas do DataTable

        Dim campo1 As New BoundField()
        campo1.DataField = "Transportadora"
        campo1.HeaderText = "Transportadora"
        gvRelatorio.Columns.Add(campo1)

        Dim campo2 As New BoundField()
        campo2.DataField = "DataCadastro"
        campo2.HeaderText = "Data Cadastro"
        campo2.DataFormatString = "{0:dd/MM/yyyy}"
        campo2.HtmlEncode = False
        gvRelatorio.Columns.Add(campo2)

        Dim campo3 As New BoundField()
        campo3.DataField = "HorarioEntrada"
        campo3.HeaderText = "Horario Entrada"
        campo3.DataFormatString = "{0:HH:mm}"
        gvRelatorio.Columns.Add(campo3)

        Dim campo4 As New BoundField()
        campo4.DataField = "HorarioSaida"
        campo4.HeaderText = "Horario Saída"
        campo4.DataFormatString = "{0:HH:mm}"
        gvRelatorio.Columns.Add(campo4)

        Dim campo5 As New BoundField()
        campo5.DataField = "TempoPermanencia"
        campo5.HeaderText = "Tempo de Permanencia"
        gvRelatorio.Columns.Add(campo5)

        Dim campo9 As New BoundField()
        campo9.DataField = "Observacao"
        campo9.HeaderText = "Observação"
        gvRelatorio.Columns.Add(campo9)

        dt.Columns.Add("TempoPermanencia", GetType(String))
        For Each row As DataRow In dt.Rows
            Dim entradaStr As String = row("HorarioEntrada").ToString()
            Dim saidaStr As String = row("HorarioSaida").ToString()

            If Not String.IsNullOrEmpty(entradaStr) AndAlso Not String.IsNullOrEmpty(saidaStr) Then
                Dim entrada As DateTime
                Dim saida As DateTime

                If DateTime.TryParse(entradaStr, entrada) AndAlso DateTime.TryParse(saidaStr, saida) Then
                    Dim permanencia As TimeSpan = saida - entrada

                    row("TempoPermanencia") = permanencia

                Else
                    row("TempoPermanencia") = "-"
                End If
            Else
                row("TempoPermanencia") = "-"
            End If
        Next

        ' Fazer o bind dos dados no GridView
        gvRelatorio.DataSource = dt
        gvRelatorio.DataBind()
    End Sub

    Protected Sub gvRelatorio_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRelatorio.RowDataBound

        If Trim(ddl_tipoRelatorio.Text) = "TEMPO DE PERMANENCIA" Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim tempoPermanenciaStr As String = DataBinder.Eval(e.Row.DataItem, "TempoPermanencia").ToString()
                Dim tempoPadraoStr As String = DataBinder.Eval(e.Row.DataItem, "TempoPadrao").ToString()

                Dim tempoPermanencia As TimeSpan
                Dim tempoPadrao As TimeSpan
                Dim status As String = "DADOS INVÁLIDOS"

                If TimeSpan.TryParse(tempoPermanenciaStr, tempoPermanencia) AndAlso TimeSpan.TryParse(tempoPadraoStr, tempoPadrao) Then
                    If tempoPermanencia > tempoPadrao Then
                        status = "FORA DO TEMPO"
                    ElseIf tempoPermanencia < tempoPadrao Then
                        status = "TEMPO ANTECIPADO"
                    Else
                        status = "DENTRO DO TEMPO"
                    End If
                End If

                ' Agora usando o índice correto da coluna "Status"
                e.Row.Cells(6).Text = status
            End If
        End If

    End Sub

    Private Function ObterDadosBanco() As DataTable
        Dim vAno As Integer = txt_ano.Text
        Dim vTipoCadastro As String = "EMBARQUE"

        Dim conexaoBD As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)

        Dim consultaSQL As String = $"SELECT * FROM tb_Cadastro WHERE YEAR(DataCadastro) = {vAno} AND TipoCadastro = 'EMBARQUE' AND Status = 4 "

        If Not String.IsNullOrEmpty(ddl_dia.Text) Then
            Dim vDia As Integer = CInt(ddl_dia.Text)
            consultaSQL &= $" AND DAY(DataCadastro) = {vDia}"
        End If

        If Not String.IsNullOrEmpty(ddl_mes.Text) Then
            Dim vMes As Integer = CInt(ddl_mes.Text)
            consultaSQL &= $" AND MONTH(DataCadastro) = {vMes}"
        End If

        ' Veja a query que será executada
        System.Diagnostics.Debug.WriteLine("Consulta SQL: " & consultaSQL)

        Dim dt As New DataTable()

        Using conexaoBD
            conexaoBD.Open()
            Using cmd As New SqlCommand(consultaSQL, conexaoBD)
                Using adaptador As New SqlDataAdapter(cmd)
                    adaptador.Fill(dt)
                End Using
            End Using
        End Using

        ' Mostre quantas linhas vieram
        System.Diagnostics.Debug.WriteLine("Qtd Linhas retornadas: " & dt.Rows.Count)


        Return dt
    End Function

    Private Function ObterDadosBanco2() As DataTable
        Dim vAno As Integer = txt_ano.Text

        Dim conexaoBD As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)

        Dim consultaSQL As String = $"SELECT * FROM tb_Cadastro WHERE YEAR(DataCadastro) = {vAno} AND TipoCadastro = 'RECEBIMENTO' AND Status = 4 "

        If Not String.IsNullOrEmpty(ddl_dia.Text) Then
            Dim vDia As Integer = CInt(ddl_dia.Text)
            consultaSQL &= $" AND DAY(DataCadastro) = {vDia}"
        End If

        If Not String.IsNullOrEmpty(ddl_mes.Text) Then
            Dim vMes As Integer = CInt(ddl_mes.Text)
            consultaSQL &= $" AND MONTH(DataCadastro) = {vMes}"
        End If

        ' Veja a query que será executada
        System.Diagnostics.Debug.WriteLine("Consulta SQL: " & consultaSQL)

        Dim dt As New DataTable()

        Using conexaoBD
            conexaoBD.Open()
            Using cmd As New SqlCommand(consultaSQL, conexaoBD)
                Using adaptador As New SqlDataAdapter(cmd)
                    adaptador.Fill(dt)
                End Using
            End Using
        End Using

        ' Mostre quantas linhas vieram
        System.Diagnostics.Debug.WriteLine("Qtd Linhas retornadas: " & dt.Rows.Count)


        Return dt
    End Function

    Protected Sub gvRelatorio_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub BtnTempoPermanencia_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(Trim(txt_ano.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", "alert('Favor colocar o ANO'); abrirModalRelatorio();", True)
            txt_ano.Focus()
            Exit Sub
        End If

        If String.IsNullOrEmpty(Trim(ddl_tipoRelatorio.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", "alert('Favor selecione o tipo de relatório'); abrirModalRelatorio();", True)
            ddl_tipoRelatorio.Focus()
            Exit Sub
        End If

        If Trim(ddl_tipoRelatorio.Text) = "TEMPO DE PERMANENCIA" Then
            executar()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalRelatorio();", True)
        ElseIf Trim(ddl_tipoRelatorio.Text) = "RECEBIMENTO" Then
            executar2()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalRelatorio();", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "alert('Erro ao gerar relatório!')", True)
        End If



    End Sub

    Protected Sub btnGerarRelatorio_Click(sender As Object, e As EventArgs)
        If gvRelatorio.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", "alert('Favor gerar o relatório');", True)
            btnPermanencia.Focus()
            Exit Sub
        Else
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=Relatorio.xlsx")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.UTF8

            ' Remova controles de paginação do GridView, se houver
            gvRelatorio.AllowPaging = False

            Dim sw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)

            gvRelatorio.RenderControl(hw)

            ' Para evitar erro de "control must be placed inside <form runat=server>"
            Response.Output.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>")
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End If

    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Necessário para exportação funcionar
    End Sub

    Protected Sub btn_voltar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Principal.aspx")
    End Sub
End Class