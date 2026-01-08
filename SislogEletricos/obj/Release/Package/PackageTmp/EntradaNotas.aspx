<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EntradaNotas.aspx.vb" Inherits="SislogEletricos.EntradaNotas" %>

<%@ Register Src="~/CadastrarNF.ascx" TagName="CadastrarNF" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Style/EntradaNotas.css" rel="stylesheet" type="text/css" />
    <title>Entrada de Notas</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="h1__titulo-modal">Entrada de Notas</h1>
        <nav class="nav__button">
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="cadastrar" OnClientClick="abrirModalCadastrar(); return false;" />
        </nav>
        <uc:CadastrarNF runat="server" />

        <asp:Button Text="◀" runat="server" ID="btn_voltar" ToolTip="Voltar" OnClick="btn_voltar_Click" />
        <div id="div_global">
            <div id="css_div_conteiner">
                <div class="grid-container-entradaNotas">
                    <asp:GridView runat="server" ID="gvEntradaNotas" AutoGenerateColumns="false" GridLines="None" CssClass="grid-table-entradaNotas" UseAccessibleHeader="true" OnRowCommand="gvEntradaNotas_RowCommand">
                        <Columns>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">

    function abrirModalCadastrar() {
            document.getElementById("modalCadastrar").style.display = "block";
        }
    
     function fehcarModalCadastrar() {
            document.getElementById("modalCadastrar").style.display = "none";
    }

</script>
