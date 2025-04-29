Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net
Imports System.Net.Mail
Imports System.Data
Imports System.Diagnostics

Partial Public Class Embarque
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    Public Sub AtualizarDados()
        ' Atualiza os dados do modal com base na sessão
        If Session("Coluna1") IsNot Nothing Then
            txtCliente.Text = Session("Coluna1").ToString()
        End If
    End Sub

    Protected Sub btnCadastrar_Embarque_Click(sender As Object, e As EventArgs)
        Dim connectionString = ConfigurationManager.ConnectionStrings("ConectarBD").ConnectionString
        Dim vNotaFiscal As String = txtNotaFiscal.Text
        Dim vCliente As String = txtCliente.Text
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
        Dim vCadastro As String = "EMBARQUE"
        Dim vPaletaBonina As String = txtPaletasbobinas.Text
        Dim vPeso As String = txtPeso.Text
        Dim vVeiculo As String = ddlVeiculo.Text
        Dim vCarga As String = txtCarga.Text
        'Dim vDataJanela As DateTime = DateTime.Parse(dataJanela.Text)
        Dim dataHora As DateTime
        Dim vDataJanela As DateTime
        If Not String.IsNullOrWhiteSpace(dataJanela.Text) Then
            vDataJanela = DateTime.Parse(dataJanela.Text)
        Else
        End If
        Dim IDnome As String = Session("Nome")

        If String.IsNullOrEmpty(Trim(txtCliente.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir o Fornecedor'); abrirModalEmbarque();", True)
            Me.txtCliente.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtCidade.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a Cidade'); abrirModalEmbarque();", True)
            Me.txtCidade.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtUF.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor colocar o UF'); abrirModalEmbarque();", True)
            Me.txtUF.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtTransportadora.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alerta", "alert('Favor inserir a transportadora'); abrirModalEmbarque();", True)
            Me.txtTransportadora.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(ddlFrete.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o frete'); abrirModalEmbarque();", True)
            Me.ddlFrete.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMotorista.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do motorista'); abrirModalEmbarque();", True)
            Me.txtMotorista.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtRG.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o RG'); abrirModalEmbarque();", True)
            Me.txtRG.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPlaca.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir a placa'); abrirModalEmbarque();", True)
            Me.txtPlaca.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtMaterial.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor inserir o nome do material'); abrirModalEmbarque();", True)
            Me.txtMaterial.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtVolumes.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar a quantidade de volumes'); abrirModalEmbarque();", True)
            Me.txtVolumes.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPaletasbobinas.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor selecionar Paletas/Bobinas'); abrirModalEmbarque();", True)
            Me.txtPaletasbobinas.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtPeso.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o peso(Kg)'); abrirModalEmbarque();", True)
            Me.txtPeso.Focus()
            Exit Sub
        End If
        If Trim(ddlVeiculo.Text) = "Selecione" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar o tipo de veiculo '); abrirModalEmbarque();", True)
            Me.ddlVeiculo.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(Trim(txtCarga.Text)) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('Favor colocar a capacidade da carga! '); abrirModalEmbarque();", True)
            Me.txtCarga.Focus()
            Exit Sub
        End If

        Using conn As New SqlConnection(connectionString)
            Dim Query As String = "INSERT INTO tb_Cadastro (NotaFiscal, FornecedorCliente, Cidade, UF, Transportadora, Frete, Motorista, RG, Placa, Material, Volumes, PaletesBobina, Peso, TipoVeiculo, CapacidadeCarga, DataCadastro, Observacao, Status, TipoCadastro, DataJanela, IDnome )

                VALUES(@NotaFiscal, @FornecedorCliente, @Cidade, @UF, @Transportadora, @Frete, @Motorista, @RG, @Placa, @Material, @Volumes, @PaletesBobina, @Peso, @TipoVeiculo, @CapacidadeCarga, @DataCadastro, @Observacao, @Status, @TipoCadastro, @DataJanela, @idnome); " &
                    "SELECT SCOPE_IDENTITY();"

            Using cmd As New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@NotaFiscal", vNotaFiscal)
                cmd.Parameters.AddWithValue("@FornecedorCliente", vCliente)
                cmd.Parameters.AddWithValue("@Cidade", vCidade)
                cmd.Parameters.AddWithValue("@UF", vUF)
                cmd.Parameters.AddWithValue("@Transportadora", vTransportadora)
                cmd.Parameters.AddWithValue("@Frete", vFrete)
                cmd.Parameters.AddWithValue("@Motorista", vMotorista)
                cmd.Parameters.AddWithValue("@RG", vRG)
                cmd.Parameters.AddWithValue("@Placa", vPlaca)
                cmd.Parameters.AddWithValue("@Material", vMaterial)
                cmd.Parameters.AddWithValue("@Volumes", vVolumes)
                cmd.Parameters.AddWithValue("@PaletesBobina", vPaletaBonina)
                cmd.Parameters.AddWithValue("@Peso", vPeso)
                cmd.Parameters.AddWithValue("@TipoVeiculo", vVeiculo)
                cmd.Parameters.AddWithValue("@CapacidadeCarga", vCarga)
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

                    Me.txtNotaFiscal.Text = " "
                    Me.txtCliente.Text = " "
                    Me.txtCidade.Text = " "
                    Me.txtUF.Text = " "
                    Me.txtTransportadora.Text = " "
                    Me.ddlFrete.Text = " "
                    Me.txtMotorista.Text = " "
                    Me.txtRG.Text = " "
                    Me.txtPlaca.Text = " "
                    Me.txtMaterial.Text = " "
                    Me.txtMaterial.Text = " "
                    Me.txtData.Text = " "
                    Me.txtObservacao.Text = " "
                    Me.txtPaletasbobinas.Text = "Selecione"
                    Me.txtPeso.Text = " "
                    Me.ddlVeiculo.Text = "Selecione"
                    Me.txtCarga.Text = " "

                    btnCadastrar.Enabled = True

                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", "alert('Erro ao cadastrar: " & ex.Message.Replace("'", "\'") & "'); fecharModalEmbarque();", True)
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
                strRelatorio &= "Cliente: " & rs!FornecedorCliente & vbCrLf & vbCrLf
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