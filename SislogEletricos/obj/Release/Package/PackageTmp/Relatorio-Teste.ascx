<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="~/Relatorio-Teste.ascx.vb" Inherits="SislogEletricos.Relatorio" %>

<link href="Style/Relatorio.css" rel="stylesheet" type="text/css" />




<div id="modalRelatorio" class="css__modal-Relatorio">
    <div class="modal__content-Relatorio">
        <h1 class="h1__titulo-modal">Relatório</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fecharModalRelatorio();">x</span>
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
            <asp:Button ID="btnPermanencia" Text="Tempo de Permanencia" runat="server" OnClick="BtnTempoPermanencia_Click" CssClass="css_btn" />
            <asp:Button ID="btnGerarRelatorio" Text="Gerar Relatório" runat="server" OnClick="btnGerarRelatorio_Click" />
        </div>

        <div class="grid-container-relatorio">
            <asp:GridView runat="server" ID="gvRelatorio" AutoGenerateColumns="false" GridLines="None" CssClass="grid-table-relatorio" UseAccessibleHeader="true" OnRowCommand="gvRelatorio_RowCommand">
                <Columns>
                   
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>

