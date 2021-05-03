<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Repuestos.aspx.cs" Inherits="AMALIA.Repuestos" %>

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
                                            <span style="font-size: large"><strong>REPUESTOS</strong> </span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="B_NUEVO" CssClass="btn btn-primary btn-sm" OnClick="B_NUEVO_Click"><i class="fa fa-cogs"></i>&nbsp;CREAR NUEVO REPUESTO</asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Repuestos</li>
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
                                                    <div style="display: none">
                                                        <a id="MODAL_GASTO" href="#modalgasto" data-toggle="modal" data-target="#modalgasto"></a>
                                                    </div>
                                                    <asp:GridView DataKeyNames="ID_PRODUCTO, nom_producto, nombre_marca " ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ID_PRODUCTO" DataField="ID_PRODUCTO" Visible="false" />
                                                            <asp:BoundField HeaderText="NOMBRE" DataField="nom_producto" />
                                                            <asp:BoundField HeaderText="COD" DataField="cod_producto" />
                                                            <asp:BoundField HeaderText="SKU" DataField="SKU" />
                                                            <asp:BoundField HeaderText="MARCA" DataField="nombre_marca" />
                                                            <asp:BoundField HeaderText="CATEGORIA" DataField="nombre_categoria" />
                                                            <asp:BoundField HeaderText="STOCK ACTUAL" DataField="cantidad" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="verstock">
                                                                        <i class="fa fa-list-alt"></i>
                                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
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
                                                            <h2><strong>INFORMACION</strong> DEL REPUESTO</h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Nombre</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_NOMBRE" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Codigo</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_CODIGO" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>SKU</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_SKU" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Marca</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList runat="server" ID="CB_MARCA" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Categoria</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList runat="server" ID="CB_CATEGORIA" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix" id="div_stock" runat="server">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Stock Inicial</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_STOCK" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR</asp:LinkButton>
                                                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>
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


    <div class="modal fade" id="modalgasto" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_GASTO_GENERAL" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <h4>Stock</h4>
                                    <h4 id="stock_nombre_producto" runat="server"></h4>
                                    <div class="card">
                                        <div class="body table-responsive">
                                            <asp:GridView  ClientIDMode="AutoID" runat="server" ID="G_LOG_STOCK" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                <HeaderStyle CssClass="thead-dark" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="FECHA" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField HeaderText="MOVIMIENTO" DataField="entra_sale2" />
                                                    <asp:BoundField HeaderText="USUARIO" DataField="usuario" />
                                                    <asp:BoundField HeaderText="CANTIDAD" DataField="cantidad" />
                                                    <asp:BoundField HeaderText="CANTIDAD ANTES" DataField="cantidad_inicial" />
                                                    <asp:BoundField HeaderText="CANTIDAD DESPUES" DataField="cantidad_final" />
                                                    <asp:BoundField HeaderText="DOCUMENTO COMPRA" DataField="doc_compra" />
                                                    <asp:BoundField HeaderText="PRECIO COMPRA" DataField="precio_compra" />
                                                    <asp:BoundField HeaderText="O.T." DataField="id_ot" />
                                                    <asp:BoundField HeaderText="MOTIVO" DataField="motivo" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron resultados.
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="CERRAR_MODAL" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });

        function Datatables() {
            try {
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
            catch (ex) {

            }

            try {
                $('#<%= G_LOG_STOCK.ClientID %>').DataTable({
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
            catch (ex) {

            }
           
        }

        function GASTOGENERAL() {
            $('#MODAL_GASTO').click();
        }

    </script>
</asp:Content>
