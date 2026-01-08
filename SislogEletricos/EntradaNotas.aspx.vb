
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
        executar()


        If Not IsPostBack Then
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


    Private Function ObterDadosBanco() As DataTable
        Dim conexaoBD As New SqlConnection(ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString)

        Dim consultaSQL As String = "SELECT * FROM EntradaNotas ORDER BY ID DESC"


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

    'Protected Sub btnCadastrar_Click(sender As Object, e As EventArgs)
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "modalCadastrar();", True)

    'End Sub






End Class