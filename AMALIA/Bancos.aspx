﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Bancos.aspx.cs" Inherits="AMALIA.Bancos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_NUEVO" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <span style="font-size: large"><strong >BANCOS</strong> DE DATOS</span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                             <asp:LinkButton runat="server" ID="B_NUEVO" CssClass="btn btn-primary btn-simple btn-sm" OnClick="B_NUEVO_Click" Text="+ Nuevo"></asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="javascript:void(0);">Index</a></li>
                                                <li class="breadcrumb-item active">Bancos</li>
                                            </ul>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" EventName="RowCommand" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="body table-responsive">
                                                    <asp:GridView DataKeyNames="ID_TABLA" OnRowDataBound="G_PRINCIPAL_RowDataBound" OnRowCommand="G_PRINCIPAL_RowCommand" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered tablaprincipal" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ID_TABLA" HeaderText="ID" />
                                                            <asp:BoundField DataField="NOMBRE_TABLA" HeaderText="BANCO" ItemStyle-Width="100%" />
                                                            <asp:BoundField DataField="ACTIVO" HeaderText="ACTIVO" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('¿Seguro que desea eliminar el registro?');">
                                                                   <i class="icon-trash"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="PANEL_DETALLE1" Visible="false">
                                                <div class="body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h2><strong>INFORMACION</strong> DEL BANCO</h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Nombre Banco</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_NOMBRE_TABLA" CssClass="form-control amalia-control" placeholder="Nombre del Banco" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:Panel runat="server" ID="PANEL_COLUMNAS" Visible="false">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="table-responsive">
                                                                    <h2><strong>COLUMNAS</strong> DEL BANCO</h2>
                                                                    <asp:LinkButton runat="server" ID="B_AGREGAR_COLUMNA" CssClass="btn btn-primary btn-simple btn-sm" OnClick="B_AGREGAR_COLUMNA_Click" Text="+ Nueva columna"></asp:LinkButton>
                                                                    <br />
                                                                           <br />
                                                                    <asp:GridView DataKeyNames="ID_TABLA_DETALLE, ID_TIPO_PARAMETRO" OnRowCommand="G_DETALLE1_RowCommand" runat="server" ID="G_DETALLE1" CssClass="table table-bordered tablaprincipal" AutoGenerateColumns="false" OnRowDataBound="G_DETALLE1_RowDataBound" ShowHeaderWhenEmpty="true" style="min-width:700px">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="ID_TABLA_DETALLE" HeaderText="ID" Visible="false" />
                                                                            <asp:BoundField DataField="ID_TABLA" HeaderText="BANCO" Visible="false" />
                                                                            <asp:BoundField DataField="ID_TIPO_PARAMETRO" HeaderText="TIPO" Visible="false" />                                                                         
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="COLUMNA" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="T_NOMBRE_COLUMNA" CssClass="form-control amalia-control" Text='<%# Eval("NOMBRE_COLUMNA")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="TIPO DATOS" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList runat="server" ID="CB_TIPO_PARAMETRO" CssClass="form-control amalia-control"></asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="ACCION" HeaderText="ESTADO" ControlStyle-Font-Bold="true" />
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('¿Seguro que desea eliminar el registro?');">
                                                                                    <i class="icon-trash"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            No se encontraron resultados.
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                    </asp:Panel>
                                                    <asp:Button UseSubmitBehavior="false" CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Guardar" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click" />
                                                    <asp:LinkButton CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Volver" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"></asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });
        function Datatables() {
            $('.tablaprincipal').DataTable({
                stateSave: true
            });
        }
    </script>
</asp:Content>
