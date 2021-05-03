<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Proveedores_OC.aspx.cs" Inherits="AMALIA.Proveedores_OC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">

    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_NUEVO" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <span style="font-size: large"><strong><i class="fa fa-user-tie"></i>&nbsp;Proveedores</strong> </span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ClientIDMode="AutoID" ID="B_NUEVO" CssClass="btn btn-primary btn-sm" OnClick="B_NUEVO_Click"><i class="fa fa-user-tie"></i>&nbsp;CREAR NUEVO PROVEEDOR</asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Proveedores</li>
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
                                            <asp:AsyncPostBackTrigger ControlID="B_GUARDAR" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="B_VOLVER" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="body table-responsive">
                                                    <asp:GridView DataKeyNames="id_oc_proveedor" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered tablaprincipal table-sm" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="id_oc_proveedor" DataField="id_oc_proveedor" Visible="false" />
                                                            <asp:BoundField HeaderText="Rut" DataField="rut" />
                                                            <asp:BoundField HeaderText="Nombre" DataField="nombre_corto" />
                                                            <asp:BoundField HeaderText="Razón Social" DataField="razon_social" />
                                                            <asp:BoundField HeaderText="Dirección" DataField="direccion" />
                                                            <asp:BoundField HeaderText="Fono" DataField="fono" />
                                                            <asp:BoundField HeaderText="Email" DataField="email" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
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
                                                            <h2><strong>INFORMACION</strong> DEL PROVEEDOR</h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label><b>Rut Proveedor</b></label>
                                                                            <asp:TextBox runat="server" ID="T_RUT" CssClass="form-control amalia-control" placeholder="Rut" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Razón Social</small>
                                                                            <asp:TextBox runat="server" ID="T_RAZONSOCIAL" CssClass="form-control amalia-control" placeholder="Razón Social" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Nombre</small>
                                                                            <asp:TextBox runat="server" ID="T_NOMBRE" CssClass="form-control amalia-control" placeholder="Nombre" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Comuna</small>
                                                                            <asp:TextBox runat="server" ID="T_COMUNA" CssClass="form-control amalia-control" placeholder="Comuna" required></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Ciudad</small>
                                                                            <asp:TextBox runat="server" ID="T_CIUDAD" CssClass="form-control amalia-control" placeholder="Ciudad" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <small>Dirección</small>
                                                                            <asp:TextBox runat="server" ID="T_DIRECCION" CssClass="form-control amalia-control" placeholder="Dirección" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Contacto</small>
                                                                            <asp:TextBox runat="server" ID="T_CONTACTO" CssClass="form-control amalia-control" placeholder="Contacto" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Fono</small>
                                                                            <asp:TextBox runat="server" ID="T_FONO" CssClass="form-control amalia-control" placeholder="Fono" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Banco</small>
                                                                            <asp:TextBox runat="server" ID="T_BANCO" CssClass="form-control amalia-control" placeholder="Banco" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Tipo Cuenta</small>
                                                                            <asp:DropDownList runat="server" ID="CB_TIPO_CUENTA" CssClass="form-control">
                                                                                <asp:ListItem Text="-- SELECCIONE --" Value="-- SELECCIONE --"></asp:ListItem>
                                                                                <asp:ListItem Text="Cuenta Corriente" Value="Cuenta Corriente"></asp:ListItem>
                                                                                <asp:ListItem Text="Cuenta Vista" Value="Cuenta Vista"></asp:ListItem>
                                                                                <asp:ListItem Text="Cuenta de Ahorro" Value="Cuenta de Ahorro"></asp:ListItem>
                                                                                <asp:ListItem Text="Cuenta Rut" Value="Cuenta Rut"></asp:ListItem>
                                                                                <asp:ListItem Text="Chequera Electronica" Value="Chequera Electronica"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Rut Cuenta</small>
                                                                            <asp:TextBox runat="server" ID="T_RUT_CUENTA" CssClass="form-control amalia-control" placeholder="Rut Cuenta" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>Nº Cuenta</small>
                                                                            <asp:TextBox runat="server" ID="T_NUMCUENTA" CssClass="form-control amalia-control" placeholder="Nº Cuenta" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <small>EMAIL</small>
                                                                            <asp:TextBox runat="server" ID="T_EMAIL" CssClass="form-control amalia-control" placeholder="Email" required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click"><i class="fa fa-save"></i>&nbsp; GUARDAR</asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-default btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click">VOLVER</asp:LinkButton>
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
            $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'lBfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                }
            });
        }

    </script>
</asp:Content>
