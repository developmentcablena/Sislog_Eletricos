Imports System.Data
Imports System.Data.SqlClient


Partial Public Class Relatorio
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub
    ' Crie uma propriedade pública para expor o GridView, se quiser acessar de fora
    Public ReadOnly Property GridRelatorio As GridView
        Get
            Return gvRelatorio
        End Get
    End Property

    Public Sub executar()
        Dim dt As DataTable = ObterDadosBanco()
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


        dt.Columns.Add("TempoPermanencia", GetType(String))

        For Each row As DataRow In dt.Rows
            Dim entradaStr As String = row("HorarioEntrada").ToString()
            Dim saidaStr As String = row("HorarioSaida").ToString()

            If Not String.IsNullOrEmpty(entradaStr) AndAlso Not String.IsNullOrEmpty(saidaStr) Then
                Dim entrada As DateTime
                Dim saida As DateTime

                If DateTime.TryParse(entradaStr, entrada) AndAlso DateTime.TryParse(saidaStr, saida) Then
                    Dim permanencia As TimeSpan = saida - entrada
                    row("TempoPermanencia") = $"{permanencia.Hours}h {permanencia.Minutes}m"
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


    Private Function ObterDadosBanco() As DataTable
        Dim vAno As Integer = txt_ano.Text
        Dim vMes As Integer = ddl_mes.Text
        Dim vTipoCadastro As String = "EMBARQUE"

        Dim conexaoBD As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)

        Dim consultaSQL As String = $"SELECT * FROM tb_Cadastro WHERE YEAR(DataCadastro) = {vAno} AND MONTH(DataCadastro) = {vMes} AND TipoCadastro = 'EMBARQUE' AND Status = 4 "

        If Not String.IsNullOrEmpty(ddl_dia.Text) Then
            Dim vDia As Integer = CInt(ddl_dia.Text)
            consultaSQL &= $" AND DAY(DataCadastro) = {vDia}"
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

        executar()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalRelatorio();", True)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal", "abrirModalRelatorio();", True)

    End Sub

    Protected Sub btnGerarRelatorio_Click(sender As Object, e As EventArgs)
        ' Recrie os dados e o bind antes de exportar, se necessário
        executar() ' Se precisar garantir que o GridView está preenchido

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Relatorio.xls")
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
        Debug.WriteLine("relatorio")
    End Sub


End Class