<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EntradaNotas.aspx.vb" Inherits="SislogEletricos.EntradaNotas" %>

<%@ Register Src="~/CadastrarNF.ascx" TagName="CadastrarNF" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Style/EntradaNotas.css" rel="stylesheet" type="text/css" />
    <title>Entrada de Notas</title>
    <style>
        .css__modal-exportar {
            display: none;
            position: fixed;
            z-index: 99999;
            left: 0px;
            top: -0%;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.3);
            overflow: auto;
        }

        .modal__content-exportar {
            background-color: white;
            margin: 16% auto;
            padding: 20px;
            width: 294px;
            border-radius: 6px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
        }

        .ddlGeral {
            border-radius: 5px;
            border: 1px solid #6f6e6e;
            width: 50px;
            margin-bottom: 30px;
        }

        #div-Conteiner {
            display: flex;
            gap: 23px;
            align-items: center;
            margin-top: 10px;
        }

        .span-close {
            cursor: pointer;
            display: flex;
            flex-direction: row-reverse;
            padding: 0px 0px 6px;
        }

        .btnExportarExcel {
            border-radius: 5px;
            border: 1px solid black;
            width: 19rem;
            padding: 7px;
            cursor: pointer;
            background: #007cffa3;
        }

       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="h1__titulo-modal">Entrada de Notas</h1>
        <nav class="nav__button">
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="cadastrar" OnClientClick="abrirModalCadastrar(); return false;" />
            <asp:Button id="btnModalExportar"  Text="Exportar" runat="server" OnClientClick="abrirModalExportar(); return false;" CssClass="btn-exportar" />
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

        <%--Modal Exportar XLS--%>

        <div id="modalExportar" class="css__modal-exportar" runat="server">
            <div class="modal__content-exportar">
                <%--<h1 class="h1__titulo-modal">Cadastrar NF</h1>--%>
                <span enableviewstate="true" class="span-close" runat="server" onclick="fecharModalExportar();">x</span>
                <div id="div-Conteiner">
                    <div>
                        <asp:Label Text="Ano" runat="server" />
                        <asp:TextBox ID="txt_ano" runat="server" CssClass="ddlGeral" MaxLength="4" />
                    </div>
                    <div>
                        <asp:Label Text="Mês" runat="server" />
                        <asp:DropDownList runat="server" CssClass="ddlGeral" ID="ddl_mes">
                            <asp:ListItem Text="" />
                            <asp:ListItem Text="01" />
                            <asp:ListItem Text="02" />
                            <asp:ListItem Text="03" />
                            <asp:ListItem Text="04" />
                            <asp:ListItem Text="05" />
                            <asp:ListItem Text="06" />
                            <asp:ListItem Text="07" />
                            <asp:ListItem Text="08" />
                            <asp:ListItem Text="09" />
                            <asp:ListItem Text="10" />
                            <asp:ListItem Text="11" />
                            <asp:ListItem Text="12" />
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label Text="Dia" runat="server" />
                        <asp:DropDownList runat="server" CssClass="ddlGeral" ID="ddl_dia">
                            <asp:ListItem Text="" />
                            <asp:ListItem Text="01" />
                            <asp:ListItem Text="02" />
                            <asp:ListItem Text="03" />
                            <asp:ListItem Text="04" />
                            <asp:ListItem Text="05" />
                            <asp:ListItem Text="06" />
                            <asp:ListItem Text="07" />
                            <asp:ListItem Text="08" />
                            <asp:ListItem Text="09" />
                            <asp:ListItem Text="10" />
                            <asp:ListItem Text="11" />
                            <asp:ListItem Text="12" />
                            <asp:ListItem Text="13" />
                            <asp:ListItem Text="14" />
                            <asp:ListItem Text="15" />
                            <asp:ListItem Text="16" />
                            <asp:ListItem Text="17" />
                            <asp:ListItem Text="18" />
                            <asp:ListItem Text="19" />
                            <asp:ListItem Text="20" />
                            <asp:ListItem Text="21" />
                            <asp:ListItem Text="22" />
                            <asp:ListItem Text="23" />
                            <asp:ListItem Text="24" />
                            <asp:ListItem Text="25" />
                            <asp:ListItem Text="26" />
                            <asp:ListItem Text="27" />
                            <asp:ListItem Text="28" />
                            <asp:ListItem Text="29" />
                            <asp:ListItem Text="30" />
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btnGerarRelatorio" Text="Exportar XLSX" runat="server" OnClick="btnGerarRelatorioExcel_Click" CssClass="btnExportarExcel" />
            </div>
        </div>
        <script>
            function abrirModalExportar() {
                document.getElementById("modalExportar").style.display = "block";
            }
            function fecharModalExportar() {
                document.getElementById("modalExportar").style.display = "none";
            }
        </script>
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
