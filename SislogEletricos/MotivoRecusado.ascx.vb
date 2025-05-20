Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics
Imports System.Net.Mail


Partial Public Class MotivoRecusado
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnEnviarMotivo_Click(sender As Object, e As EventArgs)

        EnviarMotivo()
    End Sub

    Private Sub EnviarMotivo()
        If String.IsNullOrEmpty(Trim(txtMotivo.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Favor colocar o motivo.'); abrirModalAutorizacao(); abrirModalMotivo();", True)
            txtMotivo.Focus()
            Exit Sub
        End If

        Dim motivo As String = txtMotivo.Text
        Dim vCadastroID As Integer = Session("vCadastroID")
        Debug.WriteLine("motivo" & motivo)
        Debug.WriteLine("id " & vCadastroID)
        Dim connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString

        Using conn As New SqlConnection(connectionString)
            Dim Query As String = "INSERT INTO tb_MotivoRecusado (MotivoRecusado, CadastroID) VALUES(@motivo, @cadastroID)"
            Using cmd As New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@motivo", motivo)
                cmd.Parameters.AddWithValue("@cadastroID", vCadastroID)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    atualizarStatus()

                    txtMotivo.Text = ""

                    FnEnviarEmail(vCadastroID)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Motivo enviado com sucesso!!'); abrirModalAutorizacao();", True)
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "sucesso", "alert('Erro ao enviar o motivo!'); abrirModalAutorizacao();", True)
                End Try
            End Using
        End Using

    End Sub

    Private Sub IDnomeCadastro(ByVal vID As Integer) 'Captura o nome do usuario que fez o cadastro.
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim query As String = "SELECT IDnome FROM tb_Cadastro WHERE CadastroID = @id"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@id", vID)

                Try
                    connection.Open()

                    Using reader As SqlDataReader = command.ExecuteReader()
                        ' Verifica se encontrou algum resultado
                        If reader.HasRows Then
                            reader.Read()

                            ' Obtém os valores e trata possíveis valores NULL
                            Dim nome As String = If(Not reader.IsDBNull(reader.GetOrdinal("IDnome")), reader("IDnome").ToString(), "")

                            ' Salva os valores na Sessão
                            HttpContext.Current.Session("Nome") = nome

                        End If
                    End Using

                Catch ex As Exception
                    ' Log do erro (opcional: salvar em um log ou mostrar uma mensagem)
                    Throw New Exception("Erro ao buscar dados do usuário: " & ex.Message)
                End Try
            End Using
        End Using

    End Sub

    Private Sub EmailDeUsuario() 'Captura o email de quem cadastrou
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim nomeUser As String = Session("Nome")

        Dim query As String = "SELECT Email FROM tb_Usuarios WHERE Nome = @nome"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@nome", nomeUser)
                Try
                    connection.Open()

                    Using reader As SqlDataReader = command.ExecuteReader()
                        ' Verifica se encontrou algum resultado
                        If reader.HasRows Then
                            reader.Read()

                            ' Obtém os valores e trata possíveis valores NULL
                            Dim email As String = If(Not reader.IsDBNull(reader.GetOrdinal("Email")), reader("Email").ToString(), "")

                            ' Salva os valores na Sessão
                            HttpContext.Current.Session("Email") = email

                        End If
                    End Using

                Catch ex As Exception
                    ' Log do erro (opcional: salvar em um log ou mostrar uma mensagem)
                    Throw New Exception("Erro ao buscar dados do usuário: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Function FnEnviarEmail(ByVal vID As Long) As Boolean
        IDnomeCadastro(vID)
        EmailDeUsuario()
        Try
            Dim mail As New MailMessage()
            Dim emailUser As String = Session("Email")

            mail.From = New MailAddress(FnContaSMTP())
            mail.To.Add(emailUser)
            mail.Subject = "SisLOG Elétricos - CADASTRO RECUSADO " & Format(vID, "0000")
            Dim corpoEmail As String = suRelatorio(vID)
            mail.Body = corpoEmail
            mail.IsBodyHtml = False

            Dim smtp As New SmtpClient("email-ssl.com.br")
            smtp.Port = 587
            smtp.Credentials = New System.Net.NetworkCredential(FnContaSMTP(), FnSenhaSMTP())
            smtp.EnableSsl = True
            smtp.Send(mail)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail2", $"alert('Email enviado com sucesso!'); fecharModalEmbarque();", True)
            Return True
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Erro", $"alert('Erro ao enviar email: {ex.Message}'); abrirModalEmbarque();", True)
            Return False
        End Try
    End Function

    Private Function suRelatorio(ByVal vID As Long)
        Dim str As String = New String("="c, 120) & vbCrLf & vbCrLf
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim query As String = "SELECT * FROM tb_Cadastro WHERE CadastroID = @CadastroID"
        Dim query1 As String = "SELECT * FROM tb_MotivoRecusado WHERE CadastroID = @id"
        Dim cmd As New SqlCommand(query, con)
        Dim cmd1 As New SqlCommand(query1, con)

        cmd.Parameters.AddWithValue("@CadastroID", vID)
        Dim strRelatorio As String = ""

        Try
            con.Open()

            ' Executa a primeira query
            Dim rs As SqlDataReader = cmd.ExecuteReader()
            Dim tipoCadastro As String = ""
            Dim cadastroID As String = ""
            Dim motivoRecusado As String = ""

            If rs.Read() Then
                tipoCadastro = rs("TipoCadastro").ToString()
                cadastroID = Format(rs("CadastroID"), "0000")
            End If
            rs.Close()

            ' Executa a segunda query
            cmd1.Parameters.AddWithValue("@id", vID)
            Dim rs1 As SqlDataReader = cmd1.ExecuteReader()
            If rs1.Read() Then
                motivoRecusado = rs1("MotivoRecusado").ToString()
            End If
            rs1.Close()

            ' Monta o relatório
            strRelatorio &= str
            strRelatorio &= "Nº do Cadastro: " & cadastroID & vbCrLf & vbCrLf
            strRelatorio &= "Tipo Cadastro: " & tipoCadastro & vbCrLf & vbCrLf
            strRelatorio &= "Motivo Recusado: " & motivoRecusado & vbCrLf & vbCrLf
            strRelatorio &= "Status: Recusado" & vbCrLf & vbCrLf
            strRelatorio &= str


        Catch ex As Exception
            strRelatorio = "Erro ao gerar o relatório: " & ex.Message
        Finally
            con.Close()

        End Try
        Return strRelatorio
    End Function

    Private Function FnContaSMTP() As String
        Dim strSQL As String = "SELECT * FROM tb_Email WHERE ID = 1"
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
            Using cmd As New SqlCommand(strSQL, cn)
                Try
                    cn.Open()
                    Using rs As SqlDataReader = cmd.ExecuteReader()
                        If rs.HasRows Then
                            rs.Read()
                            Return rs("Valor").ToString().Trim()
                        Else
                            Return ""
                        End If
                    End Using
                Catch ex As Exception
                    Return ""
                Finally
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End Try
            End Using
        End Using
    End Function

    Private Function FnSenhaSMTP() As String
        Dim strSQL As String = "SELECT * FROM tb_Email WHERE ID = 2"
        Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
            Using cmd As New SqlCommand(strSQL, cn)
                Try
                    cn.Open()
                    Using rs As SqlDataReader = cmd.ExecuteReader()
                        If rs.HasRows Then
                            rs.Read()
                            Return rs("Valor").ToString().Trim()
                        Else
                            Return ""
                        End If
                    End Using
                Catch ex As Exception
                    Return ""
                Finally
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End Try
            End Using
        End Using
    End Function

    Private Sub atualizarStatus()
        Dim connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim vCadastroID As Integer = Session("vCadastroID")

        Using conn As New SqlConnection(connectionString)
            Dim Query As String = "UPDATE tb_Cadastro SET Status = @status WHERE CadastroID = @vCadastroID"
            Using cmd As New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@status", 3)
                cmd.Parameters.AddWithValue("@vCadastroID", vCadastroID)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "erro", "alert('Erro ao altualizar STATUS!');", True)
                End Try
            End Using
        End Using

    End Sub

End Class

