<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Modulos.aspx.cs" Inherits="AMALIA.Modulos" %>

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
                                            <span style="font-size: large"><strong>MODULOS</strong> </span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                             <asp:LinkButton runat="server" ID="B_NUEVO" CssClass="btn btn-primary btn-simple btn-sm" OnClick="B_NUEVO_Click" Text="+ Nuevo"></asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="javascript:void(0);">Index</a></li>
                                                <li class="breadcrumb-item active">Modulos</li>
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
                                                    <asp:GridView DataKeyNames="ID_MODULO" OnRowDataBound="G_PRINCIPAL_RowDataBound" OnRowCommand="G_PRINCIPAL_RowCommand" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered tablaprincipal" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ID_MODULO" HeaderText="ID" />
                                                            <asp:BoundField DataField="NOMBRE_MODULO" HeaderText="NOMBRE MODULO" />
                                                            <asp:BoundField DataField="TIPO_MODULO" HeaderText="TIPO" />
                                                            <asp:BoundField DataField="WEB_APP" HeaderText="WEB / APP" />                                                           
                                                            <asp:BoundField DataField="ACTIVO" HeaderText="ESTADO" />
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
                                                            <h2><strong>INFORMACION</strong> DEL MODULO</h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Nombre del modulo</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_NOMBRE_MODULO" CssClass="form-control amalia-control" placeholder="Nombre del modulo" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Tipo</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                             <asp:DropDownList runat="server" ID="CB_TIPO_MODULO" CssClass="form-control amalia-control" required>                                                                               
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                 <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Disponibilidad</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList runat="server" ID="CB_WEBAPP" CssClass="form-control amalia-control" required>
                                                                                <asp:ListItem Text="WEB + APP" Value="WEB + APP" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="WEB" Value="WEB"></asp:ListItem>
                                                                                <asp:ListItem Text="APP" Value="APP"></asp:ListItem>
                                                                            </asp:DropDownList>
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
                                                                    <h2><strong>CAMPOS</strong> DEL MODULO</h2>
                                                                    <asp:LinkButton runat="server" ID="B_AGREGAR_COLUMNA" CssClass="btn btn-primary btn-simple btn-sm" OnClick="B_AGREGAR_COLUMNA_Click" Text="+ Nueva columna"></asp:LinkButton>
                                                                    <br />
                                                                    <br />                                                                    
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
