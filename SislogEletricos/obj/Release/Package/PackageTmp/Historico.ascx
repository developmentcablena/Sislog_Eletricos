<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Historico.ascx.vb" Inherits="SislogEletricos.Historico" %>

<link href="Style/Historico.css" rel="stylesheet" type="text/css" />

<style>
.modal3 {
        display: none;
        position: fixed;
        z-index: 9999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
    }

    .modal-content3 {
        background-color: white;
        margin: 6% auto;
        padding: 20px;
        width: 400px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    .h1__titulo-modal {
        background-color: #ff7f27;
        color: white;
        text-align: left;
        padding: 20px;
        margin: 0;
        width: 100%;
        font-size: 20px;
        font-weight: bold;
        border-radius: 6px 6px 0 0;
        position: relative;
        top: -40px;
        left: -20px;
    }

    #meuModal3 .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #meuModal3 .span__close:hover {
            font-size: 20px;
            color: black;
            background-color: transparent;
        }

    .modal__body {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 15px;
        padding: 0px;
    }

    .div__group {
        display: flex;
        flex-direction: column;
    }

        .div__group span {
            font-size: 14px;
            margin-bottom: 4px;
        }

    .textBox {
        padding: 6px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 14px;
    }

    #meuModal3 .btn-cadastrar {
        grid-column: span 2;
        padding: 10px;
        background-color: #ff7f27;
        color: white;
        font-size: 16px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        text-align: center;
        margin-top: 23%;
    }

    .textBox__obs {
        display: flex;
        position: absolute;
        resize: none;
        overflow: auto;
        width: 379px;
        height: 86px;
        border-radius: 4px;
        border: 1px solid #ccc;
        margin-top: 4px;
    }

    /*Modal Embarque*/
    .css__modal-Embarque3 {
        display: none;
        position: fixed;
        z-index: 99999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
    }

    .modal__content-Embarque3 {
        background-color: white;
        margin: 6% auto;
        padding: 20px;
        width: 400px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    .h1__titulo-modal {
        background-color: #ff7f27;
        color: white;
        text-align: left;
        padding: 20px;
        margin: 0;
        width: 100%;
        font-size: 20px;
        font-weight: bold;
        border-radius: 6px 6px 0 0;
        position: relative;
        top: -40px;
        left: -20px;
    }

    #modalEmbarque .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #modalEmbarque .span__close:hover {
            font-size: 20px;
            color: black;
            background-color: transparent;
        }

    .modal__body {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 15px;
        padding: 0px;
    }

    .div__group {
        display: flex;
        flex-direction: column;
    }

        .div__group span {
            font-size: 14px;
            margin-bottom: 4px;
        }

    .textBox {
        padding: 6px;
        border: 1px solid #ccc;
        border-radius: 4px;
        font-size: 14px;
    }

    #modalEmbarque3 .btn-cadastrar {
        grid-column: span 2;
        padding: 10px;
        background-color: #ff7f27;
        color: white;
        font-size: 16px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        text-align: center;
        margin-top: 23%;
    }


    .textBox__obs {
        display: flex;
        position: absolute;
        resize: none;
        overflow: auto;
        width: 379px;
        height: 86px;
        border-radius: 4px;
        border: 1px solid #ccc;
        margin-top: 4px;
        font-family: Arial, sans-serif;
    }

   
</style>

<asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="modalHistorico" class="css__modal-Historico">
            <div class="modal__content-Historico">
                <h1 class="h1__titulo-modal">Historico de Cadastro</h1>
                <asp:Button CssClass="btnAtualizar" Text="🔄" runat="server" OnClick="Unnamed_Click" />
                <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalHistorico();">x</span>

                <div class="grid-container-historico">
                        <asp:GridView runat="server" ID="gvCadastros" AutoGenerateColumns="false" GridLines="None" CssClass="grid-table-historico" UseAccessibleHeader="true" OnRowCommand="gvCadastros_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="CadastroID" HeaderText="Nº Cadastro" />
                                <asp:BoundField DataField="TipoCadastro" HeaderText="Tipo Cadastro" />
                                <asp:BoundField DataField="FornecedorCliente" HeaderText="Fornecedor/Cliente" />
                                <asp:BoundField DataField="Transportadora" HeaderText="Transportadora" />
                                <asp:BoundField DataField="Placa" HeaderText="Placa" />
                                <asp:BoundField DataField="HorarioChegada" HeaderText="Chegada" />
                                <asp:BoundField DataField="HorarioEntrada" HeaderText="Entrada" />
                                <asp:BoundField DataField="HorarioSaida" HeaderText="Saída" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAbrir" runat="server" CssClass="css_abir-dados-modal" CommandName="AbrirModal"
                                            CommandArgument='<%# Eval("CadastroID")%>' Text="📂" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
            </div>
        </div>

        <!-- Estrotura para abrir o modal Embarque  -->

        <div id="modalEmbarque3" class="css__modal-Embarque2">
            <div class="modal__content-Embarque3">
                <h1 class="h1__titulo-modal">Embarque</h1>
                <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalEmbarque3();">x</span>
                 <div class="modal__body">
                    <div class="div__group">
                        <asp:Label ID="Label1" runat="server" Text="Nota fiscal"></asp:Label>
                        <asp:TextBox ID="txtNotaFiscal" runat="server" CssClass="textBox" placeholder="Nota fiscal" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label2" runat="server" Text="Cliente"></asp:Label>
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="textBox" placeholder="Cliente" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label3" runat="server" Text="Cidade"></asp:Label>
                        <asp:TextBox ID="txtCidade" runat="server" CssClass="textBox" placeholder="Cidade" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="UF"></asp:Label>
                        <asp:TextBox ID="txtUF" runat="server" CssClass="textBox" placeholder="UF" MaxLength="2" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Transportadora"></asp:Label>
                        <asp:TextBox ID="txtTransportadora" runat="server" CssClass="textBox" placeholder="Transportadora" MaxLength="100" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="CIF / FOB"></asp:Label>
                        <asp:DropDownList ID="ddlFrete" runat="server" CssClass="textBox">
                            <asp:ListItem Text="Selecione" Value="" />
                            <asp:ListItem Text="CIF" />
                            <asp:ListItem Text="FOB" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Motorista"></asp:Label>
                        <asp:TextBox runat="server" ID="txtMotorista" CssClass="textBox" placeholder="Motorista" MaxLength="100" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="RG/CPF" runat="server" />
                        <asp:TextBox runat="server" ID="txtRG" CssClass="textBox" placeholder="RG/CPF" TextMode="Number" MaxLength="2"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Placa" runat="server" />
                        <asp:TextBox runat="server" ID="txtPlaca" CssClass="textBox" placeholder="Placa" MaxLength="17" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Material" runat="server" />
                        <asp:TextBox runat="server" ID="txtMaterial" CssClass="textBox" placeholder="Material" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div>
                        <asp:Label Text="Quantidades" runat="server" />
                        <asp:TextBox runat="server" ID="txtVolumes" CssClass="textBox" placeholder="Quantidades" TextMode="Number" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Espécie" runat="server" />
                        <asp:TextBox runat="server" ID="txtPaletasbobinas" CssClass="textBox" placeholder="Espécie" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Peso(kg)" runat="server" />
                        <asp:TextBox runat="server" ID="txtPeso" CssClass="textBox" placeholder="Peso(kg)" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Tipo de veiculo" runat="server" />
                        <asp:DropDownList runat="server" CssClass="textBox" ID="ddlVeiculo">
                            <asp:ListItem Text="Selecione" />
                            <asp:ListItem Text="MOTO" />
                            <asp:ListItem Text="C.PASSEIO" />
                            <asp:ListItem Text="FIORINO/PICK-UP" />
                            <asp:ListItem Text="VAM" />
                            <asp:ListItem Text="VEICULO 3/4" />
                            <asp:ListItem Text="V.TOCO" />
                            <asp:ListItem Text="V.CARRETA" />
                            <asp:ListItem Text="V.BITREM" />
                            <asp:ListItem Text="V.TRUCK" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Capacidade de carga" runat="server" />
                        <asp:TextBox runat="server" ID="txtCarga" CssClass="textBox" placeholder="Capacidade de carga" onkeyup="formatarMoeda(this)" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Data cadastro" runat="server" />
                        <asp:TextBox ID="txtData" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Data Embarque" runat="server" />
                        <asp:TextBox ID="dataJanelaEmbarque" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
                    </div>
                    <br />
                    <div>
                        <asp:Label Text="Observação" runat="server" />
                        <asp:TextBox runat="server" ID="txtObservacao" CssClass="textBox__obs" TextMode="MultiLine" placeholder="Observação" />
                    </div>
                    <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn-cadastrar" />
                </div>
            </div>
        </div>

        <!-- Estrotura para abrir o modal Recebimento -->

        <div id="meuModal3" class="modal3">
            <div class="modal-content3">
                <h1 class="h1__titulo-modal">Recebimento</h1>
                <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModal3();">x</span>
                 <div class="modal__body">
                    <div class="div__group">
                        <asp:Label ID="Label4" runat="server" Text="Nota fiscal"></asp:Label>
                        <asp:TextBox ID="txtNotaFiscal2" runat="server" CssClass="textBox" placeholder="Nota fiscal" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label5" runat="server" Text="Fornecedor"></asp:Label>
                        <asp:TextBox ID="txtFornecedor2" runat="server" CssClass="textBox" placeholder="Fornecedor" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label ID="Label6" runat="server" Text="Cidade"></asp:Label>
                        <asp:TextBox ID="txtCidade2" runat="server" CssClass="textBox" placeholder="Cidade" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="UF"></asp:Label>
                        <asp:TextBox ID="txtUF2" runat="server" CssClass="textBox" placeholder="UF" MaxLength="2"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Transportadora"></asp:Label>
                        <asp:TextBox ID="txtTransportadora2" runat="server" CssClass="textBox" placeholder="Transportadora" MaxLength="100" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="CIF / FOB"></asp:Label>
                        <asp:DropDownList ID="ddlFrete2" runat="server" CssClass="textBox">
                            <asp:ListItem Text="Selecione" Value="" />
                            <asp:ListItem Text="CIF" />
                            <asp:ListItem Text="FOB" />
                        </asp:DropDownList>
                    </div>
                    <div class="div__group">
                        <asp:Label runat="server" Text="Motorista"></asp:Label>
                        <asp:TextBox runat="server" ID="txtMotorista2" CssClass="textBox" placeholder="Motorista" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="RG" runat="server" />
                        <asp:TextBox runat="server" ID="txtRG2" CssClass="textBox" placeholder="RG" TextMode="Number" MaxLength="11" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Placa" runat="server" />
                        <asp:TextBox runat="server" ID="txtPlaca2" CssClass="textBox" placeholder="Placa" MaxLength="15" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Material" runat="server" />
                        <asp:TextBox runat="server" ID="txtMaterial2" CssClass="textBox" placeholder="Material" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Volumes" runat="server" />
                        <asp:TextBox runat="server" ID="txtVolumes2" CssClass="textBox" placeholder="Volumes" TextMode="Number" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Data cadastro" runat="server" />
                        <asp:TextBox ID="txtData2" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
                    </div>
                    <div class="div__group">
                        <asp:Label Text="Data Recebimento" runat="server" />
                        <asp:TextBox ID="dataJanelaRecebimento" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
                    </div>
                    <br />
                    <div>
                        <asp:Label Text="Observação" runat="server" />
                        <asp:TextBox runat="server" ID="txtOBS2" CssClass="textBox__obs" TextMode="MultiLine" placeholder="Observação" />
                    </div>
                    <asp:Button ID="btn2" runat="server" Text="Cadastrar" CssClass="btn-cadastrar" />
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<script>
     function abrirModal3() {
        document.getElementById("meuModal3").style.display = "block";
    }

    function fecharModal3() {
        document.getElementById("meuModal3").style.display = "none";
    }
    function abrirModalEmbarque3() {
        document.getElementById("modalEmbarque3").style.display = "block";
    }
    function fecharModalEmbarque3() {
        document.getElementById("modalEmbarque3").style.display = "none";
    }



    function abrirModalHistorico() {
        document.getElementById("modalHistorico").style.display = "block";
    }

    function fecharModalHistorico() {
        document.getElementById("modalHistorico").style.display = "none";
    }
    lucide.createIcons(); // Inicializar os ícones do Lucide
</script>
