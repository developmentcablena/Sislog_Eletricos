<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Relatorio.aspx.vb" Inherits="SislogEletricos.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Style/Relatorio.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="h1__titulo-modal">Relatório</h1>
        <asp:Button Text="◀" runat="server" ID="btn_voltar" ToolTip="Voltar" OnClick="btn_voltar_Click" />
        <div id="div_global">
            <div id="css_div_conteiner">
                <div>
                    <asp:Label Text="Ano" runat="server" />
                    <asp:TextBox ID="txt_ano" runat="server" CssClass="css_geral" MaxLength="4" />
                </div>
                <div>
                    <asp:Label Text="Mês" runat="server" />
                    <asp:DropDownList runat="server" CssClass="css_geral" ID="ddl_mes">
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
                    <asp:DropDownList runat="server" CssClass="css_geral" ID="ddl_dia">
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
                <asp:Button ID="btnPermanencia" Text="Gerar Relatório" runat="server" OnClick="BtnTempoPermanencia_Click" CssClass="css_btn" />
                <asp:Button ID="btnGerarRelatorio" Text="Exportar XLSX" runat="server" OnClick="btnGerarRelatorio_Click" CssClass="css_btn2" />
            </div>

            <div class="grid-container-relatorio">
                <asp:GridView runat="server" ID="gvRelatorio" AutoGenerateColumns="false" GridLines="None" CssClass="grid-table-relatorio" UseAccessibleHeader="true" OnRowCommand="gvRelatorio_RowCommand">
                    <Columns>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </form>
</body>
</html>
