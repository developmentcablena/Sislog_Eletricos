Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics

Partial Public Class Usuarios
    Inherits System.Web.UI.UserControl
    Private vConexao As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        vConexao = Session("vConexao")

        If Not IsPostBack Then
            CarregarNotasFiscais()

            If Session("FuncaoUsuario") = "Adiministrador" Then
                chk_eletricos.Enabled = True
                chk_telecom.Enabled = True
            Else
                chk_eletricos.Enabled = False
                chk_telecom.Enabled = False
            End If
        End If

            If Session("FuncaoUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
    End Sub

    Private Sub CarregarNotasFiscais()
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString)
                Dim query As String = "SELECT Nome, Usuario, Funcao FROM tb_Usuarios"
                Dim cmd As New SqlCommand(query, conn)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()

                conn.Open()
                da.Fill(dt)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    gvCadastros.DataSource = dt
                    gvCadastros.DataBind()
                Else
                    gvCadastros.DataSource = Nothing
                    gvCadastros.DataBind()
                End If
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", $"alert('Erro: {ex.Message}')", True)
        End Try
    End Sub

    Protected Sub btnAbrirModal(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalUsuarios(); abrirModalCadastrar();", True)
    End Sub

    Protected Sub gvCadastros_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim commandName As String = e.CommandName
        Dim Usuario As String

        Usuario = e.CommandArgument.ToString()
        Session("Usuario") = Usuario

        Select Case commandName

            Case "Editar"
                AbrirModal(Usuario)
        End Select

    End Sub

    Private Sub AbrirModal(ByVal cadastroID As String)
        CarregarDadosModal(cadastroID)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalUsuarios(); abrirModalCadastrar();", True)

    End Sub


    Private Sub CarregarDadosModal(ByVal cadastroID As String)
        btnsalvar.Text = "Atualizar"
        Dim conexao As New SqlConnection(ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString)
        Dim comando As SqlCommand

        comando = New SqlCommand("SELECT * FROM tb_Usuarios WHERE Usuario = @ID", conexao)

        comando.Parameters.AddWithValue("@ID", cadastroID)
        Try
            conexao.Open()
            Dim leitor As SqlDataReader = comando.ExecuteReader()
            If leitor.Read() Then
                ' Armazena os dados na Session
                Dim valorColuna1 As String = leitor(0).ToString()
                Dim valorColuna2 As String = leitor(1).ToString()
                Dim valorColuna3 As String = leitor(2).ToString()
                Dim valorFuncao As String = leitor(3).ToString()
                Dim valorEmpresa As Integer = leitor("Empresa").ToString()

                txtNome.Text = valorColuna1
                txtUsuario.Text = valorColuna2
                txtEmail.Text = valorColuna3
                ddlFuncao.Text = valorFuncao
                If valorEmpresa = 1 Then
                    chk_eletricos.Checked = True
                    chk_telecom.Checked = False
                ElseIf valorEmpresa = 2 Then
                    chk_telecom.Checked = True
                    chk_eletricos.Checked = False
                ElseIf valorEmpresa = 3 Then
                    chk_eletricos.Checked = True
                    chk_telecom.Checked = True
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erroDB", "alert('Erro ao carregar os dados!');", True)
        Finally
            conexao.Close()
        End Try
    End Sub


    Protected Sub btnsalvar_Click(sender As Object, e As EventArgs)
        Dim nome As String = txtNome.Text
        Dim usuario As String = txtUsuario.Text
        Dim email As String = txtEmail.Text
        Dim funcao As String = ddlFuncao.Text.Trim()


        If String.IsNullOrEmpty(Trim(nome)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alerta", "alert('Favor colocar o nome!')", True)
            txtNome.Focus()
            Exit Sub
        End If

        If (String.IsNullOrEmpty(Trim(txtUsuario.Text))) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alerta", "alert('Favor colocar o nome de usuario!')", True)
            txtUsuario.Focus()
            Exit Sub
        End If

        If btnsalvar.Text = "Atualizar" Then
            AtualizarDados(usuario)
        Else
            Dim vEmpresa As Integer = 0

            If chk_eletricos.Checked And chk_telecom.Checked Then
                vEmpresa = 3
            ElseIf chk_eletricos.Checked Then
                vEmpresa = 1
            ElseIf chk_telecom.Checked Then
                vEmpresa = 2
            End If

            btnsalvar.Text = "Salvar"
            Dim connectionString = ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString
                Using conn As New SqlConnection(connectionString)
                Dim sql As String = "INSERT INTO tb_Usuarios (Nome, Usuario, Email, Funcao, Empresa) 
                             VALUES (@Nome, @Usuario, @Email, @Funcao, @empresa)"

                Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Nome", nome)
                        cmd.Parameters.AddWithValue("@Usuario", usuario)
                        cmd.Parameters.AddWithValue("@Email", email)
                    cmd.Parameters.AddWithValue("@Funcao", funcao)
                    cmd.Parameters.AddWithValue("@empresa", vEmpresa)

                    Try
                            conn.Open()
                            cmd.ExecuteNonQuery()
                            conn.Close()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "abrirModalUsuarios(); alert('Usuário codastrado com sucesso!');", True)
                            CarregarNotasFiscais()
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", $"alert('Erro: {ex.Message}')", True)
                        End Try

                    End Using
                End Using

                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "abrirModalUsuarios(); alert('Senhas não se conferem!');", True)

            End If


    End Sub

    Private Sub AtualizarDados(ByVal usuarioID As String)
        Dim nome As String = txtNome.Text
        Dim usuario As String = txtUsuario.Text
        Dim email As String = txtEmail.Text
        Dim funcao As String = ddlFuncao.Text.Trim()
        Dim vEmpresa As Integer = 0

        If chk_eletricos.Checked And chk_telecom.Checked Then
            vEmpresa = 3
        ElseIf chk_eletricos.Checked Then
            vEmpresa = 1
        ElseIf chk_telecom.Checked Then
            vEmpresa = 2
        End If

        Dim connectionString = ConfigurationManager.ConnectionStrings($"{vConexao}").ConnectionString
        Using conn As New SqlConnection(connectionString)
            Dim sql As String = "UPDATE  tb_Usuarios set Nome = @nome, Usuario = @usuario, Email = @email, Funcao = @funcao, Empresa = @empresa WHERE Usuario = @usuarioID"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@nome", nome)
                cmd.Parameters.AddWithValue("@usuario", usuario)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@funcao", funcao)
                cmd.Parameters.AddWithValue("@empresa", vEmpresa)
                cmd.Parameters.AddWithValue("@usuarioID", usuario)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "abrirModalUsuarios(); alert('Dados atualizado com sucesso!');", True)
                    CarregarNotasFiscais()
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", $"alert('Erro: {ex.Message}')", True)
                End Try
            End Using
        End Using
    End Sub

    Protected Sub gvCadastros_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvCadastros.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If
    End Sub



End Class
