<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="~/Recusados.ascx.vb" Inherits="SislogEletricos.Recusados" %>

<link href="Style/Recusados.css" rel="stylesheet" type="text/css" />

<style>
    /*Modal Motivo*/
.css__modal-Motivo2 {
    display: none;
    position: fixed;
    z-index: 999;
    left: 0;
    top: -0%;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.4);
}

.modal__content-Motivo2 {
    background-color: white;
    margin: 16% auto;
    padding: 20px;
    width: 19%;
    border-radius: 6px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
}

#modalMotivo2 .h1__titulo-modal {
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
    margin-bottom: -48px;
}

#modalMotivo2 .span__close {
    color: white;
    float: right;
    font-size: 20px;
    cursor: pointer;
    z-index: 2;
    position: relative;
    top: -40px;
}

    #modalMotivo2 .span__close:hover {
        font-size: 25px;
        color: #918a8a;
    }

.txtMotivo2 {
    display: flex;
    resize: none;
    overflow: auto;
    width: 360px;
    height: 100px;
    border-radius: 4px;
    border: 1px solid #ccc;
    margin-top: 4px;
    font-family: Arial, sans-serif;
}

#modalMotivo2 .btn_css {
    background-color: #dc3545;
    border-radius: 4px;
    padding: 6px 42%;
    margin-top: 6px;
    cursor: pointer;
    color: black;
    font-family: Arial, sans-serif;
}

    #modalMotivo2 .btn_css:hover {
        background-color: #dc3545;
        border-radius: 4px;
        color: white;
    }


/*Modal Recebimento*/
.modal2222 {
        display: none;
        position: fixed;
        z-index: 9999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
    }

    .modal-content2222 {
        background-color: white;
        margin: 6% auto;
        padding: 20px;
        width: 400px;
        border-radius: 6px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    }

    .h1__titulo-modal2222 {
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

    #meuModal2222 .span__close {
        color: white;
        float: right;
        font-size: 20px;
        cursor: pointer;
        z-index: 2;
        position: relative;
        top: -85px;
    }

        #meuModal2222 .span__close:hover {
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

    #meuModal2222 .btn_enviar_recebimento {
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
    .css__modal-Embarque2 {
        display: none;
        position: fixed;
        z-index: 99999;
        left: 0;
        top: -0%;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.4);
        overflow: auto;
    }

    .modal__content-Embarque2 {
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

    #modalEmbarque2 .btn_enviar_embarque {
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


<div id="modalRecusados" class="css__modal-Recusados">
    <div class="modal__content-Recusados">
        <h1 class="h1__titulo-modal">Recusados</h1>
        <asp:Button CssClass="btnAtualizar" Text="🔄" runat="server" OnClick="Unnamed_Click" />
        <span enableviewstate="true" class="span__close" runat="server" onclick="fehcarModalRecusados();">x</span>

        <div class="grid-container-recusados">
            <asp:GridView runat="server" ID="gvCadastros" AutoGenerateColumns="false" GridLines="None" CssClass="grid-table-recusados" UseAccessibleHeader="true" OnRowCommand="gvCadastros_RowCommand">

                <Columns>
                    <asp:BoundField DataField="CadastroID" HeaderText="Nº Cadastro" />
                    <asp:BoundField DataField="TipoCadastro" HeaderText="Tipo cadastro" />
                    <asp:BoundField DataField="FornecedorCliente" HeaderText="Fornecedor/Cliente" />
                    <asp:BoundField DataField="Transportadora" HeaderText="Fornecedor/Cliente" />
                    <asp:BoundField DataField="Placa" HeaderText="Placa" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnAbrir" runat="server" CssClass="css_abir-dados-modal" CommandName="Editar"
                                CommandArgument='<%# Eval("CadastroID")%>' Text="📝" ToolTip="Editar" />
                            <asp:Button ID="btnVisualizar" runat="server" CssClass="css_abir-dados-modal" CommandName="Visualizar"
                                CommandArgument='<%# Eval("CadastroID")%>' Text="📂" ToolTip="Visualizar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>

<!-- Estrotura para abrir o modal Embarque  -->
<div id="modalEmbarque2" class="css__modal-Embarque2">
    <div class="modal__content-Embarque2">
        <h1 class="h1__titulo-modal">Embarque</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalEmbarque2();">x</span>

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
            <asp:Button ID="btnCadastrar" runat="server" Text="Enviar" CssClass="btn_enviar_embarque" OnClick="btn_enviar_embarque" OnClientClick="this.value='Processando...'; this.disabled=true;" UseSubmitBehavior="false"/>
        </div>
    </div>
</div>

<!-- Estrotura para abrir o modal Recebimento -->
<div id="meuModal2222" class="modal2222">
    <div class="modal-content2222">
        <h1 class="h1__titulo-modal2222">Recebimento</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModal2222();">x</span>
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
                <asp:TextBox ID="txtUF2" runat="server" CssClass="textBox" placeholder="UF" MaxLength="2" onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>
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
            <asp:Button ID="btn2" runat="server" Text="Enviar" CssClass="btn_enviar_recebimento"   OnClick="btn_enviar_recebimento" OnClientClick="this.value='Processando...'; this.disabled=true;" UseSubmitBehavior="false"/>
        </div>
    </div>
</div>

<!-- Abrir modal motivo -->
<div id="modalMotivo2" class="css__modal-Motivo2">
    <div class="modal__content-Motivo2">
        <h1 class="h1__titulo-modal">Motivo</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fehcarModalMotivo2();">x</span>
        <div class="div_motivo">
            <asp:TextBox runat="server" ID="txtMotivo2" CssClass="txtMotivo" TextMode="MultiLine" placeholder="Digite o motivo recusado" /> 
            <asp:Button ID="btnEnviarMotivo2"  Text ="Recusar" runat="server" CssClass="btn_css"/>
        </div>
    </div>
</div>

<script>
    function abrirModalRecusados() {
        document.getElementById("modalRecusados").style.display = "block";
    }
    function fehcarModalRecusados() {
        document.getElementById("modalRecusados").style.display = "none";
    }

    function abrirModal2222() {
        document.getElementById("meuModal2222").style.display = "block";
    }

    function fecharModal2222() {
        document.getElementById("meuModal2222").style.display = "none";
    }


    function abrirModalEmbarque2() {
        document.getElementById("modalEmbarque2").style.display = "block";
    }
    function fecharModalEmbarque2() {
        document.getElementById("modalEmbarque2").style.display = "none";
    }



    function abrirModalMotivo2() {
        document.getElementById("modalMotivo2").style.display = "block";
    }
    function fehcarModalMotivo2() {
        document.getElementById("modalMotivo2").style.display = "none";
    }

</script>
