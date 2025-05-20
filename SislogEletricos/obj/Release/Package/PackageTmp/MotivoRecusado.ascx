<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="~/MotivoRecusado.ascx.vb" Inherits="SislogEletricos.MotivoRecusado" %>

<link href="Style/MotivoRecusado.css" rel="stylesheet"  type="text/css"/>


<div id="modalMotivo" class="css__modal-Motivo">
    <div class="modal__content-Motivo">
        <h1 class="h1__titulo-modal">Motivo</h1>
        <span enableviewstate="true" class="span__close" runat="server" onclick="fehcarModalMotivo();">x</span>
        <div class="div_motivo">
            <asp:TextBox runat="server" ID="txtMotivo" CssClass="txtMotivo" TextMode="MultiLine" placeholder="Digite o motivo recusado" /> 
            <asp:Button ID="btnEnviarMotivo"  Text ="Recusar" runat="server" CssClass="btn_css" OnClick="btnEnviarMotivo_Click" OnClientClick="this.value='Processando...'; this.disabled=true;" UseSubmitBehavior="false"/>
        </div>
    </div>
</div>

