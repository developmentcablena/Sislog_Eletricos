Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics

Partial Public Class LiberarPortaria
    Inherits System.Web.UI.UserControl

    Private Shared connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
    Public cadastroID As Integer

    'Private Shared cadastroID As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CarregarNotasFiscais()
        End If

    End Sub

    Protected Sub Unnamed_Click(sender As Object, e As EventArgs)
        CarregarNotasFiscais()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalLiberar();", True)
    End Sub


    Private Sub CarregarNotasFiscais()
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
                Dim query As String = "SELECT CadastroID, TipoCadastro, FornecedorCliente, Transportadora, Placa, 
                                           FORMAT(HorarioChegada, 'HH:mm') AS HorarioChegada,  
                                            FORMAT(HorarioEntrada, 'HH:mm') AS HorarioEntrada,  
                                            FORMAT(HorarioSaida, 'HH:mm dd-MM-yyyy') AS HorarioSaida 
                                            FROM tb_Cadastro 
                                            WHERE Status = 2"
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

    Protected Sub btnChegada(sender As Object, e As EventArgs)
        Dim vdata As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        If Session("cadastroID") IsNot Nothing Then
            cadastroID = Convert.ToInt32(Session("cadastroID"))
        Else
            cadastroID = 0
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim sql2 As String = "UPDATE tb_Cadastro SET HorarioChegada = @dataAtual WHERE CadastroID = @id2"
                Dim sql3 As String = "UPDATE tb_Cadastro SET StatusHorario = @valor WHERE CadastroID = @id"
                Using cmd As New SqlCommand(sql3, conn)
                    cmd.Parameters.AddWithValue("@valor", 2)
                    cmd.Parameters.AddWithValue("@id", cadastroID)
                    cmd.ExecuteNonQuery()
                End Using
                Using cmd2 As New SqlCommand(sql2, conn)
                    cmd2.Parameters.AddWithValue("@dataAtual", vdata)
                    cmd2.Parameters.AddWithValue("@id2", cadastroID)
                    cmd2.ExecuteNonQuery()
                End Using
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Horario de chegada cadastrado com sucesso!!'); fecharModalLiberacao();", True)
                CarregarNotasFiscais()

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "alert('Erro ao cadastrar o horario!')", True)
            End Try
        End Using
        ' Debug.WriteLine("ID usado no btnChegada: " & cadastroID)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalLiberar(); fecharModalLiberacao();", True)
    End Sub
    Protected Sub btnEntrada(sender As Object, e As EventArgs)
        Dim vdata As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        If Session("cadastroID") IsNot Nothing Then
            cadastroID = Convert.ToInt32(Session("cadastroID"))
        Else
            cadastroID = 0
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim sql2 As String = "UPDATE tb_Cadastro SET HorarioEntrada = @dataAtual WHERE CadastroID = @id2"
                Dim sql3 As String = "UPDATE tb_Cadastro SET StatusHorario = @valor WHERE CadastroID = @id"
                Using cmd As New SqlCommand(sql3, conn)
                    cmd.Parameters.AddWithValue("@valor", 3)
                    cmd.Parameters.AddWithValue("@id", cadastroID)
                    cmd.ExecuteNonQuery()
                End Using
                Using cmd2 As New SqlCommand(sql2, conn)
                    cmd2.Parameters.AddWithValue("@dataAtual", vdata)
                    cmd2.Parameters.AddWithValue("@id2", cadastroID)
                    cmd2.ExecuteNonQuery()
                End Using
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Horario de Entrada cadastrado com sucesso!!'); fecharModalLiberacao();", True)
                CarregarNotasFiscais()

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "alert('Erro ao cadastrar o horario!')", True)
            End Try
        End Using
        ' Debug.WriteLine("ID usado no btnChegada: " & cadastroID)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalLiberar(); fecharModalLiberacao();", True)
    End Sub
    Protected Sub btnSaida(sender As Object, e As EventArgs)
        Dim vdata2 As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        If Session("cadastroID") IsNot Nothing Then
            cadastroID = Convert.ToInt32(Session("cadastroID"))
        Else
            cadastroID = 0
        End If

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim sql2 As String = "UPDATE tb_Cadastro SET HorarioSaida = @dataAtual WHERE CadastroID = @id2"
                Dim sql3 As String = "UPDATE tb_Cadastro SET Status = @status WHERE CadastroID = @id"
                Using cmd As New SqlCommand(sql3, conn)
                    cmd.Parameters.AddWithValue("@status", 4)
                    cmd.Parameters.AddWithValue("@id", cadastroID)
                    cmd.ExecuteNonQuery()
                End Using
                Using cmd2 As New SqlCommand(sql2, conn)
                    cmd2.Parameters.AddWithValue("@dataAtual", vdata2)
                    cmd2.Parameters.AddWithValue("@id2", cadastroID)
                    cmd2.ExecuteNonQuery()
                End Using
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Horario de Saída cadastrado com sucesso!!'); fecharModalLiberacao();", True)
                CarregarNotasFiscais()

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "alert('Erro ao cadastrar o horario!')", True)
            End Try
        End Using
        ' Debug.WriteLine("ID usado no btnChegada: " & cadastroID)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalLiberar(); fecharModalLiberacao();", True)
    End Sub

    Protected Sub gvCadastros_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim commandName As String = e.CommandName
        cadastroID = Convert.ToInt32(e.CommandArgument)
        Session("cadastroID") = cadastroID
        Select Case commandName

            Case "Horarios"
                If Session("cadastroID") IsNot Nothing Then
                    cadastroID = Convert.ToInt32(Session("cadastroID"))
                Else
                    cadastroID = 0
                End If

                Using conn As New SqlConnection(connectionString)
                    Try
                        conn.Open()
                        Dim sql As String = "SELECT StatusHorario FROM tb_Cadastro WHERE CadastroID = @id"
                        Using cmd As New SqlCommand(sql, conn)
                            cmd.Parameters.AddWithValue("@id", cadastroID)

                            Dim valor As Object = cmd.ExecuteScalar()
                            ' Verifica se na coluna StatusHorarios esta com o valor = 1
                            If valor IsNot Nothing AndAlso Convert.ToInt32(valor) = 1 Then

                                chegadaID.Enabled = True
                                entradaID.Enabled = False
                                saidaID.Enabled = False

                            ElseIf valor = 2 Then
                                chegadaID.Enabled = False
                                entradaID.Enabled = True
                                saidaID.Enabled = False

                            ElseIf valor = 3 Then
                                chegadaID.Enabled = False
                                entradaID.Enabled = False
                                saidaID.Enabled = True

                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Erro!!!')", True)
                            End If
                        End Using
                    Catch ex As Exception
                        'Debug.WriteLine("deu errado")
                    End Try
                End Using
                'Debug.WriteLine("eee" & cadastroID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalLiberar(); abrirModalLiberacao();", True)
        End Select
    End Sub
End Class
