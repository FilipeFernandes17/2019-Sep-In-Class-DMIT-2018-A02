<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplyChain.aspx.cs" Inherits="WebAppCRUD.Admin.SupplyChain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Active Suppliers</h1>

    <asp:ObjectDataSource ID="SupplierDataSource" runat="server"></asp:ObjectDataSource>

</asp:Content>
