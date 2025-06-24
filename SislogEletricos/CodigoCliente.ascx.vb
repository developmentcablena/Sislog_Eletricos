Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics

Partial Public Class CodigoCliente
    Inherits System.Web.UI.UserControl

    Public connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarCodigoCliente()

        End If

    End Sub

    Private Sub CarregarCodigoCliente()
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
                Dim query As String = "SELECT * FROM tb_CodigoCliente"
                Dim cmd As New SqlCommand(query, conn)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()

                conn.Open()
                da.Fill(dt)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    gvCodigoCliente.DataSource = dt
                    gvCodigoCliente.DataBind()
                Else
                    gvCodigoCliente.DataSource = Nothing
                    gvCodigoCliente.DataBind()
                End If
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", $"alert('Erro: {ex.Message}')", True)
        End Try
    End Sub

    Protected Sub gvCodigoCliente_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim commandName As String = e.CommandName
        ' Recupera o índice da linha clicada
        Dim CodigoID As Integer = Convert.ToInt32(e.CommandArgument)
        Session("vCodigoID") = CodigoID
        Select Case commandName

            Case "Editar"
                AbrirModal(CodigoID)
            Case "Excluir"
                ExcluirRegistro(CodigoID)
        End Select
    End Sub

    Private Sub AbrirModal(ByVal vCodigoID As Integer)
        CarregarDadosModal(vCodigoID)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalCliente(); abrirModalCadastrarCliente();", True)
    End Sub

    Private Sub CarregarDadosModal(ByVal vCodigoID As Integer)
        btnsalvar.Text = "Atualizar"
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim comando As SqlCommand

        comando = New SqlCommand("SELECT * FROM tb_CodigoCliente WHERE CodigoID = @ID", conexao)

        comando.Parameters.AddWithValue("@ID", vCodigoID)
        Try
            conexao.Open()
            Dim leitor As SqlDataReader = comando.ExecuteReader()
            If leitor.Read() Then
                ' Armazena os dados na Session
                Dim valorCodigo As String = leitor("Codigo").ToString()
                Dim valorCliente As String = leitor("ClienteTransportadora").ToString()
                Dim valorTempo As String = leitor("TempoPadrao").ToString()
                Dim valorUF As String = leitor("UF").ToString()
                Dim valorCidade As String = leitor("Cidade").ToString()

                txtCodigo.Text = valorCodigo
                txtCliente.Text = valorCliente
                txtTempo.Text = valorTempo
                txtUF.Text = valorUF
                txtCidade.Text = valorCidade

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erroDB", "alert('Erro ao carregar os dados!');", True)
        Finally
            conexao.Close()
        End Try
    End Sub

    Private Sub ExcluirRegistro(ByVal vCodigoID As Integer)
        Using conn As New SqlConnection(connectionString)
            Dim sql As String = "DELETE FROM tb_CodigoCliente WHERE CodigoID = @idCodigo"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@idCodigo", vCodigoID)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SUCESSO", "abrirModalCliente(); alert('Excluido com sucesso!');", True)
                    CarregarCodigoCliente()
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERRO", $"abrirModalCliente(); alert('Erro: {ex.Message}');", True)
                End Try
            End Using
        End Using
    End Sub

    Protected Sub btnsalvar_Click(sender As Object, e As EventArgs)
        Dim vCodigoID As Integer = Session("vCodigoID")

        If Trim(btnsalvar.Text) = "Cadastrar" Then
            If String.IsNullOrEmpty(Trim(txtCodigo.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o CÓDIGO!'); abrirModalCliente(); abrirModalCadastrarCliente(); ", True)
                txtCodigo.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtCliente.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o CLIENTE TRANSPORTADORA!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtCliente.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtTempo.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o TEMPO PADRÃO!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtTempo.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtUF.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o UF!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtUF.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtCidade.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar a CIDADE!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtCidade.Focus()
                Exit Sub
            End If

            Dim vCodigo As Integer = txtCodigo.Text
            Dim vCliente As String = txtCliente.Text
            Dim vTempo As String = txtTempo.Text
            Dim vUF As String = txtUF.Text
            Dim vCidade As String = txtCidade.Text

            Using conn As New SqlConnection(connectionString)
                Dim sql As String = "INSERT INTO tb_CodigoCliente (Codigo, ClienteTransportadora, TempoPadrao, UF, Cidade)
                                    VALUES(@codigo, @cliente, @tempo, @uf, @cidade)"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", vCodigo)
                    cmd.Parameters.AddWithValue("@cliente", vCliente)
                    cmd.Parameters.AddWithValue("@tempo", vTempo)
                    cmd.Parameters.AddWithValue("@UF", vUF)
                    cmd.Parameters.AddWithValue("@cidade", vCidade)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Sucesso", "abrirModalCliente(); alert('Dados cadastrados com sucesso.');", True)
                        CarregarCodigoCliente()
                    Catch ex As Exception
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERRO", $"abrirModalCliente(); alert('Erro: {ex.Message}')", True)
                    End Try
                End Using
            End Using

        ElseIf Trim(btnsalvar.Text) = "Atualizar" Then
            If String.IsNullOrEmpty(Trim(txtCodigo.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o CÓDIGO!'); abrirModalCliente(); abrirModalCadastrarCliente(); ", True)
                txtCodigo.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtCliente.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o CLIENTE TRANSPORTADORA!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtCliente.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtTempo.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o TEMPO PADRÃO!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtTempo.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtUF.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar o UF!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtUF.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Trim(txtCidade.Text)) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Favor colocar a CIDADE!'); abrirModalCliente(); abrirModalCadastrarCliente();", True)
                txtCidade.Focus()
                Exit Sub
            End If

            Dim vCodigo As Integer = txtCodigo.Text
            Dim vCliente As String = txtCliente.Text
            Dim vTempo As String = txtTempo.Text
            Dim vUF As String = txtUF.Text
            Dim vCidade As String = txtCidade.Text

            Using conn As New SqlConnection(connectionString)
                Dim sql As String = "UPDATE tb_CodigoCliente SET Codigo = @codigo, ClienteTransportadora = @cliente, TempoPadrao = @tempo, UF = @uf, Cidade = @cidade WHERE CodigoID = @idCodigo"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@codigo", vCodigo)
                    cmd.Parameters.AddWithValue("@cliente", vCliente)
                    cmd.Parameters.AddWithValue("@tempo", vTempo)
                    cmd.Parameters.AddWithValue("@uf", vUF)
                    cmd.Parameters.AddWithValue("@cidade", vCidade)
                    cmd.Parameters.AddWithValue("@idCodigo", vCodigoID)
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SUCESSO", "abrirModalCliente(); alert('Dados atualizado com sucesso!');", True)
                        CarregarCodigoCliente()
                    Catch ex As Exception
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERRO", $"alert('Erro: {ex.Message}');", True)
                    End Try
                End Using
            End Using
        Else

        End If
    End Sub

    Protected Sub btnNovo_Click(sender As Object, e As EventArgs)
        btnsalvar.Text = "Cadastrar"
        txtCodigo.Text = ""
        txtCliente.Text = ""
        txtTempo.Text = ""
        txtUF.Text = ""
        txtCidade.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalCliente(); abrirModalCadastrarCliente();", True)
    End Sub
End Class