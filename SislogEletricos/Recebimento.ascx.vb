Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.Data

Partial Public Class Recebimento
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtData.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm")
            txtData.Enabled = False
            btnCadastrar.Enabled = True
        End If
        If Session("FuncaoUsuario") Is Nothing Then
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
    End Sub

    Protected Sub btnCadastrar_Click(sender As Object, e As EventArgs)
        'Dim horaAtual As Integer = Now.Hour
        'Dim minutoAtual As Integer = Now.Minute

        'If (horaAtual = 11) OrElse
        '       (horaAtual = 12 AndAlso minutoAtual >= 30) OrElse
        '       (horaAtual = 13 AndAlso minutoAtual < 30) OrElse
        '       (horaAtual = 14 AndAlso minutoAtual < 35) Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alerta", "alert('PROIBIDO cadastrarno horário das 11:00 às 12:00, 12:30 às 13:30 e 14:00 às 14:35');", True)
        '    Exit Sub
        'Else
        Dim connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim vNotaFiscal As String = txtNotaFiscal.Text
        Dim vFornecedor As String = txtFornecedor.Text
        Dim vCidade As String = txtCidade.Text
        Dim vUF As String = txtUF.Text
        Dim vTransportadora As String = txtTransportadora.Text
        Dim vFrete As String = ddlFrete.Text
        Dim vMotorista As String = txtMotorista.Text
        Dim vRG As String = txtRG.Text
        Dim vPlaca As String = txtPlaca.Text
        Dim vMaterial As String = txtMaterial.Text
        Dim vVolumes As String = txtVolumes.Text
        Dim vData As DateTime = DateTime.Parse(txtData.Text)
        Dim vObs As String = txtObservacao.Text
        Dim vCadastro As String = "RECEBIMENTO"
        'Dim vDataJanela As DateTime = DateTime.Parse(dataJanela.Text)
        Dim vDataJanela As DateTime
        If Not String.IsNullOrWhiteSpace(dataJanela.Text) Then
            vDataJanela = DateTime.Parse(dataJanela.Text)
        Else
        End If
        Dim IDnome As String = Session("Nome")

        If String.IsNullOrEmpty(Trim(txtFornecedor.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir o Fornecedor'); abrirModal();", True)
            Me.txtFornecedor.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtCidade.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a Cidade'); abrirModal();", True)
            Me.txtCidade.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtUF.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor colocar o UF'); abrirModal();", True)
            Me.txtUF.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtTransportadora.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a transportadora'); abrirModal();", True)
            Me.txtTransportadora.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(ddlFrete.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o frete'); abrirModal();", True)
            Me.ddlFrete.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMotorista.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do motorista'); abrirModal();", True)
            Me.txtMotorista.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtRG.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o RG'); abrirModal();", True)
            Me.txtRG.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPlaca.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir a placa'); abrirModal();", True)
            Me.txtPlaca.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMaterial.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do material'); abrirModal();", True)
            Me.txtMaterial.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtVolumes.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar a quantidade de volumes'); abrirModal();", True)
            Me.txtVolumes.Focus()
            Exit Sub
        End If



        Using conn As New SqlConnection(connectionString)
            Dim Query As String = "INSERT INTO tb_Cadastro (NotaFiscal, FornecedorCliente, Cidade, UF, Transportadora, Frete, Motorista, RG, Placa, Material, Volumes, DataCadastro, Observacao, Status, TipoCadastro, DataJanela, IDnome)
                VALUES(@NotaFiscal, @Fornecedor, @Cidade, @UF, @Transportadora, @Frete, @Motorista, @RG, @Placa, @Material, @Volumes, @DataCadastro, @Observacao, @Status, @TipoCadastro, @DataJanela, @idnome); " &
                "SELECT SCOPE_IDENTITY();"

            Using cmd As New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@NotaFiscal", vNotaFiscal)
                cmd.Parameters.AddWithValue("@Fornecedor", vFornecedor)
                cmd.Parameters.AddWithValue("@Cidade", vCidade)
                cmd.Parameters.AddWithValue("@UF", vUF)
                cmd.Parameters.AddWithValue("@Transportadora", vTransportadora)
                cmd.Parameters.AddWithValue("@Frete", vFrete)
                cmd.Parameters.AddWithValue("@Motorista", vMotorista)
                cmd.Parameters.AddWithValue("@RG", vRG)
                cmd.Parameters.AddWithValue("@Placa", vPlaca)
                cmd.Parameters.AddWithValue("@Material", vMaterial)
                cmd.Parameters.AddWithValue("@Volumes", vVolumes)
                cmd.Parameters.AddWithValue("@DataCadastro", vData)
                cmd.Parameters.AddWithValue("@Observacao", vObs)
                cmd.Parameters.AddWithValue("@Status", 1)
                cmd.Parameters.AddWithValue("@TipoCadastro", vCadastro)
                If vDataJanela = DateTime.MinValue OrElse vDataJanela.TimeOfDay = TimeSpan.Zero Then
                    cmd.Parameters.AddWithValue("@DataJanela", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@DataJanela", vDataJanela)
                End If
                cmd.Parameters.AddWithValue("@idnome", IDnome)

                Try
                    conn.Open()

                    Dim registerID As Integer
                    registerID = Convert.ToInt32(cmd.ExecuteScalar())
                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Sucesso", $"alert('Nº {registerID} cadastrado com sucesso!'); window.location.href='" & Request.Url.AbsoluteUri & "';", True)

                    FnEnviarEmail(registerID)

                    txtNotaFiscal.Text = " "
                    txtFornecedor.Text = " "
                    txtCidade.Text = " "
                    txtUF.Text = " "
                    txtTransportadora.Text = " "
                    ddlFrete.Text = " "
                    txtMotorista.Text = " "
                    txtRG.Text = " "
                    txtPlaca.Text = " "
                    txtMaterial.Text = " "
                    txtMaterial.Text = " "
                    txtData.Text = " "
                    txtObservacao.Text = " "
                    btnCadastrar.Enabled = True

                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", "alert('Erro ao cadastrar: " & ex.Message.Replace("'", "\'") & "'); abrirmodal();", True)
                End Try
            End Using
        End Using
    End Sub

    Private Function FnEnviarEmail(ByVal vID As Long) As Boolean

        Try
            Dim mail As New MailMessage()

            mail.From = New MailAddress(FnContaSMTP())
            mail.To.Add("mpaixao@cablena.com.br")
            mail.Subject = "SisLOG Elétricos - novo cadastro " & Format(vID, "0000")
            Dim corpoEmail As String = suRelatorio(vID)
            mail.Body = corpoEmail
            mail.IsBodyHtml = False

            Dim smtp As New SmtpClient("email-ssl.com.br")
            smtp.Port = 587
            smtp.Credentials = New System.Net.NetworkCredential(FnContaSMTP(), FnSenhaSMTP())
            smtp.EnableSsl = True

            smtp.Send(mail)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SucessoEmail", $"alert('Email enviado com sucesso!'); fecharModal();", True)
            Return True

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Erro", $"alert('Erro ao enviar email: {ex.Message}'); abrirModal();", True)
            Return False
        End Try

    End Function

    Private Function suRelatorio(ByVal vID As Long)
        Dim str As String = New String("="c, 120) & vbCrLf & vbCrLf
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString)
        Dim query As String = "SELECT * FROM tb_Cadastro WHERE CadastroID = @CadastroID"
        Dim cmd As New SqlCommand(query, con)

        cmd.Parameters.AddWithValue("@CadastroID", vID)
        Dim strRelatorio As String = ""
        Try
            con.Open()

            Dim rs As SqlDataReader = cmd.ExecuteReader()

            If rs.Read() Then

                strRelatorio &= str
                strRelatorio &= "Nº do Cadastro: " & Format(rs!CadastroID, "0000") & vbCrLf & vbCrLf
                strRelatorio &= "Tipo Cadastro: " & rs!TipoCadastro & vbCrLf & vbCrLf
                strRelatorio &= "Fornecedor: " & rs!FornecedorCliente & vbCrLf & vbCrLf
                strRelatorio &= "Transportadora: " & rs!Transportadora & vbCrLf & vbCrLf
                strRelatorio &= "Placa: " & rs!Placa & vbCrLf & vbCrLf
                strRelatorio &= "Status: Pendente " & vbCrLf & vbCrLf
                strRelatorio &= "Data Cadastro: " & rs!DataCadastro & vbCrLf & vbCrLf
                strRelatorio &= str
            End If
            rs.Close()

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

End Class

