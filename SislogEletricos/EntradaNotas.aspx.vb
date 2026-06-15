
Imports System.Data
Imports System.Data.SqlClient
Public Class EntradaNotas
    Inherits System.Web.UI.Page

    Private vConexao As String
    Private vUsuario As String
    Private RazaoSocial
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        vConexao = Session("vConexao")
        vUsuario = Session("Usuario")

        If Session("Permissao") = 1 Then
            Session("Permissao") = 0
        Else
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "abrirModalCadastrar();", True)
        End If

        If Session("FuncaoUsuario") = "Adiministrador" Or Session("FuncaoUsuario") = "Liberador" Then
            btnModalExportar.Enabled = True
        Else
            btnModalExportar.Enabled = False
        End If


        If Not IsPostBack Then
            executar()

            Dim ok = Request.QueryString("ok")
            Dim msg = TryCast(Session("FlashMsg"), String)

            If ok = "1" AndAlso Not String.IsNullOrEmpty(msg) Then
                Dim safe = System.Web.HttpUtility.JavaScriptStringEncode(msg)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "FlashMsg", $"alert('{safe}');", True)
                Session.Remove("FlashMsg") ' consome a mensagem
            End If
        End If
    End Sub

    Public Sub executar()
        Dim dt As DataTable = ObterDadosBanco()
        Debug.WriteLine("exewcutar")
        gvEntradaNotas.Columns.Clear()

        ' Criar colunas dinamicamente com base nas colunas do DataTable

        Dim campo1 As New BoundField()
        campo1.DataField = "ID"
        campo1.HeaderText = "ID"
        gvEntradaNotas.Columns.Add(campo1)

        Dim campo2 As New BoundField()
        campo2.DataField = "CHAVE_ACESSO"
        campo2.HeaderText = "Chave de acesso"
        gvEntradaNotas.Columns.Add(campo2)

        Dim campo3 As New BoundField()
        campo3.DataField = "DATA_CADASTRO"
        campo3.HeaderText = "Data"
        'campo2.DataFormatString = "{0:dd/MM/yyyy}"
        campo2.HtmlEncode = False
        gvEntradaNotas.Columns.Add(campo3)

        Dim campo4 As New BoundField()
        campo4.DataField = "NOTA_FISCAL"
        campo4.HeaderText = "Nota Fiscal"
        gvEntradaNotas.Columns.Add(campo4)

        Dim campo5 As New BoundField()
        campo5.DataField = "CNPJ"
        campo5.HeaderText = "CNPJ"
        gvEntradaNotas.Columns.Add(campo5)

        Dim campo6 As New BoundField()
        campo6.DataField = "RAZAO_SOCIAL"
        campo6.HeaderText = "Razão Social"
        gvEntradaNotas.Columns.Add(campo6)

        ' Fazer o bind dos dados no GridView
        gvEntradaNotas.DataSource = dt
        gvEntradaNotas.DataBind()
    End Sub

    Public Sub ExecutarXLS()
        Dim dt As DataTable = ObterDadosBancoXLS()
        Debug.WriteLine("exewcutar")
        gvEntradaNotas.Columns.Clear()

        ' Criar colunas dinamicamente com base nas colunas do DataTable

        Dim campo1 As New BoundField()
        campo1.DataField = "ID"
        campo1.HeaderText = "ID"
        gvEntradaNotas.Columns.Add(campo1)

        Dim campo2 As New BoundField()
        campo2.DataField = "CHAVE_ACESSO"
        campo2.HeaderText = "Chave de acesso"
        gvEntradaNotas.Columns.Add(campo2)

        Dim campo3 As New BoundField()
        campo3.DataField = "DATA_CADASTRO"
        campo3.HeaderText = "Data"
        'campo2.DataFormatString = "{0:dd/MM/yyyy}"
        campo2.HtmlEncode = False
        gvEntradaNotas.Columns.Add(campo3)

        Dim campo4 As New BoundField()
        campo4.DataField = "NOTA_FISCAL"
        campo4.HeaderText = "Nota Fiscal"
        gvEntradaNotas.Columns.Add(campo4)

        Dim campo5 As New BoundField()
        campo5.DataField = "CNPJ"
        campo5.HeaderText = "CNPJ"
        gvEntradaNotas.Columns.Add(campo5)

        Dim campo6 As New BoundField()
        campo6.DataField = "RAZAO_SOCIAL"
        campo6.HeaderText = "Razão Social"
        gvEntradaNotas.Columns.Add(campo6)

        ' Fazer o bind dos dados no GridView
        gvEntradaNotas.DataSource = dt
        gvEntradaNotas.DataBind()
    End Sub


    Private Function ObterDadosBanco() As DataTable
        Dim conexaoBD As New SqlConnection(ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString)

        Dim consultaSQL As String = "SELECT * FROM EntradaNotas  ORDER BY ID DESC"

        ' Veja a query que será executada
        ' System.Diagnostics.Debug.WriteLine("Consulta SQL: " & consultaSQL)

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
        ' System.Diagnostics.Debug.WriteLine("Qtd Linhas retornadas: " & dt.Rows.Count)

        Return dt
    End Function

    Private Function ObterDadosBancoXLS() As DataTable
        Dim conexaoBD As New SqlConnection(ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString)
        Dim vAno = txt_ano.Text.Trim()

        Dim consultaSQL As String = $"SELECT * FROM EntradaNotas WHERE YEAR(DATA_CADASTRO) = {vAno}"

        If Not String.IsNullOrEmpty(ddl_dia.Text) Then
            Dim vDia As Integer = CInt(ddl_dia.Text)
            consultaSQL &= $" AND DAY(DATA_CADASTRO) = {vDia}"
        End If

        If Not String.IsNullOrEmpty(ddl_mes.Text) Then
            Dim vMes As Integer = CInt(ddl_mes.Text)
            consultaSQL &= $" AND MONTH(DATA_CADASTRO) = {vMes}"
        End If

        ' Veja a query que será executada
        ' System.Diagnostics.Debug.WriteLine("Consulta SQL: " & consultaSQL)

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
        ' System.Diagnostics.Debug.WriteLine("Qtd Linhas retornadas: " & dt.Rows.Count)
        Return dt
    End Function


    Protected Sub btn_voltar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub gvEntradaNotas_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub btnGerarRelatorioExcel_Click(sender As Object, e As EventArgs)

        If String.IsNullOrEmpty(Trim(txt_ano.Text)) Then
            Session("Permissao") = 1
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Por favor, digite o ano antes de exportar.'); abrirModalExportar();", True)
        Else
            ExecutarXLS()
            Relatorio()
        End If
        'If gvEntradaNotas.Rows.Count = 0 Then
    End Sub

    Protected Sub gvEntradaNotas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvEntradaNotas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            ' AJUSTE OS NUMEROS DAS COLUNAS (0 = ID)
            ' Exemplo: 1 = Chave, 4 = CNPJ

            e.Row.Cells(1).Attributes.Add("class", "texto") ' CHAVE
            e.Row.Cells(4).Attributes.Add("class", "texto") ' CNPJ

        End If

    End Sub

    Public Sub Relatorio()
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Relatorio.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Response.ContentEncoding = System.Text.Encoding.UTF8

        ' Remova controles de paginação do GridView, se houver
        gvEntradaNotas.AllowPaging = False

        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)

        ' FORÇA TEXTO NO EXCEL
        Response.Output.Write("<style> .texto { mso-number-format:'\@'; } </style>")
        Response.Output.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>")

        gvEntradaNotas.RenderControl(hw)
        'Response.Output.Write(sw.ToString())

        ' Para evitar erro de "control must be placed inside <form runat=server>"
        Response.Output.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>")
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.[End]()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Necessário para exportação funcionar
    End Sub

    Protected Sub gvEntradaNotas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvEntradaNotas.PageIndex = e.NewPageIndex
        executar()

    End Sub
End Class