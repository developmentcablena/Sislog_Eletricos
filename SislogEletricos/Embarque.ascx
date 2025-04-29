<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Embarque.ascx.vb" Inherits="SislogEletricos.Embarque" %>

<link href="Style/Embarque.css" rel="stylesheet" type="text/css" />
<script src="https://unpkg.com/lucide@latest/dist/umd/lucide.min.js"></script>

<div id="modalEmbarque" class="css__modal-Embarque">
    <div class="modal__content-Embarque">
        <h1 class="h1__titulo-modal">Embarque</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalEmbarque();">x</span>

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
                <asp:TextBox runat="server" ID="txtPlaca" CssClass="textBox" placeholder="Placa" MaxLength="17" onkeyup="this.value = this.value.toUpperCase();"/>
            </div>
            <div class="div__group">
                <asp:Label Text="Material" runat="server" />
                <asp:TextBox runat="server" ID="txtMaterial" CssClass="textBox" placeholder="Material" MaxLength="50" onkeyup="this.value = this.value.toUpperCase();"/>
            </div>
            <div>
                <asp:Label Text="Quantidades" runat="server" />
                <asp:TextBox runat="server" ID="txtVolumes" CssClass="textBox" placeholder="Quantidades" TextMode="Number" />
            </div>
            <div class="div__group">
                <asp:Label Text="Espécie" runat="server" />
                <asp:TextBox runat="server" ID="txtPaletasbobinas"  CssClass="textBox" placeholder="Espécie" onkeyup="this.value = this.value.toUpperCase();"/>  
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
                <asp:TextBox runat="server" ID="txtCarga" CssClass="textBox" placeholder="Capacidade de carga"  onkeyup="formatarMoeda(this)" />
            </div>
            <div class="div__group">
                <asp:Label Text="Data" runat="server" />
                <asp:TextBox ID="txtData" runat="server" TextMode="DateTimeLocal" CssClass="textBox" />
            </div>
             <div class="div__group">
                <asp:Label Text="Data Embarque" runat="server" />
                <asp:TextBox ID="dataJanela" runat="server"  TextMode="DateTimeLocal" CssClass="textBox"/>  
            </div>
            <br />
            <div>
                <asp:Label Text="Observação" runat="server" />
                <asp:TextBox runat="server" ID="txtObservacao" CssClass="textBox__obs" TextMode="MultiLine" placeholder="Observação" />
            </div>
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn-cadastrar" OnClick="btnCadastrar_Embarque_Click" OnClientClick="this.value='Processando...'; this.disabled=true; document.body.style.cursor='wait';" UseSubmitBehavior="false"/>
        </div>
    </div>
</div>

<script>
    function atualizarModal() {
        // Código para atualizar o modal
        $('#upModal1').modal('show'); // Exemplo usando jQuery para mostrar o modal
    }
    function abrirModalEmbarque() {
        document.getElementById("modalEmbarque").style.display = "block";
    }

    function fecharModalEmbarque() {
        document.getElementById("modalEmbarque").style.display = "none";
    }
    lucide.createIcons(); // Inicializar os ícones do Lucide

    function formatarMoeda(campo) {
        let valor = campo.value.replace(/\D/g, ''); // Remove tudo que não for dígito
        valor = (valor / 100).toFixed(2) + '';
        valor = valor.replace(".", ",");
        valor = valor.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
        campo.value = valor;
    }

</script>
